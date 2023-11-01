using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/reviews
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // ** Get User profile with Review summary by reviewedUser ** \\

        // GET api/Reviews/profile/{reviewedUserId}
        [HttpGet("profile/{reviewedUserid}"), Authorize]
        public IActionResult GetUserProfileWithReviewSummary(string reviewedUserId)
        {
            try
            {
                string userId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var reviewedUser = _context.Users.Find(reviewedUserId);

                var reviewsOfReviewedUser = _context.Reviews.Where(r => r.ReviewSubjectId == reviewedUserId).ToList();
                    
                    
                    //.Include(r => r.Users).Where(r => r.ReviewSubjectId == r.Users.FirstOrDefault().Id);
                //.Where(r => r.ReviewUsers.FirstOrDefault().UserId.Equals(reviewedUserid));

                return Ok(reviewsOfReviewedUser);     //placed for testing purpose. Continue with building the dto               

                var newResultsSummaryWithUserDto = new UserProfileWithReviewSummaryDto(); //needs to be mapped out
                return Ok(newResultsSummaryWithUserDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // ** Post a New Review ** \\

        // POST api/Reviews/{reveiwedUserId}
        [HttpPost("{reviewedUserId}"), Authorize]
        public IActionResult Post(string reviewedUserId, [FromBody] Review data)
        {
            try
            {
                string userId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }
                

                var reviewCreator = _context.Users.Find(userId);
                
                var reviewedUser = _context.Users.Find(reviewedUserId);

                var newReview = new Review
                {
                    AdherenceToAgreement = data.AdherenceToAgreement,
                    WorkQuality = data.WorkQuality,
                    Timeliness = data.Timeliness,
                    LikelinessOfRepeat = data.LikelinessOfRepeat,
                    Communication = data.Communication,
                    AdaptabilityRate = data.AdaptabilityRate,
                    ReviewSubjectId = reviewedUserId,
                    Users = new List<User> { reviewedUser, reviewCreator }
                };
                _context.Reviews.Add(newReview);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.SaveChanges();

                //var reviewUserCreator = new ReviewUser() { User = reviewCreator, Review = data, IsCreator = true };
                //_context.ReviewUsers.Add(reviewUserCreator);
                //var reviewUserSubject = new ReviewUser() { User = reviewedUser, Review = data, IsCreator = false };
                //_context.ReviewUsers.Add(reviewUserSubject);
                //_context.SaveChanges();

                var newReviewAsDto = new ReviewAsDto
                {
                    Id = newReview.Id,
                    AdherenceToAgreement = newReview.AdherenceToAgreement,
                    WorkQuality = newReview.WorkQuality,
                    Timeliness = newReview.Timeliness,
                    LikelinessOfRepeat = newReview.LikelinessOfRepeat,
                    Communication = newReview.Communication,
                    AdaptabilityRate = newReview.AdaptabilityRate,
                    UserReviewed = new UserForDisplayDto
                    {
                        Id = reviewedUser.Id,
                        Name = reviewedUser.FirstName,
                        UserName = reviewedUser.UserName
                    },
                    ReviewCreator = new UserForDisplayDto
                    { 
                        Id = reviewCreator.Id,
                        Name = reviewCreator.FirstName,
                        UserName = reviewCreator.UserName
                    }
                };
                return StatusCode(201, newReviewAsDto);
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
               

        // DELETE api/reviews/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
