using System;
using BusinessLogic.Interfaces.Interfaces;
using DepandencyResolver;
using Ninject;


namespace ConsolePL
{
    class Program
    {
        
        private static IKernel NinjectKernel;


        static Program()
        {
            NinjectKernel = new StandardKernel();
            NinjectKernel.ConfigurateResolver();
        }
        public static void Main()
        {
            IAccountGenerateIdNumber accountGenerateIdNumber;
            var accountService = NinjectKernel.Get<IAccountService>();
            var account1 = accountService.CreateAccount(AccountType.Base, "Alex", "Smith", 100, "alex2568@gmail.com");
            Console.WriteLine("Account1 id: " + account1);
            accountService.AddMoney(account1, 50);
            Console.WriteLine("Account1 id: " + account1);

            Console.WriteLine(new string('_', 50));
            var account2 = accountService.CreateAccount(AccountType.Gold, "Maria", "Gruz", 500, "masha25478@gmail.com");
            Console.WriteLine("Account1 id: " + account2);
            
            accountService.DivMoney(account2, 40);
            Console.WriteLine("Account1 id: " + account2);


            Console.WriteLine(new string('_', 50));
            var account3 = accountService.CreateAccount(AccountType.Platinum, "Patric", "Gomer", 500, "gomerfgj68@gmail.com");
            Console.WriteLine("Account1 id: " + account3);

            accountService.DivMoney(account3, 70);
            Console.WriteLine("Account1 id: " + account3);

            Console.ReadKey();
        }
    }
}
