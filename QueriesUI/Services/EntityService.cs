using Projects.QueriesUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.QueriesUI.Services
{
    class EntityService
    {
        public string Run()
        {
            while (true)
            {

            var taskInfo = new UI(new[] { "TaskEntityService", "TeamEntityService", "UserEntityService", "ProjectEntityService" },
                                     new[] { "Work with Tasks", "Work with Teams", "Work with Users", "Work with Projects" }).GetTaskNumber();

            var type = Type.GetType("Projects.QueriesUI.Services." + taskInfo.Method);

            var method =
            type.GetMethod("Run");

                try
                {
                    method.Invoke(Activator.CreateInstance(type), null);
                }
                catch { Console.WriteLine("Error!");
                 
                }
            Console.WriteLine("\n Press Esc to quit. Other buttons restart the execution");
            if (Console.ReadKey().Key == ConsoleKey.Escape) break;
            Console.Clear();

            }
            return "";
        }
    }
}

