using ProjectsAccess.DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjectsAccess.Repositories
{
    public class APIRepository<T> : IRepository<T> where T : class, new()
    {
        public APIRepository()
        {

        }
        public APIRepository(string apiEndpoint)
        {
            this.apiEndpoint = apiEndpoint;
        }

        private readonly string apiEndpoint;

        public IEnumerable<T> GetAll()
        {
            string connectionString = GetConnectionString<T>();

            IEnumerable<T> objects = GetObjects(connectionString);

            return objects;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            string connectionString = GetConnectionString<T>();

            IEnumerable<T> objects = await GetObjectsAsync(connectionString);

            return objects;
        }

        public T GetById(int id)
        {
            if (id < 0) return new T();

            string connectionString = GetConnectionString<T>();

            connectionString += $"/{id}";

            T @object = GetObject(connectionString);

            return @object;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            if (id < 0) return new T();

            string connectionString = GetConnectionString<T>();

            connectionString += $"/{id}";

            T @object = await GetObjectAsync(connectionString);

            return @object;
        }


        private async Task<IEnumerable<T>> GetObjectsAsync(string connectionString)
        {
            var response = await new HttpClient().GetAsync(connectionString);

            string content = String.Empty;

            if (response.IsSuccessStatusCode)
                content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T[]>(content);

        }

        private IEnumerable<T> GetObjects(string connectionString)
        {
            var response = new HttpClient().GetAsync(connectionString).Result;

            string content = String.Empty;

            if (response.IsSuccessStatusCode)
                content = response.Content.ReadAsStringAsync().Result;
           
            var objects = JsonSerializer.Deserialize<T[]>(content);

            return objects;
        }

        private async Task<T> GetObjectAsync(string connectionString)
        {
            var response = await new HttpClient().GetAsync(connectionString);

            string content = String.Empty;

            if (response.IsSuccessStatusCode)
                content = await response.Content.ReadAsStringAsync();

            var @object = JsonSerializer.Deserialize<T>(content);

            return @object;
        }

        private T GetObject(string connectionString)
        {
            var response = new HttpClient().GetAsync(connectionString).Result;

            string content = String.Empty;

            if (response.IsSuccessStatusCode)
                content = response.Content.ReadAsStringAsync().Result;

            var @object = JsonSerializer.Deserialize<T>(content);

            return @object;
        }

        private string GetConnectionString<T>()
        {
            return apiEndpoint + "/" + typeof(T).Name + "s";
        }
    }
}
