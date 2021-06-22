using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectsAccess.DataAccess.IRepositories
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
