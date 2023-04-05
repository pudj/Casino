using Casino.Api.Domain.Entities;
using Casino.Api.Domain.Entities.DTO;
using Casino.Api.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace Casino.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly GameSession _gameSession;
        public TransactionController(ITransactionService transactionService, GameSession gameSession) {
            _transactionService = transactionService;
        }

        [Authorize]
        [HttpPost("playerbets")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PlayerBets([FromBody] PlayerBetsRequest request) {
            var userName = HttpContext.User.Identity!.Name;
            string exceptionString = "";

            try
            {
                var response = await _transactionService.PlayerBets(userName, request.Amount);
                return Ok(response);
            }
            catch (Exception e)
            {
                if (_gameSession.HasPendingTransaction()) {
                   exceptionString = await _transactionService.ReturnBetAmountToWallet(userName);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, $"{e.Message}. {exceptionString}");
            }
            
        }
    }
}
