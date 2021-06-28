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
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
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
