using Autofac;
using Projects.Modelling.Interfaces;
using Projects.Modelling.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Projects.QueriesUI
{
    class ServicesRegister
    {
        public static IContainer RegisterContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<IEntityBinderService>()
                .As<EntityBinderService>();

            builder.RegisterType<ApplicationInterface>()
                .As<IApplicationInterface>();

            return builder.Build();
        }

    }
}
