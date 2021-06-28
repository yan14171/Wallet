
using System.Collections.Generic;

namespace Projects.Modelling.Interfaces
{
    public interface IEntityBinderService
    {
        IEnumerable<ProjectEntity> BindProjectEntities();
        IEnumerable<TaskEntity> BindTaskEntities();
        IEnumerable<TeamEntity> BindTeamEntities();
        IEnumerable<UserEntity> BindUserEntities();
    }
}