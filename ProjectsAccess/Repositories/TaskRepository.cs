using Projects.DataAccess.Interfaces;
using Projects.Modelling.DTOs;

namespace Projects.DataAccess.Repositories
{
    public class TaskRepository : APIRepository<Task>, ITaskRepository
    {
        public TaskRepository(string APIendpoint)
        : base(APIendpoint)
        {
        }
    }
}
