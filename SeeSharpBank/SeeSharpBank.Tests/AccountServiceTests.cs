﻿using NUnit.Framework;
using SeeSharpBank.Domain;
using SeeSharpBank.Tests.TestModels;
using System.Collections.Generic;
using System.Linq;

namespace SeeSharpBank.Tests
{
    public class AccountServiceTests
    {
        private AccountService _accountService;

        [SetUp]
        public void SetupTests()
        {
            _accountService = new AccountService();
        }

        [TestCase(1, "Bryan's Account")]
        [TestCase(2, "Test's Account")]
        public void TEST_Create_Account(int bankId, string accountName)
        {
            Account account = _accountService.CreateAccount(bankId, accountName);

            Assert.IsNotNull(account);
            Assert.AreEqual(bankId, account.BankId);
            Assert.AreEqual(accountName, account.Owner);
            Assert.AreEqual(AccountType.Checking, account.AccountType);
        }

        [TestCase(1, "Bryan's Account", InvestmentAccountType.Corporate)]
        [TestCase(2, "Test's Account", InvestmentAccountType.Individual)]
        public void TEST_Create_Investment_Account(int bankId, string accountName, InvestmentAccountType accountType)
        {
            InvestmentAccount account = _accountService.CreateInvestmentAccount(bankId, accountName, accountType);

            Assert.IsNotNull(account);
            Assert.AreEqual(bankId, account.BankId);
            Assert.AreEqual(accountName, account.Owner);
            Assert.AreEqual(accountType, account.InvestmentAccountType);
            Assert.AreEqual(AccountType.Checking, account.AccountType);
        }

        [TestCase(500f, 500f)]
        [TestCase(1500f, 1500f)]
        [TestCase(20f, 20f)]
        [TestCase(-20f, 0)]
        public void TEST_Deposit(float amount, float expected)
        {
            // in c# you can't use decimals in attributes for test cases
            // in the scenarios for this challenge, type conversions
            // are okay.
            decimal decimalValue = (decimal)amount;
            Account account = _accountService.CreateAccount(1, "Test Account");

            _accountService.Transact(account, new Transaction(TransactionType.Deposit, decimalValue));
            float floatValue = (float)account.Balance;
            Assert.AreEqual(expected, floatValue);
        }

        [TestCase(500f, 500f, InvestmentAccountType.Corporate)]
        [TestCase(1500f, 1500f, InvestmentAccountType.Individual)]
        [TestCase(20f, 20f, InvestmentAccountType.Corporate)]
        [TestCase(-20f, 0, InvestmentAccountType.Individual)]
        public void TEST_Deposit_InvestmentAccount(float amount, float expected, InvestmentAccountType type)
        {
            decimal decimalValue = (decimal)amount;
            InvestmentAccount account = _accountService.CreateInvestmentAccount(1, "Test Account", type);

            _accountService.Transact(account, new Transaction(TransactionType.Deposit, decimalValue));
            float floatValue = (float)account.Balance;
            Assert.AreEqual(expected, floatValue);
        }

        [TestCase(-500f, 500f, InvestmentAccountType.Corporate)]
        [TestCase(300f, 700f, InvestmentAccountType.Individual)]
        [TestCase(20f, 980f, InvestmentAccountType.Corporate)]
        [TestCase(-20f, 1000, InvestmentAccountType.Individual)] // Test the case where someone tries to make a "Deposit" transaction with a negative
        public void TEST_Withdraw_InvestmentAccount(float amount, float expected, InvestmentAccountType type)
        {
            decimal decimalValue = (decimal)amount;
            InvestmentAccount account = _accountService.CreateInvestmentAccount(1, "Test Account", type);
            for (int i =0; i < 10; i++)
            {
                _accountService.Transact(account, new Transaction(TransactionType.Deposit, 100m));
            }

            _accountService.Transact(account, new Transaction(TransactionType.Withdraw, decimalValue));
            float floatValue = (float)account.Balance;
            Assert.AreEqual(expected, floatValue);
        }

       [Test]
        public void TEST_Transactions_Checking()
        {
            ExecuteTransactiontest(GetAccountForTransactionTesting_ScenarioOne());
        }

        [Test]
        public void TEST_Transactions_IndividualInvestmentAccount()
        {
            ExecuteTransactiontest(GetAccountForTransactionTesting_ScenarioTwo());
        }

        [Test]
        public void TEST_Transactions_CorporateInvestmentAccount()
        {
            ExecuteTransactiontest(GetInvestmentAccountForTransactionTesting_ScenarioThree());
        }

