namespace SeeSharpBank.Domain
{
    public class Transaction
    {
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }

        public Transaction(TransactionType type, decimal amount)
        {
            Type = type;
            Amount = amount;
        }

    }
}