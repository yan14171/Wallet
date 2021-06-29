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
                var taskInfo= new UI(new[]{ "IEntityService", "IQueryService" } ,
                                     new[]{ "Work with entities", "Work with queries" }).GetTaskNumber();

                var type = Type.GetType("Projects.QueriesUI.Services." + taskInfo.Method.Trim('I'));

                var method =
                type.GetMethod("Run");

                method.Invoke(Activator.CreateInstance(type), null);
 
                Console.WriteLine("\n Press Esc to quit. Other buttons restart the execution");
                if (Console.ReadKey().Key == ConsoleKey.Escape) break;
                Console.Clear();
            }
        }

      /*  private async Task RunTaskOnNumber(int taskNumber, int inputId = 0)
        {
            switch (taskNumber)
            {
                case 1:
                    await ShowTasksQuantityPerProject(inputId);
                    break;
                case 2:
                    await ShowTasksForPerformer(inputId);
                    break;
                case 3:
                    await ShowTasksPerPerformerFromThisYear(inputId);
                    break;
                case 4:
                    await ShowOldestUsersByTeam();
                    break;
                case 5:
                    await ShowTasksPerPerformer();
                    break;
                case 6:
                    await ShowUserInfo(inputId);
                    break;
                case 7:
                    await ShowProjectsInfo();
                    break;
                default:
                    Console.WriteLine("Task number is wrong");
                    break;
            }
        }
*/
     

        private bool BasicCheckId(int userId)
        {
            if (userId < 0)
                return false;

            return true;
        }

    }
}
