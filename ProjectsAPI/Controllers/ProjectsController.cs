using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Projects.API.Interfaces;
using Projects.API.Services;
using Projects.Modelling.DTOs;
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
        private readonly IDTOHandlerService dtoHandler;

        public ProjectsController(IEntityHandlerService entityHandler, IDTOHandlerService dtoHandler)
        {
            this.entityHandler = entityHandler;
            this.dtoHandler = dtoHandler;
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByID(int id)
        {
            var project = await entityHandler.GetProjectEntitybyIdAsync(id);

            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProjectEntity))]
        public IActionResult Post([FromBody] Project project)
        {
            if (dtoHandler.TryAddProject(project))
                return Created("", project);

            else return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(int id, [FromBody] Project project)
        {
            if (id < 0)
                return BadRequest();

            if(project.Id != id)
            project.Id = id;
            
            dtoHandler
                .DeleteProjectById(id);

            dtoHandler
                .AddProject(project);

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Delete(int id)
        {
            if (id < 0)
                return BadRequest();

            dtoHandler
                .DeleteProjectById(id);

            return NoContent();
        }
    }
}
