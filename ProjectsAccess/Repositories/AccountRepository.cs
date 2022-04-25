using DataAccess.Context;
using Projects.DataAccess.Interfaces;
using Projects.Entities;

namespace Projects.DataAccess.Repositories
{
    public class AccountRepository : DBRepository<Account>, IAccountRepository
    {
        public AccountRepository(WalletDbContext context)
        :base(context)
        {
        }
    }
}
