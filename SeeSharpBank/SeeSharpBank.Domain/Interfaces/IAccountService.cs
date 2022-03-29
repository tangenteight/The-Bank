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
        Account CreateInvestmentAccount(int bankId, string owner, InvestmentAccountType accountType);

        /// <summary>
        /// Performs a transaction on an single account
        /// </summary>
        /// <param name="a">The Account</param>
        /// <param name="trans">The transaction to run</param>
        /// <returns>A boolean indicating whether or not the transaction was successful</returns>
        bool Transact(Account a, Transaction trans);
    }
}