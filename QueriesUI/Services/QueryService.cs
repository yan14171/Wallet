using Projects.Modelling.Entities;
using Projects.QueriesUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Projects.Modelling.DTOs;

namespace Projects.QueriesUI.Services
{
    class QueryService
    {
       public string Run()
        {
            Console.Clear();
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

            var taskQuantityByProjectstring = new HttpClient().
                GetAsync($"https://localhost:5001/api/misc/tasksQuantity/{authorId}");

            var resres = taskQuantityByProjectstring
                .Result;
            
            var tasksqRes = resres.Content.ReadAsStringAsync().Result;
                
            var taskQuantityByProject =
                JsonConvert.DeserializeObject<List<KeyValuePair<ProjectEntity, List<TaskEntity>>>>(tasksqRes)
             .ToList();

            if (taskQuantityByProject.Count() < 1)
            {
                Console.WriteLine("No tasks were found for this author");
                return;
            }
            foreach (var item in taskQuantityByProject)
            {
                Console.WriteLine($"Project {item.Key.Name} has {item.Value.Count()} tasks");
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

            var tasks = JsonConvert.DeserializeObject<IList<TaskEntity>>
              (new HttpClient().
                GetAsync($"https://localhost:5001/api/misc/tasks/{performerId}")
                .Result.Content.ReadAsStringAsync().Result).ToList();

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

            var tasks = JsonConvert.DeserializeObject<List<TaskInfo>>
              (new HttpClient().
                GetAsync($"https://localhost:5001/api/misc/tasksThisYear/{userId}")
                .Result.Content.ReadAsStringAsync().Result).ToList();

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


            var tasks = JsonConvert.DeserializeObject<IEnumerable<OldestUsersInfo>>
              (new HttpClient().
                GetAsync($"https://localhost:5001/api/misc/oldestTeams")
                .Result.Content.ReadAsStringAsync().Result).ToList();

            foreach (var item in tasks)
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

            var tasks = JsonConvert.DeserializeObject < IEnumerable < KeyValuePair < UserEntity, List< TaskEntity >>>>
              (new HttpClient().
                GetAsync($"https://localhost:5001/api/misc/tasksAlpha")
                .Result.Content.ReadAsStringAsync().Result).ToList();

            foreach (var item in tasks)
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
                var userInfo = JsonConvert.DeserializeObject<UserInfo>
                  (new HttpClient().
                    GetAsync($"https://localhost:5001/api/misc/userInfo/{userId}")
                    .Result.Content.ReadAsStringAsync().Result);

                Console.WriteLine($"User {userInfo.User.FirstName}" +
                $" has last project {userInfo.LastProject.Name} ," +
                $" {userInfo.LastProjectTasksQuantity} tasks in it, " +
                $"{userInfo.UnhandledTasksQuantity} unhandeled tasks " +
                $"and longest task {userInfo.LongetsTask.Name}");
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

           var tasksString = new HttpClient().GetAsync($"https://localhost:5001/api/misc/projectsInfo")
                    .Result.Content.ReadAsStringAsync().Result;

           var tasks = JsonConvert.DeserializeObject<IEnumerable<ProjectInfo>>
                  (tasksString)
                    .ToList();

            foreach (var item in tasks)
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

        private bool BasicCheckId(int userId)
        {
            if (userId < 0)
                return false;

            return true;
        }

    }
}

