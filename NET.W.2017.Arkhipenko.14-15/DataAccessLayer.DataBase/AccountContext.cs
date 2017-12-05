using System.Data.Entity;
using DataAccessLayer.DataBase.Db;


namespace DataAccessLayer.DataBase
{
   public class AccountContext : DbContext
   {
       public AccountContext() : base("Accounts")
       { }

       public DbSet<Account> Accounts { get; set; }
       public DbSet<AccountOwner> AccountOwners { get; set; }
       public DbSet<AccountType> AccountTypes { get; set; }

   }
        
}