using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankProgram.Accounts
{
    public static class Types
    {
        public enum AccountTypes
        {
            Checking,
            IndividualInvestment,
            CorporateInvestment
        }

        public enum TransactionTypes
        {
            Withdrawal, //Debit
            Deposit //Credit
        }
    }
}
