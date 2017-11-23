using System.Collections.Generic;

namespace DataAccessLayer.Interfaces.IRepository
{
    public interface IRepository
    {
        void AddAccount(DalAccount account);
        void RemoveAccount(DalAccount account);
        IEnumerable<DalAccount> GetAccounts();
        void UpdateAccount(DalAccount account);
    }
}
