using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.QueriesUI.Interfaces
{
    interface IQueryService
    {
        public string Run()
        {
            while (true)
            {
                var taskInfo = new UI(
                    new[]{
                "ShowTasksQuantityPerProject",
                "ShowTasksForPerformer",
                "ShowTasksPerPerformerFromThisYear",
                "ShowOldestUsersByTeam",
                "ShowTasksPerPerformer",
                "ShowUserInfo",
                "ShowProjectsInfo"
                },
                    new[]{
                "Task 1",
                "Task 2",
                "Task 3",
                "Task 4",
                "Task 5",
                "Task 6",
                "Task 7"
                }).GetTaskNumber();

                 RunTaskOnName(taskInfo.Method);

                Console.WriteLine("\n Press Esc to quit. Other buttons restart the execution");
                if (Console.ReadKey().Key == ConsoleKey.Escape) break;
                Console.Clear();
            }

            return "";
        }

        private void RunTaskOnName(string taskName)
        {
            switch (taskName)
            {
                case "ShowTasksQuantityPerProject":
                    ShowTasksQuantityPerProject(GetParameter());
                    break;
                case "ShowTasksForPerformer":
                    ShowTasksForPerformer(GetParameter());
                    break;
                case "ShowTasksPerPerformerFromThisYear":
                    ShowTasksPerPerformerFromThisYear(GetParameter());
                    break;
                case "ShowOldestUsersByTeam":
                    ShowOldestUsersByTeam();
                    break;
                case "ShowTasksPerPerformer":
                    ShowTasksPerPerformer();
                    break;
                case "ShowUserInfo":
                    ShowUserInfo(GetParameter());
                    break;
                case "ShowProjectsInfo":
                    ShowProjectsInfo();
                    break;
                default:
                    Console.WriteLine("Task number is wrong");
                    break;
            }
        }
        //Task 1
        private void ShowTasksQuantityPerProject(int authorId)
        {
            if (!BasicCheckId(authorId))
            {
                Console.WriteLine("Wrong input");
                return;
            }

            Console.WriteLine("Tasks quantity per project for author with id = " + authorId);

            var taskQuantityByProject = await queryProcessor.GetTasksQuantityPerProjectvoid(authorId);

            if (taskQuantityByProject.Count < 1)
            {
                Console.WriteLine("No tasks were found for this author");
                return;
            }
            foreach (var item in taskQuantityByProject)
            {
                Console.WriteLine($"Project {item.Key.Name} has {item.Value} tasks");
            }
        }
        //Task 2
        private void ShowTasksForPerformer(int performerId)
        {
            if (!BasicCheckId(performerId))
            {
                Console.WriteLine("Wrong input");
                return;
            }

            Console.WriteLine("Tasks per performer with Id = " + performerId);

            var tasks = await queryProcessor.GetTasksPerPerformervoid(performerId);

            if (tasks.Count < 1)
            {
                Console.WriteLine("No tasks with Name less than 45 chars for this performer found");
            }

            foreach (var item in tasks)
            {
                Console.WriteLine($" Task {item.Name} with Id = {item.Id}");
            }
        }

        //Task 3
        private void ShowTasksPerPerformerFromThisYear(int userId)
        {
            if (!BasicCheckId(userId))
            {
                Console.WriteLine("Wrong input");
                return;
            }

            Console.WriteLine("Tasks per Performer finished this year. ID of performer = " + userId);

            var tasks = await queryProcessor.GetTasksPerPerformerFinishedThisYearvoid(userId);

            if (tasks.Count < 1) Console.WriteLine("No tasks found. Make sure that your input is correct");

            foreach (var item in tasks)
            {
                Console.WriteLine($"Task #{item.Id} called {item.Name} for Performer with Id = {userId}");
            }
            Console.WriteLine("-______-");
        }

        //Task 4
        private void ShowOldestUsersByTeam()
        {
            Console.WriteLine("Oldest users by team");
            foreach (var item in await queryProcessor.GetOldestUsersByTeamvoid())
            {
                Console.WriteLine($" From team #{item.Id} named {item.Name} users:");
                foreach (var user in item.Users)
                {
                    Console.WriteLine($"User {user.FirstName}");
                }
                Console.WriteLine("-------");
            }
        }

        //Task 5
        private void ShowTasksPerPerformer()
        {
            Console.WriteLine("Tasks Per Performer Alphabetically");
            foreach (var item in await queryProcessor.GetTasksPerPerformerAlphabeticallyvoid())
            {
                Console.WriteLine($"---------Performer {item.Key.Id}-------------");
                Console.WriteLine($"For Performer {item.Key.FirstName} with Id {item.Key.Id} Tasks:");

                foreach (var task in item.Value)
                {
                    Console.WriteLine($"Task {task.Name} with Id {task.Id}");
                }
                Console.WriteLine("------------------------------------------------");
            }
        }

        //Task 6
        private void ShowUserInfo(int userId)
        {
            if (!BasicCheckId(userId))
            {
                Console.WriteLine("Wrong input");
                return;
            }
            Console.WriteLine("User Info for id = " + userId);

            try
            {
                var userInfo = await queryProcessor.GetUserInfovoid(userId);

                Console.WriteLine($"User {userInfo.User.FirstName}" +
                $" has last project {userInfo.LastProject.Name} ," +
                $" {userInfo.LastProjectTasksQuantity} tasks in it, " +
                $"{userInfo.UnhandledTasksQuantity} unhandeled tasks " +
                $"and longest task {userInfo.LongestTask.Name}");
            }
            catch
            {
                Console.WriteLine($"No projects were found for user " + userId);
            }
        }

        //Task 7
        private void ShowProjectsInfo()
        {
            Console.WriteLine("Projects Info");

            Console.WriteLine($"Project Name" +
                $" | Longest Task" +
                $" | Shortest Task" +
                $" | Quantity of users");

            foreach (var item in await queryProcessor.GetProjectsInfovoid())
            {
                Console.WriteLine($"{item.Project.Name}" +
                    $" | {item.LongestTask?.Name ?? "No tasks found"}" +
                    $" | {item.ShortestTask?.Name ?? "No tasks found"} " +
                    $" | {item.UsersQuantity}");
            }
        }

        private int GetParameter()
        {
            Console.Clear();
            Console.WriteLine("This method accepts additionl Id parameter :");

            int param;
            while (true)
            {
                try
                {
                    param = Convert.ToInt32(Console.ReadLine());

                    break;
                }
                catch { }
            }

            return param;
        }
    }
}
