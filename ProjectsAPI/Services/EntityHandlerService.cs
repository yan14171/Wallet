using Projects.API.Interfaces;
using Projects.DataAccess.Interfaces;
using Projects.DataAccess.Repositories;
using Projects.Modelling.Entities;
using Projects.Modelling.Interfaces;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Projects.Modelling.Services;

namespace Projects.API.Services
{
    public class EntityHandlerService : IEntityHandlerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IEntityBinderService binder;

        public EntityHandlerService(IUnitOfWork unitOfWork, IEntityBinderService binder)
        {
            this.unitOfWork = unitOfWork;
            this.binder = binder;
        }

        public async Task<IEnumerable<ProjectEntity>> GetAllProjectEntitiesAsync()
        {
            var projectModelsTask = (unitOfWork.Projects as ProjectRepository).GetAllAsync();
            var usersModelsTask = (unitOfWork.Users as UserRepository).GetAllAsync();
            var tasksModelsTask = (unitOfWork.Tasks as TaskRepository).GetAllAsync();
            var teamsModelsTask = (unitOfWork.Teams as TeamRepository).GetAllAsync();

            await Task.WhenAll(projectModelsTask,
                               usersModelsTask,
                               tasksModelsTask,
                               teamsModelsTask);

            return binder.BindProjectEntities(
                projectModels: projectModelsTask.Result,
                userModels: usersModelsTask.Result,
                taskModels: tasksModelsTask.Result,
                teamModels: teamsModelsTask.Result);
        }

        public async Task<ProjectEntity> GetProjectEntitybyIdAsync(int id)
        {
            var projects = await GetAllProjectEntitiesAsync();

            return projects.Where(n => n.Id == id)
                .FirstOrDefault();
        }

        public async Task<IEnumerable<TaskEntity>> GetAllTaskEntitiesAsync()
        {
            var usersModelsTask = (unitOfWork.Users as UserRepository).GetAllAsync();
            var tasksModelsTask = (unitOfWork.Tasks as TaskRepository).GetAllAsync();

            await Task.WhenAll(
                               usersModelsTask,
                               tasksModelsTask
                               );

            return binder.BindTaskEntities(
                userModels: usersModelsTask.Result,
                taskModels: tasksModelsTask.Result
                );
        }

        public async Task<TaskEntity> GetTaskEntitybyIdAsync(int id)
        {
            var tasks = await GetAllTaskEntitiesAsync();

            return tasks.Where(n => n.Id == id)
                .FirstOrDefault();
        }

        public async Task<IEnumerable<UserEntity>> GetAllUserEntitiesAsync()
        {
            var usersModels = await (unitOfWork.Users as UserRepository).GetAllAsync();

            return binder.BindUserEntities(
                userModels: usersModels
                );
        }

        public async Task<UserEntity> GetUserEntitybyIdAsync(int id)
        {
            var tasks = await GetAllUserEntitiesAsync();

            return tasks.Where(n => n.Id == id)
                .FirstOrDefault();
        }

        public async Task<IEnumerable<TeamEntity>> GetAllTeamEntitiesAsync()
        {
            var usersModelsTask = (unitOfWork.Users as UserRepository).GetAllAsync();
            var teamsModelsTask = (unitOfWork.Tasks as TeamRepository).GetAllAsync();

            await Task.WhenAll(
                               usersModelsTask,
                               teamsModelsTask
                               );

            return binder.BindTeamEntities(
                userModels: usersModelsTask.Result,
                teamModels: teamsModelsTask.Result
                );
        }

        public async Task<TeamEntity> GetTeamEntitybyIdAsync(int id)
        {
            var tasks = await GetAllTeamEntitiesAsync();

            return tasks.Where(n => n.Id == id)
                .FirstOrDefault();
        }

        public bool AddTask(TaskEntity task)
        {
            var taskModel = (binder as EntityBinderService).BindTask(task);

            (unitOfWork.Tasks as TaskRepository).Add(taskModel);

            return true;
        }

        public bool AddUser(UserEntity user)
        {
            var userModel = (binder as EntityBinderService).BindUser(user);

            (unitOfWork.Users as UserRepository).Add(userModel);

            return true;
        }

        public bool AddTeam(TeamEntity team)
        {
            var teamModel = (binder as EntityBinderService).BindTeam(team);

            (unitOfWork.Teams as TeamRepository).Add(teamModel);

            return true;
        }

        public bool AddProject(ProjectEntity project)
        {
            var projectModel = (binder as EntityBinderService).BindProject(project);

            (unitOfWork.Projects as ProjectRepository).Add(projectModel);

            return true;
        }

        public bool DeleteProjectById(int id)
        {
            if ((unitOfWork.Projects as ProjectRepository).Count() < id - 1)
                return false;

            (unitOfWork.Projects as ProjectRepository).DeleteAt(id);

            return true;
        }

        public bool DeleteTaskById(int id)
        {
            if ((unitOfWork.Tasks as TaskRepository).Count() < id - 1)
                return false;

            (unitOfWork.Tasks as TaskRepository).DeleteAt(id);

            return true;
        }

        public bool DeleteTeamById(int id)
        {
            if ((unitOfWork.Teams as TeamRepository).Count() < id - 1)
                return false;

            (unitOfWork.Teams as TeamRepository).DeleteAt(id);

            return true;
        }

        public bool DeleteUserById(int id)
        {
            if ((unitOfWork.Users as UserRepository).Count() < id - 1)
                return false;

            (unitOfWork.Users as UserRepository).DeleteAt(id);

            return true;
        }
    }
}
