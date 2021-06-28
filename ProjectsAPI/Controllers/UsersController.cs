using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectsAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetByID([FromQuery] int id)
        {
            return Ok(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserEntity User)
        {
            return Created("", User);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromQuery]int id, [FromBody] UserEntity User)
        {
            return Ok(User);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch([FromQuery]int id, [FromBody] UserEntity User)
        {
            return Ok(User);
        }
    }
}
