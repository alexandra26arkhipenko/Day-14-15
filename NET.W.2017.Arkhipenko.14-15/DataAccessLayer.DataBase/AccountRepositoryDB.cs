using System.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using DataAccessLayer.DataBase.Db;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Interfaces.IRepository;

namespace DataAccessLayer.DataBase
{
    public class AccountRepositoryDB : IRepository
    {
        public void AddAccount(DalAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            using (var context = new AccountContext())
            {
                var accountOwner = GetAccountOwner(account);
                context.AccountOwners.Add(accountOwner);
                context.SaveChanges();

                var accountAdd = GetAccount(account, accountOwner);
                context.Accounts.Add(accountAdd);
                context.SaveChanges();
            }
        }

        public void RemoveAccount(DalAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }
            using (var context = new AccountContext())
            {
                var acc = context.Accounts.Include(accHelper => accHelper.AccountOwner)
                    .FirstOrDefault(accHelper => accHelper.AccountId == account.Id);

                var accOwner = acc.AccountOwner;

                context.Accounts.Remove(acc);
                context.AccountOwners.Remove(accOwner);
            }
        }

        public IEnumerable<DalAccount> GetAccounts()
        {
            var accounts = new List<DalAccount>();
            using (var db = new AccountContext())
            {
                foreach (var acc in db.Accounts
                    .Include(account => account.AccountType)
                    .Include(account => account.AccountOwner))
                {
                    accounts.Add(new DalAccount
                    {
                        Id = acc.AccountId,
                        AccountType = acc.AccountType.AccountType1,
                        Email = acc.AccountOwner.Email,
                        LastName = acc.AccountOwner.LastName,
                        FirstName = acc.AccountOwner.FirstName,
                        Points = acc.Points,
                        Amount = acc.Amount
                    });
                }
            }

            return accounts;
        }

        public void UpdateAccount(DalAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            using (var context = new AccountContext())
            {
                var acc = context.Accounts.Include(accHelper => accHelper.AccountOwner)
                    .Include(accHelper => accHelper.AccountType)
                    .FirstOrDefault(acchelper => acchelper.AccountId == account.Id);

                if (!ReferenceEquals(acc, null))
                {
                    acc.Amount = account.Amount;
                    acc.Points = account.Points;
                }
                context.SaveChanges();
            }

        }

        private static Account GetAccount(DalAccount account, AccountOwner accountOwner) =>
            new Account
            {
                AccountType = GetAccountType(account),
                Amount = account.Amount,
                Points = account.Points,
                AccountId = account.Id,
                AccountOwner = accountOwner
            };

        private static AccountOwner GetAccountOwner(DalAccount account) =>
            new AccountOwner
            {
                FirstName = account.FirstName,
                LastName = account.LastName,
                Email = account.Email
            };

        private static AccountType GetAccountType(DalAccount account)
        {
            if (account.AccountType.Contains("Gold"))
            {
                return new AccountType { AccountType1 = "Gold" };
            }

            if (account.AccountType.Contains("Platinum"))
            {
                return new AccountType { AccountType1 = "Platinum" };
            }

            return new AccountType { AccountType1 = "Base" };
        }

    }
}