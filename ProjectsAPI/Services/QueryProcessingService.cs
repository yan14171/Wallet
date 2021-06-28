using Projects.Modelling.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.API.Services
{
    public class QueryProcessingService
    {

        public QueryProcessingService(EntityHandle binder)
        {
            this.binder = binder;
        }

        /// <summary>
        /// Отримати кількість тасків у проекті конкретного користувача (по id) (словник, де key буде проект, а value кількість тасків).
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Dictionary<ProjectEntity, int> GetTasksQuantityPerProject(int userId)
        {
            
            var projects = binder.BindProjectEntities();

            return
            projects
              .Where(n => n.Author.Id == userId)
              .ToDictionary(n => n, n => n.Tasks.Count());
        }

        public async Task<Dictionary<ProjectEntity, int>> GetTasksQuantityPerProjectAsync(int userId)
        {
            var projects = await (binder as EntityBinderService).BindProjectEntitiesAsync();

            var taskQuantityByTask =
                                    projects
                                    .Where(n => n.Author.Id == userId)
                                    .ToDictionary(n => n, n => n.Tasks.Count());

            return taskQuantityByTask;
        }

        public IList<TaskEntity> GetTasksPerPerformer(int performerId)
        {
            var tasks = binder.BindTaskEntities();

            return
              tasks
                .Where(n => n.Performer.Id == performerId)
                .Where(n => n.Name.Count() < 45)
                .ToList();

        }

        public async Task<IList<TaskEntity>> GetTasksPerPerformerAsync(int performerId)
        {
            var tasks = await (binder as EntityBinderService).BindTaskEntitiesAsync();

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

        public IList<(int Id, string Name)> GetTasksPerPerformerFinishedThisYear(int performerId)
        {
            var tasks = binder.BindTaskEntities();

            return
                tasks
                .Where(n => n.Performer.Id == performerId)
                .Where(n => n.State == Models.State.Finished)
                .Where(n => n.FinishedAt.Value.Year == DateTime.Now.Year)
                .Select(n => (Id: n.Id, Name: n.Name))
                .ToList();
        }

        public async Task<IList<(int Id, string Name)>> GetTasksPerPerformerFinishedThisYearAsync(int performerId)
        {
            var tasks = await (binder as EntityBinderService).BindTaskEntitiesAsync();

            return
                tasks
                .Where(n => n.Performer.Id == performerId)
                .Where(n => n.State == Models.State.Finished)
                .Where(n => n.FinishedAt?.Year == DateTime.Now.Year)
                .Select(n => (Id: n.Id, Name: n.Name))
                .ToList();
        }

        public IEnumerable<(int Id, string Name, IOrderedEnumerable<UserEntity> Users)> GetOldestUsersByTeam()
        {
            var teams = binder.BindTeamEntities();

            return 
                teams
                  .Select(n => (Id: n.Id,
                                Name: n.Name,
                                Users: n.Users
                                           .Where(user =>
                                          (DateTime.Now.Year - user.BirthDay.Year) > 10)
                                          .OrderByDescending(user => user.RegisteredAt)
                                          ));
        }

        public async Task<IEnumerable<(int Id, string Name, IOrderedEnumerable<UserEntity> Users)>> GetOldestUsersByTeamAsync()
        {
            var teams = await (binder as EntityBinderService).BindTeamEntitiesAsync();

            return
                teams
                  .Select(n => (Id: n.Id,
                                Name: n.Name,
                                Users: n.Users
                                          .Where(user =>
                                          (DateTime.Now.Year - user.BirthDay.Year) > 10)
                                          .OrderByDescending(user => user.RegisteredAt)
                                          ));
        }
            
        public IDictionary<UserEntity, List<TaskEntity>> GetTasksPerPerformerAlphabetically()
        {
            var tasks = binder.BindTaskEntities();

            return 
                tasks
                  .OrderByDescending(task => task.Name.Length)
                  .GroupBy(task => task.Performer)
                  .OrderBy(n => n.Key.FirstName)
                  .ToDictionary(n => n.Key, n => n.ToList());
        }

        public async Task<IDictionary<UserEntity, List<TaskEntity>>> GetTasksPerPerformerAlphabeticallyAsync()
        {
            var tasks = await (binder as EntityBinderService).BindTaskEntitiesAsync();

            return 
                tasks
                  .OrderByDescending(task => task.Name.Length)
                  .GroupBy(task => task.Performer)
                  .OrderBy(n => n.Key.FirstName)
                  .ToDictionary(n => n.Key, n => n.ToList());
        }

        //Throws InvalidOperation if no projects were found
        public (UserEntity User, ProjectEntity LastProject, int LastProjectTasksQuantity, int UnhandledTasksQuantity, TaskEntity LongestTask) GetUserInfo(int userId)
        {
            var users = binder.BindUserEntities();
            var projects = binder.BindProjectEntities();
            var tasks = binder.BindTaskEntities();

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
                                         .Where(n => n.State == Models.State.Canceled || n.State == Models.State.InProgress)
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
                  select
                  (user, lastProject, lastProjectTasksQuantity, unhandledTasksQuantity, longestTask))
                  .FirstOrDefault();
            }
            catch { throw; }
        }

        //Throws InvalidOperation if no projects were found
        public async Task<(UserEntity User, ProjectEntity LastProject, int LastProjectTasksQuantity, int UnhandledTasksQuantity, TaskEntity LongestTask)> GetUserInfoAsync(int userId)
        {
            var usersTask =
            (binder as EntityBinderService).BindUserEntitiesAsync();

            var projectsTask =
            (binder as EntityBinderService).BindProjectEntitiesAsync();

            var tasksTask =
            (binder as EntityBinderService).BindTaskEntitiesAsync();

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
                                         .Where(n => n.State == Models.State.Canceled || n.State == Models.State.InProgress)
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
                  select
                  (user, lastProject, lastProjectTasksQuantity, unhandledTasksQuantity, longestTask))
                  .FirstOrDefault();
            }
            catch { throw; }
        }

        public IEnumerable<(ProjectEntity Project, TaskEntity LongestTask, TaskEntity ShortestTask, int UsersQuantity)> GetProjectsInfo()
        {
            var projects = binder.BindProjectEntities();

            return
                from project in projects
                select (
                Project: project,
                LongestDesctiptionTask: project.Tasks?.Aggregate((a, b) => b.Description.Length > a.Description.Length ? b : a),
                ShortestNameTask: project.Tasks?.Aggregate((a, b) => b.Name.Length < a.Name.Length ? b : a),
                UsersQuantity: (project.Description.Length > 20 || project.Tasks.Count() < 3) ? project.Team.Users.Count() : 0);
        }

        public async Task<IEnumerable<(ProjectEntity Project, TaskEntity LongestTask, TaskEntity ShortestTask, int UsersQuantity)>> GetProjectsInfoAsync()
        {
            var projects = await (binder as EntityBinderService).BindProjectEntitiesAsync();

            return
                 from project in projects
                 let hasTasks = project.Tasks.Count()>0
                 select (
                 Project: project,
                 LongestDesctiptionTask: !hasTasks? null : project.Tasks.Aggregate((a, b) => b.Description.Length > a.Description.Length ? b : a),
                 ShortestNameTask: !hasTasks? null : project.Tasks.Aggregate((a, b) => b.Name.Length < a.Name.Length ? b : a),
                 UsersQuantity: (project.Description.Length > 20 || project.Tasks.Count() < 3) ? project.Team.Users.Count() : 0);
        }
    }
}
