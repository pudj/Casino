namespace Casino.Api.Domain.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public decimal TransactionAmount { get; set; }
        public string TransactionType { get; set; }
        public Player Player { get; set; }
        public int PlayerId { get; set; }
        public Game Game { get; set; }
        public int GameId { get; set; }
        public DateTime? TransactionDate { get; set; } = DateTime.Now;

        public Transaction() { }

        public Transaction(string transactionType) { 
            TransactionType = transactionType;
        }

        public Transaction(string transactionType, decimal transactionAmount)
        {
            TransactionType = transactionType;
            TransactionAmount = transactionAmount;
        }
    }
}
