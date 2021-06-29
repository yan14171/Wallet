using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projects.API.Interfaces;
using Projects.API.Services;
using Projects.Modelling.Entities;
using Projects.Modelling.DTOs;

namespace Tasks.API.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly IEntityHandlerService entityHandler;
        private readonly IDTOHandlerService dtoHandler;

        public TasksController(IEntityHandlerService entityHandler, IDTOHandlerService dtoHandler)
        {
            this.entityHandler = entityHandler;
            this.dtoHandler = dtoHandler;
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
        public IActionResult Post([FromBody] Projects.Modelling.DTOs.Task Task)
        {
            if (dtoHandler.TryAddTask(Task))
                return Created("", Task);

            else return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Put(int id, [FromBody] Projects.Modelling.DTOs.Task Task)
        {
            if (id < 0)
                return BadRequest();

            if (Task.Id != id)
                Task.Id = id;

            dtoHandler
                .DeleteTaskById(id);

            dtoHandler
                .AddTask(Task);

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
                .DeleteTaskById(id);

            return NoContent();
        }
    }
}
