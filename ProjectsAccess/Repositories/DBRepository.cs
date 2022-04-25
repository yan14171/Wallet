using Microsoft.EntityFrameworkCore;
using Projects.DataAccess.Interfaces;
using Projects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Projects.DataAccess.Repositories
{
    public class DBRepository<T> : IRepository<T> where T : EntityBase, new()
    {
        private List<T> _models;

        private DbContext _context;

        public DBRepository(DbContext context)
        {
            this._context = context;

            _models = GetObjects(context).ToList();
        }



#region public:

        public IEnumerable<T> GetAll()
        {
            /*string connectionString = GetConnectionString<T>();

            IEnumerable<T> objects = GetObjects(connectionString);

            return objects;*/

            return _models;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            /* string connectionString = GetConnectionString<T>();

             IEnumerable<T> objects = await GetObjectsAsync(connectionString);

             return objects;*/

            return _models;
        }

        public T GetById(int id)
        {
            if (id < 0) return new T();

            var foundEntitities =
                _models.Where(n => n.Id == id);

            if (foundEntitities.Count() > 1 || foundEntitities.Count() < 1)
                return null;

            return foundEntitities.First();

           /* string connectionString = GetConnectionString<T>();

            connectionString += $"/{id}";

            T @object = GetObject(connectionString);

            return @object;*/
        }

        public async Task<T> GetByIdAsync(int id)
        {

            if (id < 0) return new T();

            var foundEntitities =
                _models.Where(n => n.Id == id);

            if (foundEntitities.Count() > 1 || foundEntitities.Count() < 1)
                return new T();

            return foundEntitities.First();

            /* if (id < 0) return new T();

             string connectionString = GetConnectionString<T>();

             connectionString += $"/{id}";

             T @object = await GetObjectAsync(connectionString);

             return @object;*/
        }

        public void Add(T model)
        {
            _models.Add(model);
            _context.SaveChanges();
        }

        public void AddAsync(T model)
        {
            _models.Add(model);
            _context.SaveChangesAsync();
        }

        public void DeleteAt(int id)
        {
            _models.RemoveAll(n => n.Id == id);
        }

        public int Count() => _models.Count();

#endregion

#region private

        private async Task<IEnumerable<T>> GetObjectsAsync(DbContext context)
        {
            var objects = await context.Set<T>().AsNoTracking().ToListAsync();

            return objects;
        }

        private IEnumerable<T> GetObjects(DbContext context)
        {
            var objects = context.Set<T>();

            return objects;
        }

        private async Task<T> GetObjectAsync(DbContext context, int id)
        {
            var result = await context.Set<T>().AsNoTracking().SingleAsync(n => n.Id == id);

            return result;
        }

        private T GetObject(DbContext context, int id)
        {
            var result = context.Set<T>().AsNoTracking().Single(n => n.Id == id);

            return result;
        }

#endregion
    }
}
