using Projects.API.Interfaces;
using Projects.DataAccess.Interfaces;
using Projects.DataAccess.Repositories;
using Projects.Modelling.Entities;
using Projects.Modelling.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    }
}
