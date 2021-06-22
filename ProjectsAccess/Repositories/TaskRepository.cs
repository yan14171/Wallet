using ProjectsAccess.DataAccess.IRepositories;
using ProjectsAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsAccess.Repositories
{
    public class TaskRepository : APIRepository<ProjectsAccess.Models.Task>, ITaskRepository
    {
        public TaskRepository()
        {

        }
        public TaskRepository(string APIendpoint)
        : base(APIendpoint)
        {
        }
    }
}
