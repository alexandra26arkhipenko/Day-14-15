using System;
using BusinessLogic.Interfaces.Interfaces;
using DependencyResolver;
using Ninject;


namespace ConsolePL
{
    class Program
    {
        private static readonly IKernel NinjectKernel;

        static Program()
        {
            NinjectKernel = new StandardKernel();
            NinjectKernel.ConfigurateResolver();
        }
        static void Main()
        {
            var accountService = NinjectKernel.Get<IAccountService>();
            var account1 = accountService.CreateAccount(AccountType.Base, "Alex", "Smith", 100);
            Console.WriteLine("Account1 id: " + account1);
            accountService.AddMoney(account1, 50);
            Console.WriteLine("Account1 id: " + account1);

            Console.WriteLine(new string('_', 50));
            var account2 = accountService.CreateAccount(AccountType.Gold, "Maria", "Gruz", 50);
            Console.WriteLine("Account1 id: " + account2);
            
            accountService.DivMoney(account2, 40);
            Console.WriteLine("Account1 id: " + account2);


            Console.WriteLine(new string('_', 50));
            var account3 = accountService.CreateAccount(AccountType.Platinum, "Patric", "Gomer", 500);
            Console.WriteLine("Account1 id: " + account3);

            accountService.DivMoney(account2, 70);
            Console.WriteLine("Account1 id: " + account3);

            Console.ReadKey();
        }
    }
}
