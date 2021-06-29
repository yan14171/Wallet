using Projects.DataAccess.Interfaces;
using Projects.DataAccess.Repositories;
using Projects.Modelling.DTOs;

namespace Projects.API.Services
{
    public class DTOHandlerService : IDTOHandlerService
    {
        private readonly IUnitOfWork unitOfWork;

        public DTOHandlerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool AddTask(Modelling.DTOs.Task task)
        {
            (unitOfWork.Tasks as TaskRepository).Add(task);

            return true;
        }

        public bool AddUser(User user)
        {
            (unitOfWork.Users as UserRepository).Add(user);

            return true;
        }

        public bool AddTeam(Team team)
        {
            (unitOfWork.Teams as TeamRepository).Add(team);

            return true;
        }

        public bool AddProject(Project project)
        {
            (unitOfWork.Projects as ProjectRepository).Add(project);

            return true;
        }

        public bool TryAddTask(Modelling.DTOs.Task task)
        {
            if ((unitOfWork.Tasks as TaskRepository).GetById(task.Id) != null)
                return false;

            (unitOfWork.Tasks as TaskRepository).Add(task);

            return true;
        }

        public bool TryAddUser(User user)
        {
            if ((unitOfWork.Users as UserRepository).GetById(user.Id) != null)
                return false;

            (unitOfWork.Users as UserRepository).Add(user);

            return true;
        }

        public bool TryAddTeam(Team team)
        {
            if ((unitOfWork.Teams as TeamRepository).GetById(team.Id) != null)
                return false;

            (unitOfWork.Teams as TeamRepository).Add(team);

            return true;
        }

        public bool TryAddProject(Project project)
        {
            if ((unitOfWork.Projects as ProjectRepository).GetById(project.Id) != null)
                return false;

            (unitOfWork.Projects as ProjectRepository).Add(project);

            return true;
        }

        public bool DeleteProjectById(int id)
        {
            if ((unitOfWork.Projects as ProjectRepository).Count() < id - 1)
                return false;

            (unitOfWork.Projects as ProjectRepository).DeleteAt(id);

            return true;
        }

        public bool DeleteTaskById(int id)
        {
            if ((unitOfWork.Tasks as TaskRepository).Count() < id - 1)
                return false;

            (unitOfWork.Tasks as TaskRepository).DeleteAt(id);

            return true;
        }

        public bool DeleteTeamById(int id)
        {
            if ((unitOfWork.Teams as TeamRepository).Count() < id - 1)
                return false;

            (unitOfWork.Teams as TeamRepository).DeleteAt(id);

            return true;
        }

        public bool DeleteUserById(int id)
        {
            if ((unitOfWork.Users as UserRepository).Count() < id - 1)
                return false;

            (unitOfWork.Users as UserRepository).DeleteAt(id);

            return true;
        }
    }
}

