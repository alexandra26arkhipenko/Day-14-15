using System.Data.Entity;

namespace DataAccessLayer.DataBase
{
    public class AccounContext : DbContext
    {
        public AccounContext() : base("Accounts")
        {
            
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountOwner> AccountOwners { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
    }
}