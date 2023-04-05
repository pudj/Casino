using Casino.Api.Domain.Entities;
using Casino.Api.Infrastructure.Persistence;
using Casino.Api.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Casino.Api.Infrastructure.Services
{
    public class WalletService : IWalletService
    {
        private readonly CasinoContext _casinoContext;

        public WalletService(CasinoContext casinoContext)
        {
            _casinoContext = casinoContext;
        }
        public async Task<Wallet> CreateWalletOnPlayerRegister(Wallet wallet)
        {
             await _casinoContext.Wallets.AddAsync(wallet);
            _casinoContext.SaveChanges();
            return wallet;
        }


    }
}
