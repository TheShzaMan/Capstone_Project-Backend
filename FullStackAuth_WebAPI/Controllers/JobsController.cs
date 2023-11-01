using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Identity.Client;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
                //var availJobsWithUser = _context.Jobs
                //    .Include(aj => aj.JobUsers)
                //    .ThenInclude(ju => ju.User).ToList();
                var availJobsWithUser = _context.Jobs.Include(aj => aj.Users).Where(aj => aj.Users.Count() < 2)
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
                            Id = aj.Users.FirstOrDefault().Id,
                            Name = aj.Users.FirstOrDefault().FirstName,
                            UserName = aj.Users.FirstOrDefault().UserName
                        }
                    }).ToList();

                //var jobsWithUserDto = availJobsWithUser
                //    .Select(aj => new DisplayJobWithUserDto
                //    {
                //        Id = aj.Id,
                //        Location = aj.Location,
                //        JobName = aj.JobName,
                //        SkillLevel = aj.SkillLevel,
                //        JobDescription = aj.JobDescription,
                //        PayPerHour = aj.PayPerHour,
                //        PostedByUser = new UserForDisplayDto
                //        {
                //            Id = aj.PostingUserId,
                //            Name = aj.JobUsers.FirstOrDefault()?.User.FirstName,  
                //            UserName = aj.JobUsers.FirstOrDefault()?.User.UserName
                //                //  -The .FirstOrDefault() is how to break a collection down to a single element 
                //                // in order to acccess its properties.
                //                //  -The question marks are conditionals for error handling. If the method
                //                // before it is null, it will return null instead of continuing until error. 
                //        }
                   // }).ToList() ;
                
                return StatusCode(200, availJobsWithUser);                        
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

                
                var postingUser = _context.Users.Find(userId);
               
                if (postingUser.IsWorker)
                {
                    return Unauthorized();
                }


                var newJobPost = new Job
                {
                    Location = data.Location,
                    JobName = data.JobName,
                    SkillLevel = data.SkillLevel,
                    JobDescription = data.JobDescription,
                    PayPerHour = data.PayPerHour,
                    Users = new List<User> { postingUser }
                };
               
                _context.Jobs.Add(newJobPost);
                
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.SaveChanges();
              
                var newJobAsDto = new DisplayJobWithUserDto
                {
                    Id = newJobPost.Id,
                    Location = newJobPost.Location,
                    JobName = newJobPost.JobName,
                    SkillLevel = newJobPost.SkillLevel,
                    JobDescription = newJobPost.JobDescription,
                    PayPerHour = newJobPost.PayPerHour,
                    PostedByUser = new UserForDisplayDto
                    {
                        Id = userId,
                        Name = postingUser.FirstName,
                        UserName = postingUser.UserName
                    }
                };
                return StatusCode(201, newJobAsDto);
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

            // ** Edit an active job ** \\

        // PUT api/jobs/5
        [HttpPut("{id}"), Authorize]
        public IActionResult Put(int id, [FromBody] Job data)
        {
            try
            {
                Job job = _context.Jobs.Include(j => j.Users).FirstOrDefault(j => j.Id == id);

                if (job == null)
                {
                    return NotFound();
                }
                
                
                var userId = User.FindFirstValue("id");
                if (string.IsNullOrEmpty(userId) || job.Users.FirstOrDefault().Id != userId)
                {
                    return Unauthorized();
                }
                User user = _context.Users.Find(userId);
                if (user.IsWorker)
                {
                    return Unauthorized();
                }

                job.Location = data.Location;
                job.JobName = data.JobName;
                job.SkillLevel = data.SkillLevel;
                job.JobDescription = data.JobDescription;
                job.PayPerHour = data.PayPerHour;
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.SaveChanges();

                var updatedJobAsDto = new DisplayJobWithUserDto
                {
                    Id = job.Id,
                    Location = job.Location,
                    JobName = job.JobName,
                    SkillLevel = job.SkillLevel,
                    JobDescription = job.JobDescription,
                    PayPerHour = job.PayPerHour,
                    PostedByUser = new UserForDisplayDto
                    {
                        Id = userId,
                        Name = user.FirstName,
                        UserName = user.UserName,
                    }
                };

                return StatusCode(201, updatedJobAsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

            // ** Delete an active job ** \\

        // DELETE api/jobs/5
        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                var jobToDelete = _context.Jobs.Find(id);
                if (jobToDelete == null)
                {
                    return NotFound();
                }

                var userId = User.FindFirstValue("id");
                if (string.IsNullOrEmpty(userId) || jobToDelete.Users.FirstOrDefault().Id != userId)
                {
                    return Unauthorized();
                }

                _context.Jobs.Remove(jobToDelete);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
