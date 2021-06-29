using Projects.API.Interfaces;
using Projects.Modelling.DTOs;
using Projects.Modelling.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.API.Services
{
    public class QueryProcessingService : IQueryProcessingService
    {
        private readonly IEntityHandlerService handler;

        public QueryProcessingService(IEntityHandlerService handler)
        {
            this.handler = handler;
        }

        public async Task<IEnumerable<KeyValuePair<ProjectEntity, List<TaskEntity>>>> GetTasksQuantityPerProjectAsync(int userId)
        {
            var projects = await handler.GetAllProjectEntitiesAsync();

            var taskQuantityByTask =
                                    projects
                                    .Where(n => n.Author.Id == userId)
                                    .Select(n => new KeyValuePair<ProjectEntity, List<TaskEntity>>(n, n.Tasks.ToList()));

            return taskQuantityByTask.ToArray();
        }

        public async Task<IList<TaskEntity>> GetTasksPerPerformerAsync(int performerId)
        {
            var tasks = await handler.GetAllTaskEntitiesAsync();

            try
            {
                return
                tasks
                  .Where(n => n.Performer.Id == performerId)
                  .Where(n => n.Name.Count() < 45)
                  .ToList();
            }
            catch { throw; }
        }

        public async Task<IList<TaskInfo>> GetTasksPerPerformerFinishedThisYearAsync(int performerId)
        {
            var tasks = await handler.GetAllTaskEntitiesAsync();

            return
                tasks
                .Where(n => n.Performer.Id == performerId)
                .Where(n => n.State == State.Finished)
                .Where(n => n.FinishedAt?.Year == DateTime.Now.Year)
                .Select(n => new TaskInfo() { Id = n.Id, Name = n.Name })
                .ToList();
        }

        public async Task<IEnumerable<OldestUsersInfo>> GetOldestTeamsAsync()
        {
            var teams = await handler.GetAllTeamEntitiesAsync();

            return
                teams.Where( team =>
                           team.Users.All(user =>
                           (DateTime.Now.Year - user.BirthDay.Year) > 10))
                  .Select(n => new OldestUsersInfo()
                  {
                      Id = n.Id,
                      Name = n.Name,
                      Users = n.Users
                               .OrderByDescending(user => user.RegisteredAt)
                  });
        }

        public async Task<IEnumerable<KeyValuePair<UserEntity, List<TaskEntity>>>> GetTasksPerPerformerAlphabeticallyAsync()
        {
            var tasks = await handler.GetAllTaskEntitiesAsync();

            return
                tasks
                  .OrderByDescending(task => task.Name.Length)
                  .GroupBy(task => task.Performer)
                  .OrderBy(n => n.Key.FirstName)

                  .Select(n => new KeyValuePair<UserEntity, List<TaskEntity>>(n.Key, n.ToList()));
        }

        //Throws InvalidOperation if no projects were found
        public async Task<UserInfo> GetUserInfoAsync(int userId)
        {
            var usersTask =
            handler.GetAllUserEntitiesAsync();

            var projectsTask =
            handler.GetAllProjectEntitiesAsync();

            var tasksTask =
            handler.GetAllTaskEntitiesAsync();

            await System.Threading.Tasks.Task.WhenAll(usersTask, projectsTask, projectsTask);

            var users = usersTask.Result;
            var projects = projectsTask.Result;
            var tasks = tasksTask.Result;

            try
            {
                return
                 (from user in users
                  where user.Id == userId
                  let lastProject = projects
                                        .Where(n => n.Author.Id == user.Id)
                                        .Aggregate((a, b) => b.CreatedAt > a.CreatedAt ? b : a)
                  let lastProjectTasksQuantity = lastProject.Tasks.Count()
                  let userTasks = tasks
                                    .Where(n => n.Performer.Id == user.Id)
                  let unhandledTasksQuantity = userTasks
                                         .Where(n => n.State == State.Canceled || n.State == State.InProgress)
                                         .Count()
                  let longestTask = userTasks.Aggregate((a, b) =>
                  {
                      if (!b.FinishedAt.HasValue)
                      {
                          if (a.FinishedAt.HasValue)
                              return b;

                          else
                              return b.CreatedAt < a.CreatedAt ? b : a;
                      }
                      else
                      {
                          if (!a.FinishedAt.HasValue)
                              return a;

                          else
                              return (b.FinishedAt - b.CreatedAt).Value > (a.FinishedAt - a.CreatedAt).Value ? b : a;
                      }
                  })
                  select new UserInfo()
                  {
                      User = user,
                      LastProject = lastProject,
                      LastProjectTasksQuantity = lastProjectTasksQuantity,
                      UnhandledTasksQuantity = unhandledTasksQuantity,
                      LongetsTask = longestTask
                  })
                  .FirstOrDefault();
            }
            catch { throw; }
        }

        public async Task<IEnumerable<ProjectInfo>> GetProjectsInfoAsync()
        {
            var projects = await handler.GetAllProjectEntitiesAsync();

            return
                 from project in projects
                 let hasTasks = project.Tasks.Count() > 0
                 select new ProjectInfo()
                 {
                     Project = project,
                     LongestTask = !hasTasks ?
                                        null :
                                        project.Tasks
                                                .Aggregate((a, b) =>
                                                           b.Description.Length > a.Description.Length ? b : a),

                     ShortestTask = !hasTasks ?
                                         null :
                                         project.Tasks
                                                .Aggregate((a, b) =>
                                                           b.Name.Length < a.Name.Length ? b : a),

                     UsersQuantity = (project.Description.Length > 20 || project.Tasks.Count() < 3) ?
                                                                         project.Team.Users.Count() :
                                                                         0
                 };

        }
    }
}
