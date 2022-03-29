using System.Collections.Generic;

namespace SeeSharpBank.Domain
{
    // Quick Interface to abstract getting banks for this exercise
    public interface IBankService
    {
        /// <summary>
        /// Load a Bank by ID
        /// </summary>
        /// <param name="id">The Bank Id</param>
        /// <returns>The bank object</returns>
        Bank GetBankById(int id);

        /// <summary>
        /// Adds an account to a bank
        /// </summary>
        /// <param name="bankId">The bank who will be managing the account</param>
        /// <param name="account">The account we want to add</param>
        /// <returns></returns>
        bool AddAccount(int bankId, Account account);
    }
}
