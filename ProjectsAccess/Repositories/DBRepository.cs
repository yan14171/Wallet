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
        private DbContext _context;

        public DBRepository(DbContext context)
        {
            this._context = context;
        }

#region public:

        public IEnumerable<T> GetAll()
        {
            return GetObjects();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await GetObjectsAsync();
        }

        public T GetById(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException("id", id, "Id must be greater than 0");

            var foundEntitities =
                 GetObject(id);

            return foundEntitities;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException("id", id, "Id must be greater than 0");

            var foundEntitities =
                await GetObjectAsync(id);

            return foundEntitities;
        }

        public void Add(T model)
        {
            _context.Set<T>().Add(model);
            _context.SaveChanges();
        }

        public async Task AddAsync(T model)
        {
            _context.Set<T>().Add(model);
            await _context.SaveChangesAsync();
        }

        public void DeleteAt(int id)
        {
            try
            {
                var entity = GetById(id);
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException("Failed to delete an object",ex);
            }

        }

        public int Count() => _context.Set<T>().Count();

#endregion

#region private

        private async Task<IEnumerable<T>> GetObjectsAsync()
        {
            var objects = await _context.Set<T>().AsNoTracking().ToListAsync();

            return objects;
        }

        private IEnumerable<T> GetObjects()
        {
            var objects = _context.Set<T>().AsQueryable();

            return objects;
        }

        private async Task<T> GetObjectAsync(int id)
        {
            var result = await _context.Set<T>().SingleAsync(n => n.Id == id);

            return result;
        }

        private T GetObject(int id)
        {
            var result = _context.Set<T>().Single(n => n.Id == id);

            return result;
        }

#endregion
    }
}
