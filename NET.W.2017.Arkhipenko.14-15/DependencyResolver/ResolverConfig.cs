
using BusinessLogic.Interfaces.Interfaces;
using BusinessLogic.ServiceImplementation;
using DataAccessLayer.Interfaces.IRepository;
using DataAccessLayer.Repository;
using Ninject;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolver(this IKernel kernel)
        {
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<IRepository>().To<BinaryFile>().WithConstructorArgument("account.bin");
            kernel.Bind<IAccountGenerateIdNumber>().To<AccountGenerateIdNumber>().InSingletonScope();
        }
    }
}
