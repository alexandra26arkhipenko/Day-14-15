using System;
using System.Collections.Generic;


namespace DataAccessLayer.Interfaces
{
    public interface IRepository
    {
        void AddAccount(DalAccount account);
        void RemoveAccount(DalAccount account);
        IEnumerable<DalAccount> GetAccounts();
        void UpdateAccount(DalAccount account);
    }
}
