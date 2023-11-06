using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
                
              //    return Ok(reviewsOfReviewedUser);     //placed for testing purpose. Continue with building the dto               
                var reviewTotalScore = new List<int>();
                reviewTotalScore.Add(reviewsOfReviewedUser.Sum(r => r.AdherenceToAgreement));
                reviewTotalScore.Add(reviewsOfReviewedUser.Sum(r => r.Timeliness));
                reviewTotalScore.Add(reviewsOfReviewedUser.Sum(r => r.LikelinessOfRepeat));
                reviewTotalScore.Add(reviewsOfReviewedUser.Sum(r => r.Communication));

                var newResultsSummaryWithUserDto = new UserProfileWithReviewSummaryDto
                {
                    TotalReviewsJobs = reviewsOfReviewedUser.Count(),
                    AvgAdherence = reviewsOfReviewedUser.Average(r => r.AdherenceToAgreement),
                    AvgTimeliness = reviewsOfReviewedUser.Average(r => r.Timeliness),
                    AvgWouldRepeat = reviewsOfReviewedUser.Average(r => r.LikelinessOfRepeat),
                    AvgCommunication = reviewsOfReviewedUser.Average(r => r.Communication),
                    IsWorker = reviewedUser.IsWorker,
                    AvgOverallScore = reviewTotalScore.Average(),
                    User = new UserForProfileDto
                    {
                        Id = userId,
                        FirstName = reviewedUser.FirstName,
                        LastName = reviewedUser.LastName,
                        IsWorker = reviewedUser.IsWorker,
                        UserName = reviewedUser.UserName,
                        PhoneNumber = reviewedUser.PhoneNumber,
                        Email = reviewedUser.Email,
                        Area = reviewedUser.Area,
                        SkillLevel = reviewedUser.SkillLevel,
                        Availability = reviewedUser?.Availability,
                        WagePerHour = reviewedUser.WagePerHour,
                        Experience = reviewedUser?.Experience,
                        BusinessDescription = reviewedUser?.BusinessDescription,
                        IsAvailNow = reviewedUser?.IsAvailNow

                    }
                }; 
                if (reviewedUser.IsWorker)
                {
                    newResultsSummaryWithUserDto.AvgQuality = reviewsOfReviewedUser.Average(r => r.WorkQuality).GetValueOrDefault();
                    newResultsSummaryWithUserDto.AvgAdaptability = reviewsOfReviewedUser.Average(r => r.AdaptabilityRate);
                }
                if (reviewsOfReviewedUser.Count() >= 1)
                {
                    return Ok(newResultsSummaryWithUserDto);
                }
                else { return Ok(newResultsSummaryWithUserDto.User); }
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

                _context.SaveChanges();

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
