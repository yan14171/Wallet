using Autofac;
using System;
using System.Threading.Tasks;

namespace QueriesUI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var container = ServicesRegister.RegisterContainer();

            using (var scope = container.BeginLifetimeScope())
            {
                var appInterface = scope.Resolve<IApplicationInterface>();
                await (appInterface as ApplicationInterface).RunAsync();
            }
        }
    }
}
