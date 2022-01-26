using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimpleBankProgram.Accounts.Types;

namespace SimpleBankProgram.Accounts
{
    public static class AccountFactory
    {
        public static Account CreateAccount(string owner, decimal balance, AccountTypes accountType)
        {
            //Create base account object
            Account account = new Account(owner, balance);

            //Check to see if account is Individual investment. We need to add Withdrawal limit rule.
            //TODO: create switch for Account Types to add more rules in the future.
            if (accountType == AccountTypes.IndividualInvestment)
            {
                account.WithdrawalLimit = 500;
            }

            return account;
        }
    }
}
