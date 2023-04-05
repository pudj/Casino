using Casino.Api.Domain.Entities;
using Casino.Api.Domain.Entities.DTO;
using Casino.Api.Infrastructure.Persistence;
using Casino.Api.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Casino.Api.Infrastructure.Services
{
    public class GameService : IGameService
    {
        private GameSession _gameSession;
        private readonly CasinoContext _context;
        public GameService(GameSession gameSession, CasinoContext context)
        {
            _gameSession = gameSession;
            _context = context;
        }
        public async Task<string> FinishGame()
        {
            try
            {
               _gameSession.FinishGame();
            }
            catch (Exception e)
            {

                return $"{e.Message}";
            }

            return "Game finished.";
        }

        public async Task<List<Game>> GetGames()
        {
            return await _context.Games.ToListAsync();
        }

        public async Task<string> StartGame(StartGameRequest request)
        {
            Game? game = await _context.Games.FindAsync(request.GameId);
            if (game == null)
            {
                return $"Could not find Game by ID: {request.GameId}";
            }

            _gameSession.SetGameData(game);

            try
            {
                _gameSession.StartGame();
            }
            catch (Exception e)
            {

                return $"{e.Message}";
            }
            return $"Game {_gameSession.Name} started.";
        }
    }
}
