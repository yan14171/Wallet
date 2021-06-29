using Projects.DTOs;
using Projects.Modelling.DTOs;
using Projects.Modelling.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.API.Interfaces
{
    public interface IQueryProcessingService
    {
        Task<IEnumerable<OldestUsersInfo>> GetOldestTeamsAsync();

        Task<IEnumerable<ProjectInfo>> GetProjectsInfoAsync();

        Task<IEnumerable<KeyValuePair<UserEntity, List<TaskEntity>>>> GetTasksPerPerformerAlphabeticallyAsync();

        Task<IList<TaskEntity>> GetTasksPerPerformerAsync(int performerId);

        Task<IList<TaskInfo>> GetTasksPerPerformerFinishedThisYearAsync(int performerId);

        Task<IEnumerable<KeyValuePair<ProjectEntity, List<TaskEntity>>>> GetTasksQuantityPerProjectAsync(int userId);

        Task<UserInfo> GetUserInfoAsync(int userId);
    }
}