using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Interfaces.IRepository;


namespace DataAccessLayer.Repository
{
    public class AccountRepositoryBF : IRepository
    {
        #region private fields
        private readonly string _path;
        private readonly List<DalAccount> _accounts = new List<DalAccount>();
        #endregion

        #region ctor
        public AccountRepositoryBF(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Incorrect");
            }

            _path = path;

        }
        #endregion

        #region public
        /// <summary>
        /// add account to file
        /// </summary>
        /// <param name="account">account that we add</param>
        public void AddAccount(DalAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }
            AppendAccountToFile(account);
            _accounts.Add(account);
        }

        /// <summary>
        /// Remove account from file
        /// </summary>
        /// <param name="account">account that is removing</param>
        public void RemoveAccount(DalAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            _accounts.Remove(account);
            AppendAccountsToFile(_accounts);
        }

        /// <summary>
        /// returns all accounts from file
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DalAccount> GetAccounts()
        {
            List<DalAccount> accounts = new List<DalAccount>();
            using (var br = new BinaryReader(File.Open(_path, FileMode.OpenOrCreate,
                FileAccess.Read, FileShare.Read)))
            {
                while (br.BaseStream.Position != br.BaseStream.Length)
                {
                    var account = Reader(br);
                    accounts.Add(account);
                }
            }

            return accounts;
        }

        /// <summary>
        /// overwriting file, update account
        /// </summary>
        /// <param name="account">account that is updating</param>
        public void UpdateAccount(DalAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            _accounts.Remove(account);
            _accounts.Add(account);
            AppendAccountsToFile(_accounts);
        }
        #endregion

        #region private
        private void AppendAccountToFile(DalAccount account)
        {
            using (var bw = new BinaryWriter(File.Open(_path, FileMode.Append,
                FileAccess.Write, FileShare.None), Encoding.UTF8, false))
            {
                Writer(bw, account);
            }
        }

        private void AppendAccountsToFile(List<DalAccount> accounts)
        {
            using (var bw = new BinaryWriter(File.Open(_path, FileMode.Create,
                FileAccess.Write, FileShare.None)))
            {
                foreach (var account in accounts)
                {
                    Writer(bw, account);
                }
            }
        }

        private static void Writer(BinaryWriter binary, DalAccount account)
        {
            binary.Write(account.Id);
            binary.Write(account.FirstName);
            binary.Write(account.LastName);
            binary.Write(account.Amount);
            binary.Write(account.Points);
        }

        private static DalAccount Reader(BinaryReader binary)
        {
            var id = binary.ReadString();
            var firstName = binary.ReadString();
            var lastName = binary.ReadString();
            var amount = binary.ReadDecimal();
            var points = binary.ReadInt32();

            return new DalAccount
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Amount = amount,
                Points = points
            };
        }
#endregion
    }
}
