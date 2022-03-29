using NUnit.Framework;
using SeeSharpBank.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeeSharpBank.Tests
{
    public class BankServiceTests
    {
        [TestCase(1,false)]
        [TestCase(2,false)]
        [TestCase(3, true)]
        public void Test_Get_Bank_By_Id(int bankId, bool shouldThrowException)
        {
            IBankService bankService = new BankService();
            Exception ex = null;
            Bank bank = null;
            try
            {
                bank = bankService.GetBankById(bankId);
            }
            catch(Exception e)
            {
                ex = e;
            }

            if (shouldThrowException)
            {
                Assert.NotNull(ex);
            }
            else
            {
                Assert.AreEqual(bankId, bank.Id);
            }
        }
        [Test]
        public void Test_Add_New_Account()
        {
            int tstBankId = 1;
            IBankService bankService = new BankService();
            Account a = new Account
            {
                AccountId = 1,
                AccountType = AccountType.Checking,
                Ledger = new List<Transaction>(),
                Owner = "TST_1"
            };

            bankService.AddAccount(tstBankId, a);
            Assert.AreEqual(tstBankId, a.BankId);
        }
    }
}
