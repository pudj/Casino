using Casino.Api.Domain.Entities.DTO;
using Casino.Api.Infrastructure.Services;
using Casino.Api.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Casino.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public PlayerController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var response = await _authenticationService.Register(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }  
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
               var response = await _authenticationService.Login(request);
               return Ok(response);
            }
            catch (Exception e)
            {
               return StatusCode(500, e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("logout")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> Logout() { 
            var response = await _authenticationService.Logout();
            return Ok(response);
        }
    }
}
