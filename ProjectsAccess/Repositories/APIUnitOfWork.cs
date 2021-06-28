using Projects.DataAccess.Interfaces;

namespace Projects.DataAccess.Repositories
{ 
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
