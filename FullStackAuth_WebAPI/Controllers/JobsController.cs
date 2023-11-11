using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Linq.Expressions;
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

        // GET: api/Jobs/avail
        [HttpGet("avail")]
        public IActionResult GetAvailJobs()
        {
            try
            {
                // var postedByUser = _context.Users.Include(u => u.Jobs).FirstOrDefault(u => u.Id == u.Jobs.FirstOrDefault()  
                var availJobsWithUser = _context.Jobs.Include(aj => aj.Users)
                    .Where(aj => aj.IsFulfilled == false)
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
                            Id = aj.PostedByUserId,
                            Name = aj.Users.First(u => u.Id == aj.PostedByUserId).FirstName,
                            UserName = aj.Users.SingleOrDefault(u => u.Id == aj.PostedByUserId).UserName,
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

        // ** Get list of all Jobs ** \\

        // GET: api/Jobs
        [HttpGet]
        public IActionResult GetAllJobs()
        {
            try
            {
                var jobs = _context.Jobs.Include(j => j.Users).Select(j => new DisplayJobWithUserDto
                {
                    Id = j.Id,
                    Location = j.Location,
                    JobName = j.JobName,
                    SkillLevel = j.SkillLevel,
                    JobDescription = j.JobDescription,
                    PayPerHour = j.PayPerHour,
                    PostedByUser = new UserForDisplayDto
                    {
                        Id = j.PostedByUserId,
                        Name = j.Users.First().FirstName,
                        UserName = j.Users.First().UserName,
                    },
                    AcceptedByUser = new UserForDisplayDto
                    {
                        UserName = j.Users.FirstOrDefault(u => u.Id != j.PostedByUserId).UserName
                    },
                }).ToList();

                return StatusCode(200, jobs);
            }
            catch (Exception ex)
            {
                // If an error occurs, return a 500 internal server error with the error message
                return StatusCode(500, ex.Message);
            }
        }

        // ** Get job with postingUser profile by job id ** \\

        // GET api/Jobs/5
        [HttpGet("{jobId}"), Authorize]
        public IActionResult Get(int jobId)
        {
            try
            {
                string userId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }
                var job = _context.Jobs.Include(j => j.Users).SingleOrDefault(j => j.Id == jobId);
                //var job = _context.Jobs.Find(jobId);
                if (job == null)
                {
                    return NotFound();
                }
                var postingUser = job.Users.First(u => u.Id == job.PostedByUserId);
                var jobDto = new DisplayJobWithUserDto
                {
                    Id = job.Id,
                    Location = job.Location,
                    JobName = job.JobName,
                    SkillLevel = job.SkillLevel,
                    JobDescription = job.JobDescription,
                    PayPerHour = job.PayPerHour,
                    PostedByProfile = new UserForProfileDto
                    {
                        FirstName = postingUser.FirstName,
                        UserName = postingUser.UserName,
                        Email = postingUser.Email,
                        PhoneNumber = postingUser.PhoneNumber,
                        BusinessDescription = postingUser.BusinessDescription,
                    }
                };
                return StatusCode(200, jobDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        } // ** Get list of users of a job by id ** \\

        // GET api/Jobs/applied/{jobId
        [HttpGet("applied/{jobId}"), Authorize]
        public IActionResult GetAppliedUsers(int jobId)
        {
            try
            {
                string userId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }
                var job = _context.Jobs.Include(j => j.Users).SingleOrDefault(j => j.Id == jobId);
                if (job == null)
                {
                    return NotFound();
                }
                if (job.PostedByUserId != userId)
                {
                    return Unauthorized();
                }

                var postingUser = job.Users.First(u => u.Id == job.PostedByUserId);
                var appliedUsers = job.Users.Where(u => u.Id != userId).ToList();
                var appliedUsersDto = appliedUsers.Select(au => new UserForDisplayDto
                {
                    Id = au.Id,
                    Name = au.FirstName,
                    LastName = au.LastName,
                    UserName = au.UserName
                }).ToList();
                return StatusCode(200, appliedUsersDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // ** Post a New Job ** \\

        // POST api/Jobs
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

                data.PostedByUserId = postingUser.Id;
                data.Users.Add(postingUser);
                //data.Users = new List<User> { postingUser };
                
                
                _context.Jobs.Add(data);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.SaveChanges();

                var newJobAsDto = new DisplayJobWithUserDto
                {
                    Id = data.Id,
                    Location = data.Location,
                    JobName = data.JobName,
                    SkillLevel = data.SkillLevel,
                    JobDescription = data.JobDescription,
                    PayPerHour = data.PayPerHour,
                    IsFulfilled = data.IsFulfilled,
                    PostedByUser = new UserForDisplayDto
                    {
                        Id = userId,
                        Name = postingUser.FirstName,
                        UserName = postingUser.UserName,
                    }
                };
                return StatusCode(201, newJobAsDto);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // ** Worker wants to apply therefore added to list of job users** \\

        // PUT api/Jobs/apply/{jobId}
        [HttpPut("apply/{jobId}/"), Authorize]
        public IActionResult AddInterestedUser(int jobId)
        {
            try
            {
                // is registered user ?
                var userId = User.FindFirstValue("id");
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }
                // is a worker user ?
                User user = _context.Users.Find(userId);
                if (!user.IsWorker)
                {
                    return Unauthorized();
                }
                // there is in fact a job with an id that matches the id sent ?
                Job job = _context.Jobs.Include(j => j.Users).FirstOrDefault(j => j.Id == jobId);
                if (job == null)
                {
                    return NotFound();
                }

                job.Users.Add(user);


                var postedByUser = job.Users.First(u => u.Id == job.PostedByUserId);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        // need get request that will find all avail jobs that a user is the acceptedByUserId
       
        // ** Update AcceptedByUserId when job is offered ** \\

        // PUT api/Jobs/offer/{jobId}
        [HttpPut("offer/{jobId}/"), Authorize]
        public IActionResult OfferJob(int jobId, [FromBody] string offerToUserId)
        {
            try
            {
                // is registered user ?
                var userId = User.FindFirstValue("id");
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }
                // is postedByUser ?
                var job = _context.Jobs.Include(j => j.Users).FirstOrDefault(j => j.Id == jobId);
                if (job.PostedByUserId != userId)
                {
                    return Unauthorized("UserId does not match postedByUserId");
                }
                var offerToUser = _context.Users.Find(offerToUserId);

                job.AcceptedByUserId = offerToUserId;
                
                _context.SaveChanges(); 
                return Ok($"Offer has been made. Awaiting response from {offerToUser.UserName}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // ** Update isFulfilled when user accepts job ** \\

        // PUT api/Jobs/fulfill/{jobId}
        [HttpPut("fulfill/{jobId}/"), Authorize]
        public IActionResult FulfillJob(int jobId, [FromBody] bool isFulfilled)
        {
            try
            {
                var job = _context.Jobs.Include(j => j.Users).FirstOrDefault(j => j.Id == jobId);
                // is registered user ?  User id matches AcceptedByUserId ?
                var userId = User.FindFirstValue("id");
                if (string.IsNullOrEmpty(userId) || userId != job.AcceptedByUserId)
                {
                    return Unauthorized();
                }
                var user = _context.Users.Find(userId);

                job.IsFulfilled = true;
                _context.SaveChanges();

                return Ok($"User {user.UserName} has accepted the job.");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

            // ** Edit an active job ** \\

            // PUT api/Jobs/5
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

                if (data.Location != null) { job.Location = data.Location; }
                if (data.JobName != null) { job.JobName = data.JobName; }
                if (data.SkillLevel != null) { job.SkillLevel = data.SkillLevel; }
                if (data.JobDescription != null) { job.JobDescription = data.JobDescription; }
                if (data.PayPerHour != 0) { job.PayPerHour = data.PayPerHour; }
               
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
