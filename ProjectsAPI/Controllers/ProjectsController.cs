using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Projects.API.Interfaces;
using Projects.API.Services;
using Projects.Modelling.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IEntityHandlerService entityHandler;

        public ProjectsController(IEntityHandlerService entityHandler)
        {
            this.entityHandler = entityHandler;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProjectEntity>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get()
        {
           var projects = await entityHandler.GetAllProjectEntitiesAsync();

            if (projects.Count() < 1)
                return NoContent();

            return Ok(projects);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectEntity))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetByID(int id)
        {
            var project = await entityHandler.GetProjectEntitybyIdAsync(id);

            if (project == null)
                return NoContent();

            return Ok(project);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProjectEntity))]
        public IActionResult Post([FromBody] ProjectEntity project)
        {
            if (entityHandler.AddProject(project))
                return Created("", project);

            else return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Put([FromQuery]int id, [FromBody] ProjectEntity project)
        {
            if (id < 0)
                return BadRequest();

            if(project.Id != id)
            project.Id = id;
            
            entityHandler
                .DeleteProjectById(id);

            entityHandler
                .AddProject(project);

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Delete([FromQuery]int id)
        {
            if (id < 0)
                return BadRequest();

            entityHandler
                .DeleteProjectById(id);

            return NoContent();
        }
    }
}
