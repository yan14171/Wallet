using Autofac;
using DataAccess.Context;
using DataAccess.Repositories;
using Projects.DataAccess.Interfaces;
using Projects.DataAccess.Repositories; 

namespace BusinessLogicLeve
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.Register(c => new WalletDbContextFactory().CreateDbContext())
                .As<WalletDbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TransactionRepository>()
                .As<ITransactionRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AccountRepository>()
                .As<IAccountRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<IncomeItemRepository>()
                .As<IIncomeItemRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DBUnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<BankService>()
                .AsSelf()
                .InstancePerLifetimeScope();

            return builder.Build();
        }
    }
}
