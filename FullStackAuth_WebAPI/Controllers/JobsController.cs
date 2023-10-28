using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public JobsController(ApplicationDbContext context)
        {
            _context = context;
        }

                       // Get all available jobs using DisplayJobWithUserDto
        // GET: api/jobs
        [HttpGet]
        public IActionResult GetAvailJobs()
        {
            try
            {
                var availableJobs = _context.Jobs
                    .Include(job => job.Users)
                    .Where(job => job.Users.Count < 2)
                    .Select(job => new DisplayJobWithUserDto
                    {
                        Location = job.Location,
                        JobName = job.JobName,
                        SkillLevel = job.SkillLevel,
                        JobDescription = job.JobDescription,
                        PayPerHour = job.PayPerHour,
                        PostedByUser = (UserForDisplayDto)job.Users
                        .Select(u => new UserForDisplayDto
                        {
                            Id = u.Id,
                            Name = u.FirstName,
                            UserName = u.UserName,
                        }),
                    }).ToList();
               
                return StatusCode(201, availableJobs);                        
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/jobs/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/jobs
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/jobs/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/jobs/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
