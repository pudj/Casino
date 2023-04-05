using Casino.Api.Domain.Entities;
using Casino.Api.Domain.Entities.DTO;

namespace Casino.Api.Infrastructure.Services.Interfaces
{
    public interface IGameService
    {
        Task<string> StartGame(StartGameRequest request);
        Task<string> FinishGame();
        Task<List<Game>> GetGames();
    }
}