        private void ExecuteTransactiontest(TestAccountTransactionModel scenario)
        {
            // Account account, List<Transaction> transactions, float expected
            Account account = scenario.Account;

            Account transferAccount = new Account() { AccountId = 2, AccountType = AccountType.Checking, BankId = 1, Ledger = new List<Transaction>(), Owner = "Transferee" };

            decimal amountTransferred = 0m;
            foreach (Transaction t in scenario.Transactions)
            {
                if (t.Type == TransactionType.Transfer)
                {
                    // try to withdraw funds
                    bool withDrew = _accountService.Transact(account, new Transaction(TransactionType.Withdraw, t.Amount));
                    if (withDrew)
                    {
                        // successfully withdrew the funds
                        // i.e. biz rules passed
                        bool depositToTransferAccount = _accountService.Transact(transferAccount, new Transaction(TransactionType.Deposit, t.Amount * 1));
                        Assert.IsTrue(depositToTransferAccount);
                        amountTransferred += t.Amount;
                    }
                }
                else
                {
                    _accountService.Transact(account, t);
                }

            }

            Assert.AreEqual(scenario.Expected, account.Balance);

            // Make sure the account we did any transfers to have the right balance too
            Assert.AreEqual(amountTransferred, transferAccount.Balance);
        }

        private static TestAccountTransactionModel GetAccountForTransactionTesting_ScenarioOne()
        {
            return new TestAccountTransactionModel()
            {
                Account = new Account
                {
                    AccountId = 1,
                    AccountType = AccountType.Checking,
                    BankId = 1,
                    Ledger = new List<Transaction>() { new Transaction(TransactionType.Deposit, 500) },
                    Owner = "Bryan"
                },
                Expected = 2689,
                IsRegularChecking = true,
                Transactions = new List<Transaction>() // The transaction we want to run for our test scenario
                {
                    new Transaction(TransactionType.Deposit, 500), // 1000
                    new Transaction(TransactionType.Withdraw, -500), // 500
                    new Transaction(TransactionType.Transfer, -250), // 250
                    new Transaction(TransactionType.Deposit, 1029), // 1279
                    new Transaction(TransactionType.Withdraw, -279), // 1000
                    new Transaction(TransactionType.Deposit, 1689) // 2689
                }
            };
        }
        private static TestAccountTransactionModel GetAccountForTransactionTesting_ScenarioTwo()
        {
            return new TestAccountTransactionModel()
            {
                Account = new Account
                {
                    AccountId = 1,
                    AccountType = AccountType.Investment,
                    BankId = 1,
                    Ledger = new List<Transaction>() { new Transaction(TransactionType.Deposit, 10000) },
                    Owner = "Bryan"
                },
                Expected = 2689,
                IsRegularChecking = false,
                InvestmentAccountType = InvestmentAccountType.Individual,
                Transactions = new List<Transaction>()  // The transaction we want to run for our test scenario
                {
                    new Transaction(TransactionType.Deposit, 500), // 1000
                    new Transaction(TransactionType.Withdraw, -500), // 500
                    new Transaction(TransactionType.Transfer, -250), // 250                    
                    new Transaction(TransactionType.Deposit, 1029), // 1279
                    new Transaction(TransactionType.Withdraw, -1279), // 1279 -- Should not be able to withdraw more than 500
                    new Transaction(TransactionType.Deposit, 1689), // 2689
                    new Transaction(TransactionType.Transfer, -2689), // 2689 // Should not be able to withdraw to transfer more than 500
                }
            };
        }

        private static TestAccountTransactionModel GetInvestmentAccountForTransactionTesting_ScenarioThree()
        {
            return new TestAccountTransactionModel()
            {
                Account = new Account
                {
                    AccountId = 1,
                    AccountType = AccountType.Investment,
                    BankId = 1,
                    Ledger = new List<Transaction>() { new Transaction(TransactionType.Deposit, 10000) },
                    Owner = "Bryan"
                },
                Expected = 1865242,
                IsRegularChecking = false,
                InvestmentAccountType = InvestmentAccountType.Corporate,
                Transactions = new List<Transaction>()  // The transaction we want to run for our test scenario
                {
                    new Transaction(TransactionType.Withdraw, -10000), // on corporate we don't have a withdraw limit
                    new Transaction(TransactionType.Deposit, 1800000), // 1800000
                    new Transaction(TransactionType.Transfer, -800000), // 1000000
                    new Transaction(TransactionType.Deposit, 865242), // 1865242
                }
            };
        }
    }
}