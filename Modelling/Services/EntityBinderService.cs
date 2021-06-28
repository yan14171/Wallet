using Projects.Modelling.DTOs;
using Projects.Modelling.Entities;
using Projects.Modelling.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projects.Modelling.Services
{
    public class EntityBinderService : IEntityBinderService
    {
        public EntityBinderService()
        {

        }

#region public
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
        
        public Project BindProject(ProjectEntity entity)
        {
            return new Project()
            {
                Id = entity.Id,
                AuthorId = entity.Author.Id,
                TeamId = entity.Team.Id,
                Name = entity.Name,
                Description = entity.Description,
                CreatedAt = entity.CreatedAt,
                Deadline = entity.Deadline,
            };
        }

        public Task BindTask(TaskEntity task)
        {
            return new Task()
            {
                Id = task.Id,
                ProjectId = task.ProjectId,
                PerformerId = task.Performer.Id,
                Name = task.Name,
                Description = task.Description,
                CreatedAt = task.CreatedAt,
                FinishedAt = task.FinishedAt,
                State = task.State,
            };
        }

        public Team BindTeam(TeamEntity team)
        {
            return new Team()
            {
                CreatedAt = team.CreatedAt,
                Id = team.Id,
                Name = team.Name
            };
        }

        public User BindUser(UserEntity user)
        {
            return new User()
            {
                Id = user.Id,
                BirthDay = user.BirthDay,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RegisteredAt = user.RegisteredAt,
                TeamId = user.TeamId
            };
        }
        #endregion

        #region private
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

        #endregion
    }
}
