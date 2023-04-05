namespace Casino.Api.Domain.Entities
{
    public class GameSession : Game
    {
        private bool _gameRunning;
        private bool _pendingTransaction;
        private decimal? _transactionBet;
        
        public void StartGame()
        {
            if (!IsGameRunning())
            {
                _gameRunning = true;
            }
            else
            {
                throw new Exception("Game already running.");
            }
        }
        public void FinishGame()
        {
            if (!HasPendingTransaction())
            {
                _gameRunning = false;
            }
            else
            {
                throw new Exception("Game has a pending transaction.");
            }
        }

        public bool IsGameRunning()
        {
            return _gameRunning;
        }

        public bool HasPendingTransaction()
        {
            return _pendingTransaction;
        }

        public void SetPendingTransaction(bool pendingTransaction, decimal? transactionBet) { 
            _pendingTransaction = pendingTransaction;
            _transactionBet = transactionBet;
        }

        public decimal? GetTransactionBet() => _transactionBet.GetValueOrDefault();

        public void SetGameData(Game game)
        {
            Name = game.Name;
            GameId = game.GameId;
            Description = game.Description;
            GameType = game.GameType;
            MinimalBet = game.MinimalBet;
            MaximalBet = game.MaximalBet;
            MaximalGain = game.MaximalGain;
        }
    }
}
