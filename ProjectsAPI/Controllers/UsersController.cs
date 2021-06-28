using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projects.API.Interfaces;
using Projects.API.Services;
using Projects.Modelling.Entities;

namespace Users.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IEntityHandlerService entityHandler;
        private readonly IQueryProcessingService queryProcessor;

        public UsersController(IEntityHandlerService entityHandler, IQueryProcessingService queryProcessor)
        {
            this.entityHandler = entityHandler;
            this.queryProcessor = queryProcessor;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserEntity>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get()
        {
            var Users = await entityHandler.GetAllUserEntitiesAsync();

            if (Users.Count() < 1)
                return NoContent();

            return Ok(Users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserEntity))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetByID(int id)
        {
            var User = await entityHandler.GetUserEntitybyIdAsync(id);

            if (User == null)
                return NoContent();

            return Ok(User);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserEntity))]
        public IActionResult Post([FromBody] UserEntity User)
        {
            if (entityHandler.AddUser(User))
                return Created("", User);

            else return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Put([FromQuery] int id, [FromBody] UserEntity User)
        {
            if (id < 0)
                return BadRequest();

            if (User.Id != id)
                User.Id = id;

            entityHandler
                .DeleteUserById(id);

            entityHandler
                .AddUser(User);

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
                .DeleteUserById(id);

            return NoContent();
        }
    }
}
