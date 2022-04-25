using DataAccess.Context;
using Projects.DataAccess.Interfaces;
using Projects.DataAccess.Repositories;
using Projects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class TransactionRepository : DBRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(WalletDbContext context)
        : base(context)
        {
        }
    }
}
