using System;
using System.Linq;
using BusinessLogic.Interfaces.Entities;
using BusinessLogic.Interfaces.Interfaces;
using BusinessLogic.Mappers;
using DataAccessLayer.Interfaces.IRepository;

namespace BusinessLogic.ServiceImplementation
{
    public class AccountService : IAccountService
    {
        #region private fields 
        private const int BaseAccountBonusValue = 1;
        private const int GoldAccountBonusValue = 50;
        private const int PlatinumAccountBonusValue = 1000;

        private readonly IAccountGenerateIdNumber _accountGenerateIdNumber;
        private readonly IRepository _accountRepsitory;

        #endregion

        #region ctor
        public AccountService(IRepository accountRepsitory, IAccountGenerateIdNumber accountGenerateIdNumber)
        {
            _accountRepsitory = accountRepsitory;
            _accountGenerateIdNumber = accountGenerateIdNumber;
        }
        #endregion

        #region public
        /// <summary>
        /// Add money to accout's amount and add bonus points
        /// </summary>
        /// <param name="account"> account in which the balance is replenished. </param>
        /// <param name="money"> money that we add</param>
        public void AddMoney(Account account, decimal money)
        {
            if (ReferenceEquals(account, null))
                throw new ArgumentException(nameof(account));
            account.AddMoney(money);
            _accountRepsitory.UpdateAccount(account.ConvertToDalAccount());
        }

        public void AddMoney(string accountId, decimal money)
        {
            if (string.IsNullOrEmpty(accountId))
                throw new ArgumentException(nameof(accountId));
            var account = _accountRepsitory.GetAccounts().FirstOrDefault(acc => acc.Id == accountId);
            account.ConvertToAccount().AddMoney(money);
            _accountRepsitory.UpdateAccount(account);
        }

        /// <summary>
        /// Withdraw money from accout's amount and add bonus points
        /// </summary>
        /// <param name="account">account from which we withdraw money.</param>
        /// <param name="money">money that we withdraw</param>
        public void DivMoney(Account account, decimal money)
        {
            if (ReferenceEquals(account, null))
                throw new ArgumentException(nameof(account));
            account.DivMoney(money);
            _accountRepsitory.UpdateAccount(account.ConvertToDalAccount());
        }

        public void DivMoney(string accountId, decimal money)
        {
            if (string.IsNullOrEmpty(accountId))
                throw new ArgumentException(nameof(accountId));
            var account = _accountRepsitory.GetAccounts().FirstOrDefault(acc => acc.Id == accountId);
            account.ConvertToAccount().DivMoney(money);
            _accountRepsitory.UpdateAccount(account);
        }


        /// <summary>
        /// Remove account 
        /// </summary>
        /// <param name="account">account that is removing</param>
        public void CloseAccout(Account account)
        {
            if (ReferenceEquals(account, null))
                throw new ArgumentException(nameof(account));
            _accountRepsitory.RemoveAccount(account.ConvertToDalAccount());
        }

        public void CloseAccout(string accountId)
        {
            if (string.IsNullOrEmpty(accountId))
                throw new ArgumentException(nameof(accountId));
            var account = _accountRepsitory.GetAccounts().FirstOrDefault(acc => acc.Id == accountId);
            _accountRepsitory.RemoveAccount(account);
        }

        public string GetAccount(string accountId)
        {
            if (string.IsNullOrWhiteSpace(accountId))
            {
                throw new ArgumentException(nameof(accountId));
            }

            var accounts = _accountRepsitory.GetAccounts();
            var account = accounts.FirstOrDefault(acc => acc.Id == accountId);
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentException($"Account with id {accountId} not found");
            }

            return account.ConvertToAccount().ToString();
        }

        /// <summary>
        /// Create new account
        /// </summary>
        /// <param name="accountType">Type of account : Base, Gold, Platinum</param>
        /// <param name="firstName"> owner name</param>
        /// <param name="lastName">owner lastname</param>
        /// <param name="amount"></param>
        /// <returns>new account</returns>
        public Account CreateAccount(AccountType accountType, string firstName, string lastName, decimal amount, string email)
        {
            var account = CreateAccount(TypeOfAccount(accountType), _accountGenerateIdNumber.GenerateId(), firstName,
                lastName, amount,  GetBonuses(accountType), email);

            _accountRepsitory.AddAccount(account.ConvertToDalAccount());
            return account;
        }

        public Account CreateAccount(AccountType accountType, string firstName, string lastName, decimal amount, string email, IAccountGenerateIdNumber accountGenerateIdNumberNotField)
        {
            var account = CreateAccount(TypeOfAccount(accountType), accountGenerateIdNumberNotField.GenerateId(), firstName,
                lastName, amount,  GetBonuses(accountType), email);

            _accountRepsitory.AddAccount(account.ConvertToDalAccount());
            return account;
        }
        #endregion

        #region private 
        private Account CreateAccount(Type accountType, string id, string firstName, string lastName, decimal amount,
            int bonusPoints, string email)
        {
            return (Account) Activator.CreateInstance(accountType, id, firstName, lastName, amount, bonusPoints, email);
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
#endregion
    }
}