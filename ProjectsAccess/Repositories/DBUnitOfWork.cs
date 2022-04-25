using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Projects.DataAccess.Interfaces;
using System.Threading.Tasks;

namespace Projects.DataAccess.Repositories
{ 
    public class DBUnitOfWork : IUnitOfWork
    {
        public DBUnitOfWork(ITransactionRepository transactions, IIncomeItemRepository incomeItems, IAccountRepository accounts, WalletDbContext context)
        {
            Transactions = transactions;

            IncomeItems = incomeItems;

            Accounts = accounts;

            _context = context;
        }

        private WalletDbContext _context;
        public IAccountRepository Accounts { get; }

        public IIncomeItemRepository IncomeItems { get; }

        public ITransactionRepository Transactions { get; }

        public void Save()
        {
            _context.SaveChanges();
        }
 
        public int Complete()
        {
            _context.SaveChanges();
            return default(int);
        }

        public void Dispose()
        {
            Complete();
        }
    }
}
