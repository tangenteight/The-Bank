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
            var bank = GetBanks().FirstOrDefault(i => i.Id == id);
            if (bank == null)
            {
                // Basic exception, always throw exceptions
                // & log
                throw new System.Exception($"Bank with id of {id} not found.");
            }
            return bank;
        }

        /// <summary>
        /// For this exercise I am primarily focusing on 
        /// Accounts
        /// </summary>
        /// <param name="bankId">The Id of the bank.</param>
        /// <param name="account">The Account we want to add.</param>
        /// <returns></returns>
        public bool AddAccount(int bankId, Account account)
        {
            Bank b = GetBankById(bankId);
            if (b == null)
            {
                // Basic exception, always throw exceptions
                // & log
                throw new System.Exception($"Bank with id of {bankId} not found.");
            }

            // assign the bank id to the account
            account.BankId = bankId;

            // If we had some business rules
            // I'd do the checks here
            b.Accounts.Add(account);

            return true;
        }
    }
}