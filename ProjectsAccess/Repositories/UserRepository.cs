using Projects.DataAccess.Interfaces;
using Projects.Modelling.DTOs;

namespace Projects.DataAccess.Repositories
{
    public class UserRepository : APIRepository<User>, IUserRepository
    {
        public UserRepository()
        {

        }

        public UserRepository(string APIendpoint)
        : base(APIendpoint)
        {
        }
    }
}
