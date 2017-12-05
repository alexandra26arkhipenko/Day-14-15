using System;
using BusinessLogic.Interfaces.Interfaces;
using BusinessLogic.ServiceImplementation;
using DataAccessLayer.DataBase;
using DataAccessLayer.Interfaces.IRepository;
using DataAccessLayer.Repository;
using Ninject;

namespace DepandencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolver(this IKernel kernel)
        {
            kernel.Bind<IAccountService>().To<AccountService>();
            //kernel.Bind<IRepository>().To<AccountRepositoryBF>().WithConstructorArgument("account.bin");
            kernel.Bind<IRepository>().To<AccountRepositoryDB>();
            kernel.Bind<IAccountGenerateIdNumber>().To<AccountGenerateIdNumber>().InSingletonScope();
        }
    }
}
