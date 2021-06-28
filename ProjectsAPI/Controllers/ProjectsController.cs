using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Projects.API.Interfaces;
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
        private readonly IQueryProcessingService queryProcessor;

        public ProjectsController(IEntityHandlerService entityHandler, IQueryProcessingService queryProcessor)
        {
            this.entityHandler = entityHandler;
            this.queryProcessor = queryProcessor;
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
        public IActionResult GetByID([FromQuery] int id)
        {
            return Ok(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProjectEntity project)
        {
            return Created("", project);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromQuery]int id, [FromBody] ProjectEntity project)
        {
            return Ok(project);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch([FromQuery]int id, [FromBody] ProjectEntity project)
        {
            return Ok(project);
        }
    }
}
