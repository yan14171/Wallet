using ProjectsAccess.DataAccess.IRepositories;
using ProjectsAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsAccess.Repositories
{
    public class ProjectRepository : APIRepository<Project>, IProjectRepository
    {
        public ProjectRepository()
        {

        }
        public ProjectRepository(string APIendpoint)
        :base(APIendpoint)
        {
        }
    }
}
