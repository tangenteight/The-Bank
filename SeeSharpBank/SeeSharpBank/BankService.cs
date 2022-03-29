using SeeSharpBank.Domain;
using System.Collections.Generic;
using System.Linq;

namespace SeeSharpBank
{
    public class BankService : IBankService
    {
        private IEnumerable<Bank> GetBanks()
        {
            return new List<Bank>()
            {
                new Bank() { Id = 1, Name = "Bank 1", Accounts = new List<Account>() },
                new Bank() { Id = 2, Name = "Bank 2", Accounts = new List<Account>() }
            };
        }

        public Bank GetBankById(int id)
        {
            return GetBanks().FirstOrDefault(i => i.Id == id);
        }
    }
}