using Projects.Modelling.DTOs;
using Projects.Modelling.Entities;
using System.Collections.Generic;

namespace Projects.Modelling.Interfaces
{
    public interface IEntityBinderService
    {
        IEnumerable<ProjectEntity> BindProjectEntities(IEnumerable<Project> projectModels,
                                                              IEnumerable<DTOs.Task> taskModels,
                                                              IEnumerable<User> userModels,
                                                              IEnumerable<Team> teamModels);
        IEnumerable<TaskEntity> BindTaskEntities(IEnumerable<DTOs.Task> taskModels, IEnumerable<User> userModels);
        IEnumerable<TeamEntity> BindTeamEntities(IEnumerable<Team> teamModels, IEnumerable<User> userModels);
        IEnumerable<UserEntity> BindUserEntities(IEnumerable<User> userModels);
    }
}