using System;
using BusinessLogic.Interfaces.Entities;
using BusinessLogic.Interfaces.Interfaces;
using BusinessLogic.Mappers;
using DataAccessLayer.Interfaces.IRepository;

namespace BusinessLogic.ServiceImplementation
{
    public class AccountService : IAccountService
    {
        private const int BaseAccountBonusValue = 1;
        private const int GoldAccountBonusValue = 50;
        private const int PlatinumAccountBonusValue = 100;

        private readonly IAccountGenerateIdNumber _accountGenerateIdNumber;
        private readonly IRepository _accountRepsitory;

        public AccountService(IRepository accountRepsitory, IAccountGenerateIdNumber accountGenerateIdNumber)
        {
            _accountRepsitory = accountRepsitory;
            _accountGenerateIdNumber = accountGenerateIdNumber;
        }


        public void AddMoney(Account account, decimal money)
        {
            if (ReferenceEquals(account, null))
                throw new ArgumentException(nameof(account));
            account.AddMoney(money);
            _accountRepsitory.UpdateAccount(account.ConvertToDalAccount());
        }

        public void DivMoney(Account account, decimal money)
        {
            if (ReferenceEquals(account, null))
                throw new ArgumentException(nameof(account));
            account.DivMoney(money);
            _accountRepsitory.UpdateAccount(account.ConvertToDalAccount());
        }

        public void CloseAccout(Account account)
        {
            if (ReferenceEquals(account, null))
                throw new ArgumentException(nameof(account));
            _accountRepsitory.RemoveAccount(account.ConvertToDalAccount());
        }

        public Account CreateAccount(AccountType accountType, string firstName, string lastName, decimal amount)
        {
            var account = CreateAccount(TypeOfAccount(accountType), _accountGenerateIdNumber.GenerateId(), firstName,
                lastName, amount, GetBonuses(accountType));

            _accountRepsitory.AddAccount(account.ConvertToDalAccount());
            return account;
        }

        private Account CreateAccount(Type accountType, string id, string firstName, string lastName, decimal amount,
            int bonusPoints)
        {
            return (Account) Activator.CreateInstance(accountType, id, firstName, lastName, amount, bonusPoints);
        }

        private static int GetBonuses(AccountType accountType)
        {
            switch (accountType)
            {
                case AccountType.Base:
                    return BaseAccountBonusValue;
                case AccountType.Gold:
                    return GoldAccountBonusValue;
                case AccountType.Platinum:
                    return PlatinumAccountBonusValue;
                default:
                    throw new ArgumentException(nameof(accountType));
            }
        }

        private static Type TypeOfAccount(AccountType accountType)
        {
            switch (accountType)
            {
                case AccountType.Base:
                    return typeof(BaseAccount);
                case AccountType.Gold:
                    return typeof(GoldAccount);
                case AccountType.Platinum:
                    return typeof(PlatinumAccount);
                default:
                    throw new ArgumentException(nameof(accountType));
            }
        }
    }
}