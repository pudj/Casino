using System.ComponentModel.DataAnnotations;

namespace Casino.Api.Domain.Entities.DTO
{
    public class StartGameRequest
    {
        [Required]
        public int GameId { get; set; }
    }
}
