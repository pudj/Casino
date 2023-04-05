using Casino.Api.Domain.Entities;
using Casino.Api.Domain.Entities.DTO;
using Casino.Api.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Casino.Api.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<Player> _userManager;
        private readonly SignInManager<Player> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IWalletService _walletService;
        public AuthenticationService(UserManager<Player> userManager, SignInManager<Player> signInManager, IConfiguration configuration, IWalletService walletService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _walletService = walletService;
        }
        public async Task<string> Login(LoginRequest request)
        {
            Player? player = await _userManager.FindByNameAsync(request.Username);

            if (player == null)
            {
                player = await _userManager.FindByEmailAsync(request.Username);
            }

            if (player == null || !await _userManager.CheckPasswordAsync(player, request.Password))
            {
                throw new ArgumentException($"Unable to authenticate player {request.Username}");
            }

            var authClaims = new List<Claim>
            {
                { new Claim(ClaimTypes.Name, player.UserName) },
                { new Claim(ClaimTypes.Email, player.Email) },
                { new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) }
            };

            var token = GetToken(authClaims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> Logout()
        {
            await _signInManager.SignOutAsync();
            return $"User logged out.";
        }

        public async Task<string> Register(RegisterRequest request)
        {
            Player? playerByEmail = await _userManager.FindByEmailAsync(request.Email);
            Player? playerByUsername = await _userManager.FindByNameAsync(request.UserName);

            if (playerByEmail != null || playerByUsername != null) {
                throw new ArgumentException($"Player with email {request.Email} or username {request.UserName} already exists.");
            }

            var wallet = await _walletService.CreateWalletOnPlayerRegister(new Wallet()); 

            Player newPlayer = new Player
            {
                Email = request.Email,
                UserName = request.UserName,
                Wallet = wallet,
                WalletId = wallet.WalletId,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(newPlayer, request.Password);
            if (!result.Succeeded) {
                throw new Exception($"Unable to register new player with Username: {request.UserName}. \n {GetErrorsText(result.Errors)}");
            }

            return await Login(new LoginRequest { Username = request.UserName, Password = request.Password });
        }

        private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return token;
        }

        private string GetErrorsText(IEnumerable<IdentityError> errors)
        {
            return string.Join(", ", errors.Select(error => error.Description).ToArray());
        }
    }
}
