using Projects.Modelling.DTOs;
using Projects.Modelling.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projects.API.Interfaces
{
    public interface IEntityHandlerService
    {
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