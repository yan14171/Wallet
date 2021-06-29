using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projects.API.Services;
using Projects.Modelling.Entities;
using Projects.API.Interfaces;
using Projects.Modelling.DTOs;

namespace Users.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IEntityHandlerService entityHandler;
        private readonly IDTOHandlerService dtoHandler;

        public UsersController(IEntityHandlerService entityHandler, IDTOHandlerService dtoHandler)
        {
            this.entityHandler = entityHandler;
            this.dtoHandler = dtoHandler;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserEntity>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var Users = await entityHandler.GetAllUserEntitiesAsync();

            if (Users.Count() < 1)
                return NotFound();

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
        public IActionResult Post([FromBody] User User)
        {
            if (dtoHandler.TryAddUser(User))
                return Created("", User);

            else return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(int id, [FromBody] User User)
        {
            if (id < 0)
                return BadRequest();

            if (User.Id != id)
                User.Id = id;

            dtoHandler
                .DeleteUserById(id);

            dtoHandler
                .AddUser(User);

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
                .DeleteUserById(id);

            return NoContent();
        }
    }
}
