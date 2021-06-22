using ProjectsAccess.Entities;
using System;
using System.Collections.Generic;

namespace ProjectsAccess.Services
{
    public interface IEntityBinderService
    {
        IEnumerable<ProjectEntity> BindProjectEntities();
        IEnumerable<TaskEntity> BindTaskEntities();
        IEnumerable<TeamEntity> BindTeamEntities();
        IEnumerable<UserEntity> BindUserEntities();
    }
}