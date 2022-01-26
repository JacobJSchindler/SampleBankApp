using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimpleBankProgram.Accounts.Types;

namespace SimpleBankProgram.Accounts
{
    public class Account
    {
        public Guid AccountID { get;}
        public string Owner { get;}

        public decimal Balance { get; set; }

        public decimal WithdrawalLimit { get; set; } = 0;

        public Account (string owner, decimal balance)
        {
            AccountID = Guid.NewGuid();
            this.Owner = owner;
            this.Balance = balance;
        }

        public GenericResponse Withdrawal(decimal debitAmount)
        {
            GenericResponse response = new GenericResponse();

            if (Balance < debitAmount)
            {
                response.Message = "Insufficient funds";
            }
            else if (debitAmount <= 0)
            {
                response.Message = "Invalid withdrawl amount";
            }
            //Check against withdrawal rule
            else if (WithdrawalLimit > 0 && debitAmount > WithdrawalLimit)
            {
                response.Message = "Withdrawal limit hit";
            }
            else
            {
                Balance -= debitAmount;
                response.Completed = true;
            }

            return response;
        }

        public void Transfer(decimal transferAmount)
        {
            Balance += transferAmount;
        }

        public GenericResponse Deposit(decimal amount)
        {
            GenericResponse response = new GenericResponse();
            if (amount <= 0)
            {
                response.Message = "Invalid deposit amount";
            }
            else
            {
                Balance += amount;
                response.Completed = true;
            }
            return response;
        }
    }
}
