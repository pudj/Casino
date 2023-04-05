using Casino.Api.Domain.Entities;

namespace Casino.Api.Infrastructure.Services.Interfaces
{
    public interface IWalletService
    {
        Task<Wallet> CreateWalletOnPlayerRegister(Wallet wallet);
    }
}
