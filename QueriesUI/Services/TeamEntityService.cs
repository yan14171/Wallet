using Newtonsoft.Json;
using Projects.Modelling.DTOs;
using Projects.Modelling.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Projects.QueriesUI.Services
{
    class TeamEntityService
    {
        public string Run()
        {
            Console.Clear();
            while (true)
            {
                var taskInfo = new UI(
                    new[]{
                "GetAll",
                "GetById",
                "Post",
                "Put",
                "Delete"
            },
                    new[]{
                "Get all",
                "Get by ID",
                "Post",
                "Put",
                "Delete"
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
                case "GetAll":
                    GetAll();
                    break;
                case "GetById":
                    GetByID();
                    break;
                case "Post":
                    Post();
                    break;
                case "Put":
                    Put();
                    break;
                case "Delete":
                    Delete();
                    break;
                default:
                    Console.WriteLine("Task number is wrong");
                    break;
            }
        }

        private void Delete()
        {
            int Id = GetParameter<int>("Id");

            new HttpClient()
                .DeleteAsync($"https://localhost:5001/api/teams/{Id}");
        }

        private void Put()
        {
            int id = GetParameter<int>("Id");

            var team = new Team()
            {
                Id = id,

                CreatedAt = GetParameter<DateTime>("Created date"),

                Name = GetParameter<string>("Name")
            };


            StringContent httpContent = new StringContent(JsonConvert.SerializeObject(team), Encoding.UTF8, "application/json");

            var result = new HttpClient().PutAsync($"https://localhost:5001/api/teams/{id}", httpContent).Result;
        }

        private void Post()
        {
            var team = new Team()
            {
                Id = GetParameter<int>("Id"),
                
                CreatedAt = GetParameter<DateTime>("Created date"),
                
                Name = GetParameter<string>("Name")
            };

            StringContent httpContent = new StringContent(JsonConvert.SerializeObject(team), Encoding.UTF8, "application/json");

            var result = new HttpClient().PostAsync($"https://localhost:5001/api/teams", httpContent).Result;
        }

        private void GetByID()
        {
            int teamId = GetParameter<int>("Id:");

            var team = JsonConvert.DeserializeObject<TeamEntity>
              (new HttpClient().
                GetAsync($"https://localhost:5001/api/teams/{teamId}")
                .Result.Content.ReadAsStringAsync().Result);

            Console.WriteLine(team.ToString());
        }

        private void GetAll()
        {
            var teams = JsonConvert.DeserializeObject<List<TeamEntity>>
              (new HttpClient().
                GetAsync($"https://localhost:5001/api/teams")
                .Result.Content.ReadAsStringAsync().Result);

            foreach (var item in teams)
            {
                Console.WriteLine(item.ToString());
            }
        }

        private T GetParameter<T>(string parmaName)
        {
            Console.Clear();
            Console.WriteLine("This method accepts additionl " + parmaName + " parameter :");

            T param;
            while (true)
            {
                try
                {
                    param = (T)Convert.ChangeType(Console.ReadLine(), typeof(T));

                    break;
                }
                catch { Console.WriteLine("Wrong parameter type"); }
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
