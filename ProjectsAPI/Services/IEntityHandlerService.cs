using Projects.Modelling.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projects.API.Services
{
    public interface IEntityHandlerService
    {
        bool AddProject(ProjectEntity project);
        bool AddTask(TaskEntity task);
        bool AddTeam(TeamEntity team);
        bool AddUser(UserEntity user);
        bool DeleteProjectById(int id);
        bool DeleteTaskById(int id);
        bool DeleteTeamById(int id);
        bool DeleteUserById(int id);
        Task<IEnumerable<ProjectEntity>> GetAllProjectEntitiesAsync();
        Task<IEnumerable<TaskEntity>> GetAllTaskEntitiesAsync();
        Task<IEnumerable<TeamEntity>> GetAllTeamEntitiesAsync();
        Task<IEnumerable<UserEntity>> GetAllUserEntitiesAsync();
        Task<ProjectEntity> GetProjectEntitybyIdAsync(int id);
        Task<TaskEntity> GetTaskEntitybyIdAsync(int id);
        Task<TeamEntity> GetTeamEntitybyIdAsync(int id);
        Task<UserEntity> GetUserEntitybyIdAsync(int id);
    }
}