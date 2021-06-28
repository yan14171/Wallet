using Projects.Modelling.DTOs;
using Projects.Modelling.Entities;
using Projects.Modelling.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Projects.Modelling.Services
{
    public class EntityBinderService : IEntityBinderService
    {
        public IEnumerable<UserEntity> BindUserEntities(IEnumerable<User> userModels)
        {
            IEnumerable<UserEntity> userEntities = CastToUserEntities(userModels);

            return userEntities;
        }

        public IEnumerable<TeamEntity> BindTeamEntities(IEnumerable<Team> teamModels , IEnumerable<User> userModels)
        {
            IEnumerable<UserEntity> userEntities = CastToUserEntities(userModels);

            IEnumerable<TeamEntity> teamEntities = CastToTeamEntities(teamModels, userEntities);

            return teamEntities;
        }

        public IEnumerable<TaskEntity> BindTaskEntities(IEnumerable<DTOs.Task> taskModels , IEnumerable<User> userModels)
        {
            IEnumerable<UserEntity> userEntities = CastToUserEntities(userModels);

            IEnumerable<TaskEntity> taskEntities = CastToTaskEntities(taskModels, userEntities);

            return taskEntities;
        }

        public IEnumerable<ProjectEntity> BindProjectEntities(IEnumerable<Project> projectModels,
                                                              IEnumerable<DTOs.Task> taskModels,
                                                              IEnumerable<User> userModels,
                                                              IEnumerable<Team> teamModels)
        {
            IEnumerable<UserEntity> userEntities = CastToUserEntities(userModels);

            IEnumerable<TaskEntity> taskEntities = CastToTaskEntities(taskModels, userEntities);

            IEnumerable<TeamEntity> teamEntities = CastToTeamEntities(teamModels, userEntities);

            IEnumerable<ProjectEntity> projectEntities = CastToProjectEntities(projectModels, userEntities, taskEntities, teamEntities);

            return projectEntities;
        }

        private IEnumerable<ProjectEntity> CastToProjectEntities(
            IEnumerable<Project> projectModels,
            IEnumerable<UserEntity> userEntities,
            IEnumerable<TaskEntity> taskEntities,
            IEnumerable<TeamEntity> teamEntities)
        {
            return projectModels.GroupJoin(
                        taskEntities,
                        project => project.Id,
                        task => task.ProjectId,
                        (project, tasks) =>
                        new
                        {
                            Tasks = tasks,
                            TeamId = project.TeamId,
                            AuthorId = project.AuthorId,

                            projModel = project
                        })
                        .Join(
                        userEntities,
                        project => project.AuthorId,
                        user => user.Id,
                        (project, user) =>
                        new
                        {
                            Author = user,
                            TeamId = project.TeamId,
                            Tasks = project.Tasks,

                            projModel = project.projModel
                        })
                        .Join(
                        teamEntities,
                        project => project.TeamId,
                        team => team.Id,
                        (project, team) =>
                        new ProjectEntity(project.projModel, project.Author, team, project.Tasks));
        }

        private IEnumerable<TeamEntity> CastToTeamEntities(
            IEnumerable<Team> teamModels,
            IEnumerable<UserEntity> userEntities)
        {
            return teamModels.GroupJoin(
                userEntities,
                team => team.Id,
                user => user.TeamId,
                (team, user) =>
                new TeamEntity(team, user));
        }

        private IEnumerable<TaskEntity> CastToTaskEntities(
            IEnumerable<DTOs.Task> taskModels,
            IEnumerable<UserEntity> performerEntities)
        {
            return taskModels.Join(
                             performerEntities,
                             task => task.PerformerId,
                             user => user.Id,
                             (task, user) =>
                             new TaskEntity(task, user));
        }

        private IEnumerable<UserEntity> CastToUserEntities(
            IEnumerable<User> userModels)
        {
            return userModels.Select(
                n => new UserEntity(n));
        }
    }
}
