using SimpleBankProgram.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimpleBankProgram.Accounts.Types;

namespace SimpleBankProgram
{
    public class Bank
    {
        public string Name { get;}
        public List<Account> Accounts { get;} = new List<Account>();

        public Bank (string Name)
        {
            this.Name = Name;
        }

        public Account AddBankAccount(string owner, decimal startingBalance, AccountTypes accountType)
        {
            Account account = AccountFactory.CreateAccount(owner, startingBalance, accountType);
            Accounts.Add(account);
            return account;
        }

        public Account GetAccount(Guid accountID)
        {
            Account account = Accounts.Where(x => x.AccountID == accountID).FirstOrDefault();

            if (account == null)
            {
                throw new ApplicationException("no account exists");
            }

            return account;
        }

        public GenericResponse TransferFunds(Guid fromAccountID, Guid toAccountID, decimal transferAmount)
        {
            GenericResponse response = new GenericResponse();
            //Check for invalid amount.
            if (transferAmount <= 0)
            {
                response.Message = "invalid transfer amount";
            }
            else
            {
                Account fromAccount = GetAccount(fromAccountID);
                Account toAccount = GetAccount(toAccountID);

                if (fromAccount.Balance < transferAmount)
                {
                    response.Message = "insufficient funds";
                }
                else
                {
                    fromAccount.Transfer(-1 * transferAmount);
                    toAccount.Transfer(transferAmount);
                    response.Completed = true;
                }
            }
            return response;
        }
    }
}
