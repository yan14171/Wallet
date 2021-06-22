using ProjectsAccess.DataAccess.IRepositories;
using ProjectsAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsAccess.Repositories
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
