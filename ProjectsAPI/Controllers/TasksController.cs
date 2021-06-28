﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectsAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsAPI.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
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
        public IActionResult Post([FromBody] TaskEntity Task)
        {
            return Created("", Task);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromQuery]int id, [FromBody] TaskEntity Task)
        {
            return Ok(Task);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch([FromQuery]int id, [FromBody] TaskEntity Task)
        {
            return Ok(Task);
        }
    }
}