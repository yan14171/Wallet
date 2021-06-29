using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projects.API.Interfaces;
using Projects.Modelling.DTOs;
using System.Threading.Tasks;

namespace Projects.API.Controllers
{
    [ApiController]
    [Route("api/misc")]
    public class MiscellaneousController : ControllerBase
    {
        private readonly IQueryProcessingService queryHandler;

        public MiscellaneousController(IQueryProcessingService queryHandler)
        {
            this.queryHandler = queryHandler;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("tasksQuantity/{id}")]
        public async Task<IActionResult> GetTasksQuantityPerProject(int id)
        {
            var QuantityByProject = await queryHandler.GetTasksQuantityPerProjectAsync(id);

            return Ok(QuantityByProject);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("tasks/{id}")]
        public async Task<IActionResult> GetTasksPerPerformer(int id)
        {
            var TasksPerPerfomer = await queryHandler.GetTasksPerPerformerAsync(id);

            return Ok(TasksPerPerfomer);
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("tasksThisYear/{id}")]
        public async Task<IActionResult> GetTasksPerPerformerThisYear(int id)
        {
            var TasksThisYear = await queryHandler.GetTasksPerPerformerFinishedThisYearAsync(id);

            return Ok(TasksThisYear);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("oldestTeams")]
        public async Task<IActionResult> GetOldestTeams()
        {
            var OldestTeams = await queryHandler.GetOldestTeamsAsync();

            return Ok(OldestTeams);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("tasksAlpha")]
        public async Task<IActionResult> GetTasksPerPerformerAlphabetically()
        {
            var TasksPerPerfomer = await queryHandler.GetTasksPerPerformerAlphabeticallyAsync();

            return Ok(TasksPerPerfomer);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("userInfo/{id}")]
        public async Task<IActionResult> GetUserInfo(int id)
        {
            UserInfo userInfo;

            try
            {
             userInfo = await queryHandler.GetUserInfoAsync(id);
            }
            catch
            {
                return NotFound("No projects with this author");
            }

            return Ok(userInfo);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("projectsInfo")]
        public async Task<IActionResult> GetProjectsInfo()
        {
            var projectInfo = await queryHandler.GetProjectsInfoAsync();

            return Ok(projectInfo);
        }
    }
}
