using Autofac;
using Projects.Modelling.Interfaces;
using Projects.Modelling.Services;
using Projects.QueriesUI.Interfaces;
using Projects.QueriesUI.Services;

namespace Projects.QueriesUI
{
    class ServicesRegister
    {
        public static IContainer RegisterContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<EntityBinderService>()
                .As<IEntityBinderService>();

            builder.RegisterType<ApplicationInterface>()
                .As<IApplicationInterface>();

            return builder.Build();
        }

    }
}
