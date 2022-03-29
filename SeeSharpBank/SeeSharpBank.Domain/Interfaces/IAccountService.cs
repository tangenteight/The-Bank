namespace SeeSharpBank.Domain
{
    /// <summary>
    /// The Contract for working with the See Sharp Bank
    /// The reason I have this in the Domain project
    /// is because the purpose of this interface is to define
    /// the operations that any concrete implementation of the See Sharp Bank can do
    /// The actual implementation will be implemented in another project
    /// This gives me some flexibility because I may have multiple implementations
    /// with other Banking Services in the future. The implementation for this project
    /// May rely on a bank like Wells Fargo, but if I ever needed to extend our service
    /// I may have another bank on the back end, like Chase. So, defining this here,
    /// along with the common domain models affords me the ability to extend while
    /// maintaining the definitions and contracts.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Retreive the Bank for an Account
        /// </summary>
        /// <param name="bankId">The id of the bank.</param>
        /// <returns></returns>
        Bank GetBankById(int bankId);

        /// <summary>
        /// Creates a normal Account (checking)
        /// </summary>
        /// <param name="bankId">Id of the Bank</param>
        /// <param name="owner">Owner of the Account</param>
        /// <param name="accountType">Type of Account</param>
        /// <returns></returns>
        Account CreateAccount(int bankId, string owner);

        /// <summary>
        /// Creates a normal Account (checking)
        /// </summary>
        /// <param name="bankId">Id of the Bank</param>
        /// <param name="owner">Owner of the Account</param>
        /// <param name="accountType">Type of Account</param>
        /// <returns></returns>
        InvestmentAccount CreateInvestmentAccount(int bankId, string owner, InvestmentAccountType accountType);

        /// <summary>
        /// Retreives the Balance for an account
        /// </summary>
        /// <param name="accountId">The ID of the account who's balance we want to check.</param>
        /// <returns>The balance, in decimal format, of the account.</returns>
        decimal GetBalance(int accountId);

        /// <summary>
        /// Performs a transaction on an single account
        /// </summary>
        /// <param name="a">The Account</param>
        /// <param name="trans">The transaction to run</param>
        /// <returns>A boolean indicating whether or not the transaction was successful</returns>
        bool Transact(Account a, Transaction trans);

        /// <summary>
        /// A method to withdraw from one account and deposit into another
        /// I have this as a separate method in order to be clear about the purpose/intent of transfer
        /// and to be able to utilize the business rules I have in Deposit/Withdraw in addition
        /// to any other business rules for a transfer
        /// </summary>
        /// <param name="fromAccountId">Which account is this coming from?</param>
        /// <param name="toAccountId">Which account is this going to?</param>
        /// <param name="amount">What is the amount to transfer?</param>
        /// <returns>A boolean indicating whether or not the transfer was successful</returns>
        bool Transfer(int fromAccountId, int toAccountId, decimal amount);
    }
}