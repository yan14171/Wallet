using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projects.API.Interfaces;
using Projects.API.Services;
using Projects.Modelling.Entities;

namespace Tasks.API.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly IEntityHandlerService entityHandler;
        private readonly IQueryProcessingService queryProcessor;

        public TasksController(IEntityHandlerService entityHandler, IQueryProcessingService queryProcessor)
        {
            this.entityHandler = entityHandler;
            this.queryProcessor = queryProcessor;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TaskEntity>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get()
        {
            var Tasks = await entityHandler.GetAllTaskEntitiesAsync();

            if (Tasks.Count() < 1)
                return NoContent();

            return Ok(Tasks);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TaskEntity))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetByID(int id)
        {
            var Task = await entityHandler.GetTaskEntitybyIdAsync(id);

            if (Task == null)
                return NoContent();

            return Ok(Task);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TaskEntity))]
        public IActionResult Post([FromBody] TaskEntity Task)
        {
            if (entityHandler.AddTask(Task))
                return Created("", Task);

            else return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Put([FromQuery] int id, [FromBody] TaskEntity Task)
        {
            if (id < 0)
                return BadRequest();

            if (Task.Id != id)
                Task.Id = id;

            entityHandler
                .DeleteTaskById(id);

            entityHandler
                .AddTask(Task);

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Delete([FromQuery] int id)
        {
            if (id < 0)
                return BadRequest();

            entityHandler
                .DeleteTaskById(id);

            return NoContent();
        }
    }
}
