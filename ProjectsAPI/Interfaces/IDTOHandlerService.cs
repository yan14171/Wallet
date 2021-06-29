using Projects.Modelling.DTOs;

namespace Projects.API.Services
{
    public interface IDTOHandlerService
    {
        bool AddProject(Project project);
        bool AddTask(Task task);
        bool AddTeam(Team team);
        bool AddUser(User user);
        bool TryAddProject(Project project);
        bool TryAddTask(Task task);
        bool TryAddTeam(Team team);
        bool TryAddUser(User user);
        bool DeleteProjectById(int id);
        bool DeleteTaskById(int id);
        bool DeleteTeamById(int id);
        bool DeleteUserById(int id);
    }
}