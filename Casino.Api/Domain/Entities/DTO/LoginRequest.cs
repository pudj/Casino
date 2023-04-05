using System.ComponentModel.DataAnnotations;

namespace Casino.Api.Domain.Entities.DTO
{
    public class LoginRequest
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
