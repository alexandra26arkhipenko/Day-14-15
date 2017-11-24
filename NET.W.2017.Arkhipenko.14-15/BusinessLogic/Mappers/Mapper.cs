using System;
using BusinessLogic.Interfaces.Entities;
using DataAccessLayer.Interfaces;


namespace BusinessLogic.Mappers
{
    public static class Mapper
    {
        /// <summary>
        /// Transform DalAccount to Account
        /// </summary>
        /// <param name="dalAccount"></param>
        /// <returns>Instance of Account</returns>
        public static Account ConvertToAccount(this DalAccount dalAccount) =>
            (Account)Activator.CreateInstance(
                dalAccount.AccountType,
                dalAccount.Id,
                dalAccount.FirstName,
                dalAccount.LastName,
                dalAccount.Amount,
                dalAccount.Points);

        /// <summary>
        /// Transform Account to DalAccount
        /// </summary>
        /// <param name="account"></param>
        /// <returns>Instance of DalAccount</returns>
        public static DalAccount ConvertToDalAccount(this Account account) =>
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
