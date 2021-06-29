using Projects.Modelling.Interfaces;
using Projects.QueriesUI.Interfaces;
using System;
using System.Threading.Tasks;

namespace Projects.QueriesUI.Services
{
    class ApplicationInterface : IApplicationInterface
    {
        private readonly IEntityBinderService entityBinder;

        public ApplicationInterface( IEntityBinderService entityBinder)
        {
            this.entityBinder = entityBinder;
        }

        public async Task RunAsync()
        {
            while (true)
            {
                var taskInfo= new UI(new[]{ "EntityService", "QueryService" } ,
                                     new[]{ "Work with entities", "Work with queries" }).GetTaskNumber();

                var type = Type.GetType("Projects.QueriesUI.Services." + taskInfo.Method);

                var method =
                type.GetMethod("Run");

                method.Invoke(Activator.CreateInstance(type), null);
 
                Console.WriteLine("\n Press Esc to quit. Other buttons restart the execution");
                if (Console.ReadKey().Key == ConsoleKey.Escape) break;
                Console.Clear();
            }
        }
 

    }
}
