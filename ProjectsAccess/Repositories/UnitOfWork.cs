using ProjectsAccess.DataAccess.IRepositories;
using ProjectsAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsAccess.Repositories
{ 

    //maybe rename to APIUnitOfWork to emphasize the concrete API implementation
    public class APIUnitOfWork : IUnitOfWork
    {
        public APIUnitOfWork(IProjectRepository projects, ITaskRepository tasks, ITeamRepository teams, IUserRepository users)
        {
            Projects = projects;

            Tasks = tasks;

            Teams = teams;

            Users = users;
        }
        
        public IProjectRepository Projects { get; }

        public ITaskRepository Tasks { get; }
        
        public ITeamRepository Teams { get; }
        
        public IUserRepository Users { get; }

        public int Complete()
        {
            return default(int);
        }

        public void Dispose()
        {

        }
    }
}
