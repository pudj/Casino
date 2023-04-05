namespace Casino.Api.Domain.Entities
{
    public class Wallet
    {
        public int WalletId { get; set; }
        public decimal Balance { get; private set; }

        public Wallet() { 
            SetWalletBalance(100);
        }
        public void SetWalletBalance(decimal balance) {
            if (IsBalanceValid(balance)) { 
                Balance += balance;
            }
            else
            {
                throw new InvalidOperationException("Balance cannot be below 100.");
            }
        }

        public bool IsBalanceValid(decimal balance) {
            decimal currentBalance = Balance;
            if (currentBalance + balance < 0)
            {
                return false;
            }
            return true;
        }

        public void SetBetFromBalance(decimal betAmount) {
            if (IsBalanceValid(betAmount))
            {
                Balance = Balance - betAmount;
            }
            else
            {
                throw new InvalidOperationException("Balance cannot be below 100.");
            }
        }

    }
}
