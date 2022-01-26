using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleBankProgram;
using SimpleBankProgram.Accounts;
using System;
using static SimpleBankProgram.Accounts.Types;

namespace BankTests
{
    [TestClass]
    public class BankTests
    {
        public Bank bank;

        Account account1;
        Account account2;

        [TestInitialize]
        public void BankSetup()
        {
            bank = new Bank("Test Bank");
            account1 = bank.AddBankAccount("Joe", 3000, AccountTypes.Checking);
            account2 = bank.AddBankAccount("Jack", 452, AccountTypes.Checking);
        }

        [TestMethod]
        public void TransferringFromAnAccountWithCorrectBalanceShouldSucceed()
        {
            Assert.IsTrue(bank.TransferFunds(account1.AccountID, account2.AccountID, 100).Completed);
            Assert.AreEqual(account1.Balance, 2900);
            Assert.AreEqual(account2.Balance, 552);
        }

        [TestMethod]
        public void TransferringFromAnAccountWithInsufficientFundsShouldFail()
        {
            Assert.IsFalse(bank.TransferFunds(account1.AccountID, account2.AccountID, 3001).Completed);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TransferringWithInvalidToAccountIdShouldFail()
        {
            bank.TransferFunds(account1.AccountID, Guid.NewGuid(), 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TransferringWithInvalidFromAccountIdShouldFail()
        {
            Assert.IsFalse(bank.TransferFunds(Guid.NewGuid(), account1.AccountID, 100).Completed);
        }

        [TestMethod]
        public void TransferringInvalidAmountShouldFail()
        {
            Assert.IsFalse(bank.TransferFunds(account1.AccountID, account2.AccountID, -594).Completed);
        }
    }
}
