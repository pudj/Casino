using Casino.Api.Domain.Common.Constants;
using Casino.Api.Domain.Entities;
using Casino.Api.Infrastructure.Persistence;
using Casino.Api.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Casino.Api.Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly UserManager<Player> _userManager;
        private readonly GameSession _gameSession;
        private readonly CasinoContext _context;

        public TransactionService(UserManager<Player> userManager, GameSession gameSession, CasinoContext context) { 
            _userManager = userManager;
            _gameSession = gameSession;
            _context = context;
        }
        public async Task<string> PlayerBets(string? userName, decimal betAmount)
        {
            if (_gameSession.Name.IsNullOrEmpty() || !_gameSession.IsGameRunning())
            {
                throw new InvalidOperationException("Game is not running. No game selected.");
            }

            Player player = await _context.Player.Where(x => x.UserName == userName!)!.FirstOrDefaultAsync();
            Wallet wallet = await _context.Wallets.FindAsync(player.WalletId)!;

            if (!CheckBalance(wallet.Balance, betAmount))
            {
                throw new InvalidOperationException($"You don't have enough credits. Wallet Balance: {wallet.Balance}");
            }
            if (!CheckBet(betAmount, _gameSession.MinimalBet, _gameSession.MaximalBet))
            {
                throw new InvalidOperationException($"Check minimal and maximal bet of the game: {_gameSession.Name}");
            }

            
            Transaction transaction = new Transaction(TransactionConstants.BET, betAmount);
            transaction.Player = player;
            transaction.GameId = _gameSession.GameId;

            _gameSession.SetPendingTransaction(true, betAmount);

            decimal betResult = PlayBet(betAmount, _gameSession.MaximalGain);
            if (betResult == 0)
            {
                wallet.SetBetFromBalance(betAmount);
            }
            else
            {
                wallet.SetWalletBalance(betResult);
            }
            
            _gameSession.SetPendingTransaction(false, null);
            _context.Transaction.Add(transaction);
            await _context.SaveChangesAsync();

            await SaveResult(betResult);

            return $"Transaction Sucessful. \n Result: {betResult}. \n Current balance: {wallet.Balance}";
        }

        private async Task SaveResult(decimal betResult)
        {
            if (betResult == 0)
            {
                _context.Results.Add(new Result { Amount = betResult, ResultType = ResultConstants.LOSE });
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Results.Add(new Result { Amount = betResult, ResultType = ResultConstants.WIN });
                await _context.SaveChangesAsync();
            }
        }

        private bool CheckBalance(decimal balance, decimal bet) {
            if (bet <= balance) {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckBet(decimal bet, decimal minimalBet, decimal maximalBet) {
            if (bet >= minimalBet && bet <= maximalBet) {
                return true;
            }
            else
            {
                return false;
            }
        }

        private decimal PlayBet(decimal bet, decimal maximalBet) { 
            Random rnd = new Random();
            int chance = rnd.Next(1, 10);
            List<int> loseRange = new List<int> { 1,2,3,4,5};
            List<int> winRange = new List<int> { 6,7,8,9 };
            if (loseRange.Contains(chance)) {
                return 0;
            }
            if (winRange.Contains(chance))
            {
                bet = bet * 2;
            }
            if (chance == 10) {
                bet = bet * 10;
            }
            if (bet > maximalBet) {
                bet = maximalBet;
            }
            return bet;
        }

        public async Task<string> ReturnBetAmountToWallet(string? userName)
        {
            string result = "";
            decimal? betAmount = _gameSession.GetTransactionBet();
            if (betAmount.HasValue)
            {
                Player player = await _context.Player.Where(x => x.UserName == userName!)!.FirstOrDefaultAsync();
                Wallet wallet = await _context.Wallets.FindAsync(player.WalletId)!;
                wallet.SetWalletBalance((decimal)betAmount);
                result = $"Returned {betAmount} to wallet.";
            }
            return result;
        }

    }
}
