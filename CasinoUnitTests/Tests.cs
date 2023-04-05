using Casino.Api.Domain.Common.Constants;
using Casino.Api.Domain.Entities;
using Casino.Api.Infrastructure.Persistence;
using Casino.Api.Infrastructure.Services;
using Casino.Api.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CasinoUnitTests
{
    public class Tests
    {
        public Wallet Wallet { get; set; }
        public GameSession GameSession { get; set; }
        [SetUp]
        public void Setup()
        {
            Wallet = new Wallet();
            GameSession = new GameSession();
        }

        [Test]
        public void SetWalletNegativeWalletBalance() { 
            try
            {
                Wallet.SetWalletBalance(-200);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            Console.WriteLine(Wallet.Balance);
        }

        [Test]
        public void StartGameSession() {
            GameSession.MinimalBet = 1;
            GameSession.MaximalBet = 10;
            GameSession.MaximalGain = 100;

            try
            {
                GameSession.StartGame();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            Transaction t = new Transaction(TransactionConstants.BET);
            t.Game = GameSession;
            //t.Player = //auth player
            //transaction init
            //set the bet
            //transaction set to finish
            //set on the game that transaction is finished
            //save result

            try
            {
                GameSession.FinishGame();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}