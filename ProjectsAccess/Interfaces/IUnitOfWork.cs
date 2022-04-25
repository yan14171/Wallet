using System;

namespace Projects.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository Accounts { get; }

        IIncomeItemRepository IncomeItems { get; }

        ITransactionRepository Transactions { get; }

        int Complete();
    }
}
