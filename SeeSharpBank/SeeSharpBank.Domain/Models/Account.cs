using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SeeSharpBank.Domain
{
    public class Account
    {
        public int BankId { get; set; }
        public int AccountId { get; set; }
        public string Owner { get; set; }
        public decimal Balance => Ledger.Sum(i => i.Amount);
        public AccountType AccountType { get; set; }
        public InvestmentAccountType InvestmentAccountType { get; set; } = InvestmentAccountType.None;
        // An Account has a ledger
        // which is a collection of transactions
        // that represent the activity on the account
        public List<Transaction> Ledger { get; set; }
    }
}