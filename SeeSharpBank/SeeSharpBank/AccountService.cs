using SeeSharpBank.Domain;

namespace SeeSharpBank
{
    public class AccountService : IAccountService
    {
        public Account CreateAccount(int bankId, string owner)
        {
            throw new System.NotImplementedException();
        }

        public bool Transact(Account a, Transaction trans)
        {
            throw new System.NotImplementedException();
        }

        public InvestmentAccount CreateInvestmentAccount(int bankId, string owner, InvestmentAccountType accountType)
        {
            throw new System.NotImplementedException();
        }

        public decimal GetBalance(int accountId)
        {
            throw new System.NotImplementedException();
        }

        public Bank GetBankById(int bankId)
        {
            throw new System.NotImplementedException();
        }

        public bool Transfer(int fromAccountId, int toAccountId, decimal amount)
        {
            throw new System.NotImplementedException();
        }
    }
}