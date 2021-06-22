using ProjectsAccess.DataAccess.IRepositories;
using ProjectsAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsAccess.Repositories
{
    public class TeamRepository : APIRepository<Team>, ITeamRepository
    {
        public TeamRepository()
        {

        }
        public TeamRepository(string APIendpoint)
        : base(APIendpoint)
        {
        }
    }
}
