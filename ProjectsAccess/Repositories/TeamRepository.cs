using Projects.DataAccess.Interfaces;
using Projects.Modelling.DTOs;

namespace Projects.DataAccess.Repositories
{
    public class TeamRepository : APIRepository<Team>, ITeamRepository
    {
        public TeamRepository(string APIendpoint)
        : base(APIendpoint)
        {
        }
    }
}
