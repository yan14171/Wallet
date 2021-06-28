using System;

namespace Projects.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProjectRepository Projects { get; }

        ITaskRepository Tasks { get; }

        ITeamRepository Teams { get; }

        IUserRepository Users { get; }

        int Complete();
    }
}
