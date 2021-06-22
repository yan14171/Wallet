using Autofac;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ProjectsAccess;
using System.Linq;
using ProjectsAccess.Repositories;
using ProjectsAccess.DataAccess.IRepositories;
using ProjectsAccess.Services;

namespace QueriesUI
{
    class ServicesRegister
    {
        public static IContainer RegisterContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterGeneric(typeof(APIRepository<>))
                   .As(typeof(IRepository<>))
                .WithParameter(new TypedParameter(typeof(string), "https://bsa21.azurewebsites.net/api"));

            builder.RegisterType<ProjectRepository>()
                .As<IProjectRepository>()
                .WithParameter(new TypedParameter(typeof(string), "https://bsa21.azurewebsites.net/api"));

            builder.RegisterType<TeamRepository>()
              .As<ITeamRepository>()
              .WithParameter(new TypedParameter(typeof(string), "https://bsa21.azurewebsites.net/api"));

            builder.RegisterType<TaskRepository>()
              .As<ITaskRepository>()
              .WithParameter(new TypedParameter(typeof(string), "https://bsa21.azurewebsites.net/api"));

            builder.RegisterType<UserRepository>()
              .As<IUserRepository>()
              .WithParameter(new TypedParameter(typeof(string), "https://bsa21.azurewebsites.net/api"));

            builder.RegisterType<APIUnitOfWork>()
             .As<IUnitOfWork>();

            builder.RegisterType<EntityBinderService>()
             .As<IEntityBinderService>();

            builder.RegisterType<QueryProcessingService>()
                .AsSelf();

            builder.RegisterType<ApplicationInterface>()
                .As<IApplicationInterface>();

            return builder.Build();
        }

    }
}
