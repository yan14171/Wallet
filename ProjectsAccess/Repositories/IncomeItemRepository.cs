using DataAccess.Context;
using Projects.DataAccess.Interfaces;
using Projects.Entities;

namespace Projects.DataAccess.Repositories
{
    public class IncomeItemRepository : DBRepository<IncomeItem>, IIncomeItemRepository
    {
        public IncomeItemRepository(WalletDbContext context)
        : base(context)
        {
        }
    }
}
