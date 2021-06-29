﻿using Projects.QueriesUI.Interfaces;
using System;
using System.Threading.Tasks;

namespace Projects.QueriesUI.Services
{
    class QueryService : IQueryService
    {
       public string Run()
        {
            //Task 1
            private async Task ShowTasksQuantityPerProject(int authorId)
            {
                if (!BasicCheckId(authorId))
                {
                    Console.WriteLine("Wrong input");
                    return;
                }

                Console.WriteLine("Tasks quantity per project for author with id = " + authorId);

                var taskQuantityByProject = await queryProcessor.GetTasksQuantityPerProjectAsync(authorId);

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
            private async Task ShowTasksForPerformer(int performerId)
            {
                if (!BasicCheckId(performerId))
                {
                    Console.WriteLine("Wrong input");
                    return;
                }

                Console.WriteLine("Tasks per performer with Id = " + performerId);

                var tasks = await queryProcessor.GetTasksPerPerformerAsync(performerId);

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
            private async Task ShowTasksPerPerformerFromThisYear(int userId)
            {
                if (!BasicCheckId(userId))
                {
                    Console.WriteLine("Wrong input");
                    return;
                }

                Console.WriteLine("Tasks per Performer finished this year. ID of performer = " + userId);

                var tasks = await queryProcessor.GetTasksPerPerformerFinishedThisYearAsync(userId);

                if (tasks.Count < 1) Console.WriteLine("No tasks found. Make sure that your input is correct");

                foreach (var item in tasks)
                {
                    Console.WriteLine($"Task #{item.Id} called {item.Name} for Performer with Id = {userId}");
                }
                Console.WriteLine("-______-");
            }

            //Task 4
            private async Task ShowOldestUsersByTeam()
            {
                Console.WriteLine("Oldest users by team");
                foreach (var item in await queryProcessor.GetOldestUsersByTeamAsync())
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
            private async Task ShowTasksPerPerformer()
            {
                Console.WriteLine("Tasks Per Performer Alphabetically");
                foreach (var item in await queryProcessor.GetTasksPerPerformerAlphabeticallyAsync())
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
            private async Task ShowUserInfo(int userId)
            {
                if (!BasicCheckId(userId))
                {
                    Console.WriteLine("Wrong input");
                    return;
                }
                Console.WriteLine("User Info for id = " + userId);

                try
                {
                    var userInfo = await queryProcessor.GetUserInfoAsync(userId);

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
            private async Task ShowProjectsInfo()
            {
                Console.WriteLine("Projects Info");

                Console.WriteLine($"Project Name" +
                    $" | Longest Task" +
                    $" | Shortest Task" +
                    $" | Quantity of users");

                foreach (var item in await queryProcessor.GetProjectsInfoAsync())
                {
                    Console.WriteLine($"{item.Project.Name}" +
                        $" | {item.LongestTask?.Name ?? "No tasks found"}" +
                        $" | {item.ShortestTask?.Name ?? "No tasks found"} " +
                        $" | {item.UsersQuantity}");
                }
            }
        }
    }
}