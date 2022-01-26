using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleBankProgram;
using SimpleBankProgram.Accounts;
using System;
using static SimpleBankProgram.Accounts.Types;

namespace BankTests
{
    [TestClass]
    public class AccountTests
    {
        public Bank bank;

        public Account individualInvestmentAccount;
        public Account checkingAccount;
        public Account corporateInvestmentAccount;

        [TestInitialize]
        public void AccountSetup()
        {
            bank = new Bank("Test Bank");
            individualInvestmentAccount = bank.AddBankAccount("Joe",1000, AccountTypes.IndividualInvestment);
            checkingAccount = bank.AddBankAccount("Jack", 2000, AccountTypes.Checking);
            corporateInvestmentAccount = bank.AddBankAccount("Jill", 3000, AccountTypes.CorporateInvestment);
        }

        [TestMethod]
        public void AccountIdShouldBePopulated()
        {
            Assert.IsNotNull(individualInvestmentAccount.AccountID);
            Assert.IsNotNull(checkingAccount.AccountID);
            Assert.IsNotNull(corporateInvestmentAccount.AccountID);
        }

        [TestMethod]
        public void CheckingBalanceShouldMatchAccountBalance()
        {
            Assert.AreEqual(individualInvestmentAccount.Balance, 1000);
            Assert.AreEqual(checkingAccount.Balance, 2000);
            Assert.AreEqual(corporateInvestmentAccount.Balance, 3000);
        }

        [TestMethod]
        public void DepositingShouldSucceed()
        {
            Assert.IsTrue(individualInvestmentAccount.Deposit(100).Completed);
            Assert.AreEqual(individualInvestmentAccount.Balance, 1100);

            Assert.IsTrue(checkingAccount.Deposit(100).Completed);
            Assert.AreEqual(checkingAccount.Balance, 2100);

            Assert.IsTrue(corporateInvestmentAccount.Deposit(100).Completed);
            Assert.AreEqual(corporateInvestmentAccount.Balance, 3100);
        }

        [TestMethod]
        public void WithdrawingShouldSucceed()
        {
            Assert.IsTrue(individualInvestmentAccount.Withdrawal(100).Completed);
            Assert.AreEqual(individualInvestmentAccount.Balance, 900);

            Assert.IsTrue(checkingAccount.Withdrawal(100).Completed);
            Assert.AreEqual(checkingAccount.Balance, 1900);

            Assert.IsTrue(corporateInvestmentAccount.Withdrawal(100).Completed);
            Assert.AreEqual(corporateInvestmentAccount.Balance, 2900);
        }

        [TestMethod]
        public void WithdrawalLimitShouldBeSet()
        {
            Assert.AreEqual(individualInvestmentAccount.WithdrawalLimit, 500);

            Assert.AreEqual(checkingAccount.WithdrawalLimit, 0);

            Assert.AreEqual(corporateInvestmentAccount.WithdrawalLimit, 0);
        }

        [TestMethod]
        public void WithdrawingMoreThanLimitShouldFail()
        {
            Assert.IsFalse(individualInvestmentAccount.Withdrawal(501).Completed);
        }

        [TestMethod]
        public void WithdrawingMoreThanIndividualLimitShouldSucceed()
        {
            Assert.IsTrue(checkingAccount.Withdrawal(501).Completed);

            Assert.IsTrue(corporateInvestmentAccount.Withdrawal(501).Completed);
        }

        [TestMethod]
        public void OverdraftingShouldFail()
        {
            Assert.IsTrue(individualInvestmentAccount.Withdrawal(500).Completed);
            Assert.IsTrue(individualInvestmentAccount.Withdrawal(500).Completed);
            Assert.IsFalse(individualInvestmentAccount.Withdrawal(500).Completed);
        }
    }
}
