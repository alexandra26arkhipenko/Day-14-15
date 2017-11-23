using System;
using System.Collections.Generic;
using BusinessLogic.Interfaces.Entities;

namespace BusinessLogic.Interfaces.Interfaces
{
    public interface IAccountService
    {
        void AddMoney(Account account, decimal money);
        void DivMoney(Account account, decimal money);
        void CloseAccout(Account account);
        //IEnumerable<Account> GetAllAccounts();
        Account CreateAccount(AccountType accountType, string firstName, string lastName, decimal amount);
    }
}
