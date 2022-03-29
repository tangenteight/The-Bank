using SeeSharpBank.Domain;
using System.Collections.Generic;

namespace SeeSharpBank.Tests.TestModels
{
    public class TestAccountTransactionModel
    {
        public Account Account { get; set; }
        public List<Transaction> Transactions { get; set; }
        public decimal Expected { get; set; }
        public bool IsRegularChecking { get; set; }
        public InvestmentAccountType InvestmentAccountType { get; set; }
    }
}