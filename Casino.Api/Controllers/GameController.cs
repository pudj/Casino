using Azure.Core;
using Casino.Api.Domain.Entities.DTO;
using Casino.Api.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Casino.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService) {
            _gameService = gameService;
        }

        [Authorize]
        [HttpPost("startgame")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> StartGame([FromBody] StartGameRequest request) {
            try
            {
                var response = await _gameService.StartGame(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }            
        }

        [AllowAnonymous]
        [HttpGet("getall")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllGames() { 
            var response = await _gameService.GetGames();
            return Ok(response);
        }

        [Authorize]
        [HttpGet("finishgame")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FinishGame() {
            try
            {
               var response = await _gameService.FinishGame();
               return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
