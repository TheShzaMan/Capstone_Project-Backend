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

        // ** Get list of all Jobs ** \\

        // GET: api/Users 
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
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
                    SkillLevel  = u.SkillLevel,
                    Availability = u.Availability,
                    PayPerHour = u.PayPerHour,  
                    Experience = u.Experience,
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
    }
}