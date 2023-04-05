using Casino.Api.Domain.Entities;

namespace Casino.Api.Infrastructure.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<string> PlayerBets(string? userId, decimal balance);
        Task<string> ReturnBetAmountToWallet(string? userName);

    }
}
