using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;
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


            // ** Get all available jobs using DisplayJobWithUserDto ** \\

        // GET: api/jobs
        [HttpGet]
        public IActionResult GetAvailJobs()
        {
            try
            {
                //    var availableJobs = _context.Jobs                    
                //        .Include(j => j.JobUsers)
                //        .Where(j => j.JobUsers.Count < 2);
                //    var postingUserId = _context.JobUsers
                //        .Select(ju => ju.UserId);

                //        .Select(u => new UserForDisplayDto
                //        {
                //            Id = u.Id,
                //            Name = u.FirstName,
                //            UserName = u.UserName
                //        }).Single();

                var availJobsWithUser = _context.Jobs
                    .Include(aj => aj.JobUsers)
                    .ThenInclude(ju => ju.User).ToList();

                var jobsWithUserDto = availJobsWithUser
                    .Select(aj => new DisplayJobWithUserDto
                    {
                        Id = aj.Id,
                        Location = aj.Location,
                        JobName = aj.JobName,
                        SkillLevel = aj.SkillLevel,
                        JobDescription = aj.JobDescription,
                        PayPerHour = aj.PayPerHour,
                        PostedByUser = new UserForDisplayDto
                        {
                            Id = aj.PostingUserId,
                            Name = aj.JobUsers.FirstOrDefault()?.User.FirstName,  
                            UserName = aj.JobUsers.FirstOrDefault()?.User.UserName
                                //  -The .FirstOrDefault() is how to break a collection down to a single element 
                                // in order to acccess its properties.
                                //  -The question marks are conditionals for error handling. If the method
                                // before it is null, it will return null instead of continuing until error. 
                        }
                    }).ToList() ;
                
                return StatusCode(200, jobsWithUserDto);                        
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

            // ** Post a New Job ** \\

        // POST api/jobs
        [HttpPost, Authorize]
        public IActionResult Post([FromBody] Job data)
        {
            try
            {
                string userId = User.FindFirstValue("id");
                

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }
                User user = _context.Users.Find(userId);
                data.PostingUserId = userId;
                
                    
                //    new JobUser()
                //{ User = user, UserId = userId, Job = data };
                
               
                _context.Jobs.Add(data);
                _context.SaveChanges();
                var jobUser = new JobUser() { User = user, Job = data };
                _context.JobUsers.Add(jobUser);
                _context.SaveChanges();


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.SaveChanges();
                
                return StatusCode(201, "Job has been posted");  //check response, if 'data' sends entire User info, change this response.
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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
