using Newtonsoft.Json;
using Projects.Modelling.DTOs;
using Projects.Modelling.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Projects.QueriesUI.Services
{
    class UserEntityService
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
                .DeleteAsync($"https://localhost:5001/api/users/{Id}");
        }

        private void Put()
        {
            int id = GetParameter<int>("Id");

            var user = new User()
            {
                Id = id,

                TeamId = GetParameter<int>("TeamID:"),

                FirstName = GetParameter<string>("Name:"),

                LastName = GetParameter<string>("surname:"),

                Email = GetParameter<string>("email:"),

                RegisteredAt = GetParameter<DateTime>("Registered Date:"),

                BirthDay = GetParameter<DateTime>("Birth day:")
            };

            StringContent httpContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var result = new HttpClient().PutAsync($"https://localhost:5001/api/users/{id}", httpContent).Result;
        }

        private void Post()
        {
            var user = new User()
            { 
                Id = GetParameter<int>("Id"),

                TeamId = GetParameter<int>("TeamID:"),

                FirstName = GetParameter<string>("Name:"),

                LastName = GetParameter<string>("surname:"),

                Email = GetParameter<string>("email:"),

                RegisteredAt = GetParameter<DateTime>("Registered Date:"),

                BirthDay = GetParameter<DateTime>("Birth day:")
            };

            StringContent httpContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var result = new HttpClient().PostAsync($"https://localhost:5001/api/users", httpContent).Result;
        }

        private void GetByID()
        {
            int userId = GetParameter<int>("Id:");

            var user = JsonConvert.DeserializeObject<UserEntity>
              (new HttpClient().
                GetAsync($"https://localhost:5001/api/users/{userId}")
                .Result.Content.ReadAsStringAsync().Result);

            Console.WriteLine(user.ToString());
        }

        private void GetAll()
        {
            var users = JsonConvert.DeserializeObject<List<UserEntity>>
              (new HttpClient().
                GetAsync($"https://localhost:5001/api/users")
                .Result.Content.ReadAsStringAsync().Result);

            foreach (var item in users)
            {
                Console.WriteLine(item.ToString());
            }
        }

        private T GetParameter<T>(string parmaName)
        {
            Console.Clear();
            Console.WriteLine("This method accepts additionl "+ parmaName +" parameter :");

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
