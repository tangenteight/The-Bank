using Newtonsoft.Json;
using SeeSharpBank.Domain;
using System;
using System.Collections.Generic;

namespace SeeSharpBank
{
    public class AccountService : IAccountService
    {
        public Account CreateAccount(int bankId, string owner)
        {
            // simplified dramatically
            Account account = new Account()
            {
                BankId = bankId,
                Owner = owner,
                AccountId = 1,
                AccountType = AccountType.Checking,
                Ledger = new List<Transaction>()
            };

            return account;
        }

        public bool Transact(Account a, Transaction trans)
        {
            // This is where generic validation would occur
            // prior to executing the code below

            bool isInvestmentAccount = a.AccountType == AccountType.Investment;

            return trans.Type switch
            {
                TransactionType.Deposit => Deposit(a, trans),
                TransactionType.Withdraw => !isInvestmentAccount ? Withdraw(a, trans) : WithdrawInvestment(a, trans),
                TransactionType.Transfer => !isInvestmentAccount ? TransferChecking(a, trans) : TransferInvestment(a, trans),
                _ => throw new System.Exception("Invalid Transaction Type")
            };
        }

        private bool TransferInvestment(Account a, Transaction trans)
        {
            if (trans.ToAccount == null)
            {
                throw new System.Exception("To Account Is Null");
            }
            decimal balance = a.Balance;
            if (balance < trans.Amount)
            {
                // Cannot transfer if our balance is less than transfer amount                
                return false;
            }
            if (a.InvestmentAccountType == InvestmentAccountType.Individual)
            {
                if (trans.Amount > 500)
                {
                    // Individual Accounts cannot withdraw more than 500
                    return false;
                }
            }

            return Transfer(a, trans.ToAccount, trans.Amount);
        }

        private bool TransferChecking(Account a, Transaction trans)
        {
            if (trans.ToAccount == null)
            {
                throw new System.Exception("To Account Is Null");
            }
            decimal balance = a.Balance;
            if (balance < trans.Amount)
            {
                // Cannot transfer if our balance is less than transfer amount                
                return false;
            }

            return Transfer(a, trans.ToAccount, trans.Amount);
        }

        private bool WithdrawInvestment(Account a, Transaction trans)
        {
            if (a.InvestmentAccountType == InvestmentAccountType.Individual)
            {
                if (trans.Amount < -500)
                {
                    // Individual Accounts cannot withdraw more than 500
                    return false;
                }
            }
            // rules have been applied/validated
            return Withdraw(a, trans);            
        }

        private bool Withdraw(Account a, Transaction trans)
        {
            if (trans.Amount > 0)
            {
                return false;
            }
            a.Ledger.Add(trans);
            // Fake persist
            return true;
        }

        public Account CreateInvestmentAccount(int bankId, string owner, InvestmentAccountType accountType)
        {
            // simplified dramatically
            Account account = new Account()
            {
                BankId = bankId,
                Owner = owner,
                AccountId = 1,
                AccountType = AccountType.Investment,
                InvestmentAccountType = accountType,
                Ledger = new List<Transaction>()
            };

            return account;
        }

        private bool Deposit(Account a, Transaction trans)
        {
            if (trans.Amount < 0)
            {
                // Normally would have more robust return types :P
                // Cannot deposit a negative number
                return false;
            }
            // no limit on deposits for any type of Account
            // or investment account
            a.Ledger.Add(trans);
            // FUTURE: persist the transaction
            return true;
        }

        private bool Transfer(Account from, Account to, decimal amount)
        {
            // Getting to this method, all the rules have been validated and applied            
            // Withdrawel should be negative
            bool withDrewSuccess = Withdraw(from, new Transaction(TransactionType.Withdraw, amount * -1));
            if (withDrewSuccess)
            {
                return Deposit(to, new Transaction(TransactionType.Deposit, amount));
            }
            // Was not able to withdraw for some reason
            return false;
        }
    }
}