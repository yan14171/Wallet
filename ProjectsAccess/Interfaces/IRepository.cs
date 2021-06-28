using System.Collections.Generic;

namespace Projects.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);

        IEnumerable<T> GetAll();
    }
}
