using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ** Get list of all Users ** \\

        // GET: api/Users 
        [HttpGet, Authorize]
        public IActionResult Get()
        {
            try
            {
                string userId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }
                var usersDtos = _context.Users.Select(u => new UserForProfileDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    IsWorker = u.IsWorker,
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Area = u.Area,
                    SkillLevel = u.SkillLevel,
                    Availability = u.Availability,
                    PayPerHour = u.PayPerHour,
                    BusinessDescription = u.BusinessDescription,
                    IsAvailNow = u.IsAvailNow,
                }).ToList();


                return StatusCode(200, usersDtos);
            }
            catch (Exception ex)
            {
                // If an error occurs, return a 500 internal server error with the error message
                return StatusCode(500, ex.Message);
            }
        }

        // ** Get User by Id ** \\

        // GET: api/Users/{id} 
        [HttpGet("{id}"), Authorize]
        public IActionResult GetById(string id)
        {
            try
            {
                string userId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var userById = _context.Users.Find(id);

                var userByIdDto = _context.Users.Select(u => new UserForProfileDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    IsWorker = u.IsWorker,
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Area = u.Area,
                    SkillLevel = u.SkillLevel,
                    Availability = u.Availability,
                    PayPerHour = u.PayPerHour,
                    BusinessDescription = u.BusinessDescription,
                    IsAvailNow = u.IsAvailNow,
                });


                return StatusCode(200, userByIdDto);
            }
            catch (Exception ex)
            {
                // If an error occurs, return a 500 internal server error with the error message
                return StatusCode(500, ex.Message);
            }
        }

        // ** Get list of Users that have applied to Job ** \\

        // GET: api/Users/appliedto/{jobId} 
        [HttpGet("{jobId}"), Authorize]
        public IActionResult GetUsersThatApplied(int jobId)
        {
            try
            {
                string userId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }
               
                var job = _context.Jobs.Include(j => j.Users).SingleOrDefault(j => j.Id == jobId); 

                if (job.PostedByUserId != userId)
                {
                    return Unauthorized("User Id does not match postedByUser Id");
                }

                var appliedUsers = job.Users.Select(u => new UserForProfileDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    IsWorker = u.IsWorker,
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Area = u.Area,
                    SkillLevel = u.SkillLevel,
                    Availability = u.Availability,
                    PayPerHour = u.PayPerHour,
                    BusinessDescription = u.BusinessDescription,
                    IsAvailNow = u.IsAvailNow,
                }).ToList();


                return StatusCode(200, appliedUsers);  //NEED TO TEST
            }
            catch (Exception ex)
            {
                // If an error occurs, return a 500 internal server error with the error message
                return StatusCode(500, ex.Message);
            }
        }
        // ** Edit User Profile ** \\

        // PUT api/Users/{userId}
        [HttpPut("{id}"), Authorize]
        public IActionResult Put(string id, [FromBody] User updatedUser)
        {
            try
            {
                string userId = User.FindFirstValue("id");
                if (string.IsNullOrEmpty(userId) || userId != id)
                {
                    return Unauthorized();
                }

                User thisUser = _context.Users.SingleOrDefault(u => u.Id == id);
                
                if (updatedUser.PhoneNumber != null){ thisUser.PhoneNumber = updatedUser.PhoneNumber; }
                if (updatedUser.SkillLevel != null) { thisUser.SkillLevel= updatedUser.SkillLevel; }
                if (updatedUser.Availability != null ) { thisUser.Availability= updatedUser.Availability; }
                if (updatedUser.PayPerHour != 0) { thisUser.PayPerHour= updatedUser.PayPerHour; }
                if (updatedUser.BusinessDescription != null ) { thisUser.BusinessDescription= updatedUser.BusinessDescription; }

                

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.SaveChanges();

                var updatedProfileAsDto = new UserForProfileDto
                {
                    Id = thisUser.Id,
                    FirstName = thisUser.FirstName,
                    LastName = thisUser.LastName,
                    UserName = thisUser.UserName,
                    Email = thisUser.Email,
                    PhoneNumber = thisUser.PhoneNumber,
                    Area = thisUser.Area,
                    SkillLevel = thisUser.SkillLevel,
                    Availability = thisUser.Availability,
                    PayPerHour = thisUser.PayPerHour,
                    BusinessDescription = thisUser.BusinessDescription,
                    IsAvailNow = thisUser.IsAvailNow,
                };
                return StatusCode(201, updatedProfileAsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        // ** Edit User isAvailNow ** \\

        // PUT api/Users/{userId}
        [HttpPut("avail/{id}"), Authorize]
        public IActionResult EditIsAvailNow(string id, [FromBody] User isAvailNowStatus)
        {
            try
            {
                string userId = User.FindFirstValue("id");
                if (string.IsNullOrEmpty(userId) || userId != id)
                {
                    return Unauthorized();
                }

                User thisUser = _context.Users.SingleOrDefault(u => u.Id == id);

                thisUser.IsAvailNow = isAvailNowStatus.IsAvailNow;
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.SaveChanges();
                return StatusCode(201, thisUser.IsAvailNow);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // ** Delete a user for testing purpose ** \\

        // DELETE api/Users/{id}
        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(string id)
        {
            try
            {
                var userToDelete = _context.Users.Find(id);
                if (userToDelete == null)
                {
                    return NotFound();
                }

                var userId = User.FindFirstValue("id");
                if (string.IsNullOrEmpty(userId)) 
                {
                    return Unauthorized();
                }

                _context.Users.Remove(userToDelete);
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