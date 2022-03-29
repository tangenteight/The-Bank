namespace SeeSharpBank.Domain
{
    public class Transaction
    {
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }

        public Account ToAccount { get; set; } = null;

        public Transaction(TransactionType type, decimal amount)
        {
            Type = type;
            Amount = amount;
        }

        public Transaction(TransactionType type, decimal amount, Account transferAccount)
        {
            Type = type;
            Amount = amount;
            ToAccount = transferAccount;
        }

    }
}