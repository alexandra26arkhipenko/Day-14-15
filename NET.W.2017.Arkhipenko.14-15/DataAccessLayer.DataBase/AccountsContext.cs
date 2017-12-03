using System.Data.Entity;

namespace DataAccessLayer.DataBase
{
    public class AccountsContext : DbContext
    {

        public AccountsContext() : base("Accounts")
        {

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountOwner> AccountOwners { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
    }
}
}