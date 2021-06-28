using Projects.DataAccess.Interfaces;
using Projects.Modelling.DTOs;

namespace Projects.DataAccess.Repositories
{
    public class ProjectRepository : APIRepository<Project>, IProjectRepository
    {
        public ProjectRepository(string APIendpoint)
        :base(APIendpoint)
        {
        }
    }
}
