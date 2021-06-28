using ProjectsAccess.DataAccess.IRepositories;
using ProjectsAccess.Entities;
using ProjectsAccess.Models;
using ProjectsAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Modelling.Services
{
    public class EntityBinderService : IEntityBinderService
    {
        private readonly IUnitOfWork unitOfWork;

        IEnumerable<Project> projectModels;

        IEnumerable<ProjectsAccess.Models.Task> taskModels;

        IEnumerable<Team> teamModels;

        IEnumerable<User> userModels;

        public EntityBinderService(IUnitOfWork unitOfWork)
        {  
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<UserEntity> BindUserEntities()
        {
            userModels = unitOfWork.Users.GetAll();

            IEnumerable<UserEntity> userEntities = CastToUserEntities(userModels);

            return userEntities;
        }

        public async Task<IEnumerable<UserEntity>> BindUserEntitiesAsync()
        {
            userModels = await (unitOfWork.Users as UserRepository).GetAllAsync();

            IEnumerable<UserEntity> userEntities = CastToUserEntities(userModels);

            return userEntities;
        }

        public IEnumerable<TeamEntity> BindTeamEntities()
        {
            teamModels = unitOfWork.Teams.GetAll();
            userModels = unitOfWork.Users.GetAll();

            IEnumerable<UserEntity> userEntities = CastToUserEntities(userModels);

            IEnumerable<TeamEntity> teamEntities = CastToTeamEntities(teamModels, userEntities);

            return teamEntities;
        }

        public async Task<IEnumerable<TeamEntity>> BindTeamEntitiesAsync()
        {
            var teammodelsTask =
            System.Threading.Tasks.Task.Run(
                   () => (unitOfWork.Teams as TeamRepository).GetAllAsync());
            var usermodelsTask =
            System.Threading.Tasks.Task.Run(
                 () => (unitOfWork.Users as UserRepository).GetAllAsync());

            await System.Threading.Tasks.Task.WhenAll(teammodelsTask, usermodelsTask);

            IEnumerable<UserEntity> userEntities = CastToUserEntities(userModels = usermodelsTask.Result);

            IEnumerable<TeamEntity> teamEntities = CastToTeamEntities(teamModels = teammodelsTask.Result, userEntities);

            return teamEntities;
        }

        public IEnumerable<TaskEntity> BindTaskEntities()
        {
            taskModels = unitOfWork.Tasks.GetAll();
            userModels = unitOfWork.Users.GetAll();

            IEnumerable<UserEntity> userEntities = CastToUserEntities(userModels);

            IEnumerable<TaskEntity> taskEntities = CastToTaskEntities(taskModels, userEntities);

            return taskEntities;
        }

        public async Task<IEnumerable<TaskEntity>> BindTaskEntitiesAsync()
        {
            var taskmodelsTask =
             System.Threading.Tasks.Task.Run(
                    () => (unitOfWork.Tasks as TaskRepository).GetAllAsync());
            var usermodelsTask =
            System.Threading.Tasks.Task.Run(
                 () => (unitOfWork.Users as UserRepository).GetAllAsync());

            await System.Threading.Tasks.Task.WhenAll(taskmodelsTask, usermodelsTask);

            IEnumerable<UserEntity> userEntities = CastToUserEntities(userModels = usermodelsTask.Result);

            IEnumerable<TaskEntity> taskEntities = CastToTaskEntities(taskModels = taskmodelsTask.Result, userEntities);

            return taskEntities;
        }

        public IEnumerable<ProjectEntity> BindProjectEntities()
        {
            projectModels = unitOfWork.Projects.GetAll();
            taskModels = unitOfWork.Tasks.GetAll();
            userModels = unitOfWork.Users.GetAll();
            teamModels = unitOfWork.Teams.GetAll();

            IEnumerable<UserEntity> userEntities = CastToUserEntities(userModels);

            IEnumerable<TaskEntity> taskEntities = CastToTaskEntities(taskModels, userEntities);

            IEnumerable<TeamEntity> teamEntities = CastToTeamEntities(teamModels, userEntities);

            IEnumerable<ProjectEntity> projectEntities = CastToProjectEntities(projectModels, userEntities, taskEntities, teamEntities);

            return projectEntities;
        }

        public async Task<IEnumerable<ProjectEntity>> BindProjectEntitiesAsync()
        {
            var taskmodelsTask =
            System.Threading.Tasks.Task.Run(
                () => (unitOfWork.Tasks as TaskRepository).GetAllAsync());

            var usermodelsTask =
            System.Threading.Tasks.Task.Run(
                 () => (unitOfWork.Users as UserRepository).GetAllAsync());

            var teammodelsTask =
            System.Threading.Tasks.Task.Run(
              () => (unitOfWork.Teams as TeamRepository).GetAllAsync());

            var projectmodelsTask =
            System.Threading.Tasks.Task.Run(
                 () => (unitOfWork.Projects as ProjectRepository).GetAllAsync());

            await System.Threading.Tasks.Task.WhenAll(taskmodelsTask, usermodelsTask, teammodelsTask, projectmodelsTask);

            IEnumerable<UserEntity> userEntities = CastToUserEntities(userModels = usermodelsTask.Result);

            IEnumerable<TaskEntity> taskEntities = CastToTaskEntities(taskModels = taskmodelsTask.Result, userEntities);

            IEnumerable<TeamEntity> teamEntities = CastToTeamEntities(teamModels = teammodelsTask.Result, userEntities);

            IEnumerable<ProjectEntity> projectEntities = CastToProjectEntities(projectModels = projectmodelsTask.Result, userEntities, taskEntities, teamEntities);

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
            IEnumerable<ProjectsAccess.Models.Task> taskModels,
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
