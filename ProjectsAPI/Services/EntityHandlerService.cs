using Projects.DataAccess.Interfaces;
using Projects.DataAccess.Repositories;
using Projects.Modelling.Entities;
using Projects.Modelling.Interfaces;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Projects.API.Interfaces;
using Projects.Modelling.DTOs;

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

            await System.Threading.Tasks.Task.WhenAll(projectModelsTask,
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
            var usersModelsTask = (unitOfWork.Users as UserRepository).GetAllAsync();
            var teamsModelsTask = (unitOfWork.Teams as TeamRepository).GetAllAsync();
            var tasksModelsTask = (unitOfWork.Tasks as TaskRepository).GetAllAsync();
            var projectsModelsTask = (unitOfWork.Projects as ProjectRepository).GetAllAsync();

            await System.Threading.Tasks.Task.WhenAll(
                               usersModelsTask,
                               teamsModelsTask,
                               tasksModelsTask,
                               projectsModelsTask
                               );

            return
                binder.BindProjectEntities(
                    projectsModelsTask.Result.Where(n => n.Id == id),
                    tasksModelsTask.Result,
                    usersModelsTask.Result,
                    teamsModelsTask.Result)
                .FirstOrDefault();
        }

        public async Task<IEnumerable<TaskEntity>> GetAllTaskEntitiesAsync()
        {
            var usersModelsTask = (unitOfWork.Users as UserRepository).GetAllAsync();
            var tasksModelsTask = (unitOfWork.Tasks as TaskRepository).GetAllAsync();

            await System.Threading.Tasks.Task.WhenAll(
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
            var usersModelsTask = (unitOfWork.Users as UserRepository).GetAllAsync();
            var tasksModelsTask = (unitOfWork.Tasks as TaskRepository).GetAllAsync();

            await System.Threading.Tasks.Task.WhenAll(
                               usersModelsTask,
                               tasksModelsTask
                               );

            return
                binder.BindTaskEntities(
                tasksModelsTask.Result.Where(n => n.Id == id),
                usersModelsTask.Result)
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
            var users = await GetAllUserEntitiesAsync();

            return users.Where(n => n.Id == id)
                .FirstOrDefault();
        }

        public async Task<IEnumerable<TeamEntity>> GetAllTeamEntitiesAsync()
        {
            var usersModelsTask = (unitOfWork.Users as UserRepository).GetAllAsync();
            var teamsModelsTask = (unitOfWork.Teams as TeamRepository).GetAllAsync();

            await System.Threading.Tasks.Task.WhenAll(
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
            var usersModelsTask = (unitOfWork.Users as UserRepository).GetAllAsync();
            var teamsModelsTask = (unitOfWork.Teams as TeamRepository).GetAllAsync();

            await System.Threading.Tasks.Task.WhenAll(
                               usersModelsTask,
                               teamsModelsTask
                               );

            return
                binder.BindTeamEntities(
                teamsModelsTask.Result.Where(n => n.Id == id),
                usersModelsTask.Result)
                .FirstOrDefault();
        }
    }
}
