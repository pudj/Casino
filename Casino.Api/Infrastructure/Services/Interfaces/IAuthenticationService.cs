using Casino.Api.Domain.Entities.DTO;

namespace Casino.Api.Infrastructure.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> Register(RegisterRequest request);
        Task<string> Login(LoginRequest request);
        Task<string> Logout();
    }
}
