using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projects.API.Services;
using Projects.Modelling.Entities;
using Projects.API.Interfaces;
using Projects.Modelling.DTOs;

namespace Teams.API.Controllers
{
    [ApiController]
    [Route("api/teams")]
    public class TeamsController : ControllerBase
    {
        private readonly IEntityHandlerService entityHandler;
        private readonly IDTOHandlerService dtoHandler;

        public TeamsController(IEntityHandlerService entityHandler, IDTOHandlerService dtoHandler)
        {
            this.entityHandler = entityHandler;
            this.dtoHandler = dtoHandler;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeamEntity>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get()
        {
            var Teams = await entityHandler.GetAllTeamEntitiesAsync();

            if (Teams.Count() < 1)
                return NoContent();

            return Ok(Teams);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeamEntity))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetByID(int id)
        {
            var Team = await entityHandler.GetTeamEntitybyIdAsync(id);

            if (Team == null)
                return NoContent();

            return Ok(Team);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TeamEntity))]
        public IActionResult Post([FromBody] Team Team)
        {
            if (dtoHandler.TryAddTeam(Team))
                return Created("", Team);

            else return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Put(int id, [FromBody] Team Team)
        {
            if (id < 0)
                return BadRequest();

            if (Team.Id != id)
                Team.Id = id;

            dtoHandler
                .DeleteTeamById(id);

            dtoHandler
                .AddTeam(Team);

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
                .DeleteTeamById(id);

            return NoContent();
        }
    }
}
