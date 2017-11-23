using System;
using BusinessLogic.Interfaces;
using DataAccessLayer.Interfaces;


namespace BusinessLogic.Mappers
{
    public static class Mapper
    {
        public static Account ToBllAccount(this DalAccount dalAccount) =>
            (Account)Activator.CreateInstance(
                dalAccount.AccountType,
                dalAccount.Id,
                dalAccount.FirstName,
                dalAccount.LastName,
                dalAccount.Amount,
                dalAccount.Points);

        public static DalAccount ToDalAccount(this Account account) =>
            new DalAccount
            {
                AccountType = account.GetType(),
                Points = account.Points,
                Amount = account.Amount,
                Id = account.Id,
                FirstName = account.FirstName,
                LastName = account.LastName
            };
    }


}
