using Projects.API.Interfaces;
using Projects.DataAccess.Interfaces;
using Projects.DataAccess.Repositories;
using Projects.Modelling.Entities;
using Projects.Modelling.Interfaces;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

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

        public async Task<ProjectEntity> GetProjectEntityById(int id)
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

        public async Task<TaskEntity> GetTaskEntityById(int id)
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

        public async Task<UserEntity> GetUserEntityById(int id)
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

        public async Task<TeamEntity> GetTeamEntityById(int id)
        {
            var tasks = await GetAllTeamEntitiesAsync();

            return tasks.Where(n => n.Id == id)
                .FirstOrDefault();
        }

        public bool AddProject(ProjectEntity project)
        {
            throw new NotImplementedException();
        }

    }
}
