using Projects.Modelling.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projects.API.Interfaces
{
    public interface IEntityHandlerService
    {
        Task<IEnumerable<ProjectEntity>> GetAllProjectEntities();
    }
}