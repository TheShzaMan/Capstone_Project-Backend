using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
                var reviewsOfReviewedUser = _context.Reviews.Where(r => r.ReviewedUserId == reviewedUserId).ToList();               
                var userReviewCount = reviewsOfReviewedUser.Count();
                if (userReviewCount == 0)
                {
                    var userNoReviews = new UserForProfileDto
                    {
                        Id = reviewedUserId,
                        FirstName = reviewedUser.FirstName,
                        LastName = reviewedUser.LastName,
                        IsWorker = reviewedUser.IsWorker,
                        UserName = reviewedUser.UserName,
                        PhoneNumber = reviewedUser.PhoneNumber,
                        Email = reviewedUser.Email,
                        Area = reviewedUser.Area,
                        SkillLevel = reviewedUser.SkillLevel,
                        Availability = reviewedUser?.Availability,
                        PayPerHour = reviewedUser.PayPerHour,
                        BusinessDescription = reviewedUser?.BusinessDescription,
                        IsAvailNow = reviewedUser?.IsAvailNow,
                        TotalReviews = userReviewCount

                    };
                    return Ok(userNoReviews); 
                }
                

                var reviewTotalScore = new List<int>();
                reviewTotalScore.Add(reviewsOfReviewedUser.Sum(r => r.AdherenceToAgreement));
                reviewTotalScore.Add(reviewsOfReviewedUser.Sum(r => r.Timeliness));
                reviewTotalScore.Add(reviewsOfReviewedUser.Sum(r => r.LikelinessOfRepeat));
                reviewTotalScore.Add(reviewsOfReviewedUser.Sum(r => r.Communication));
                
                var reviewTotalsAsDouble = reviewTotalScore.Select(rts => (double)rts).ToList(); // Using .toFixed(1) function in React frontend to only display one decimal place 
                var avgOverall = reviewTotalsAsDouble.Sum() / (reviewTotalsAsDouble.Count() * reviewsOfReviewedUser.Count());
                
                var newResultsSummaryWithUserDto = new UserProfileWithReviewSummaryDto
                {
                    TotalReviewsJobs = userReviewCount,
                    AvgAdherence = reviewsOfReviewedUser.Average(r => r.AdherenceToAgreement),
                    AvgTimeliness = reviewsOfReviewedUser.Average(r => r.Timeliness),
                    AvgWouldRepeat = reviewsOfReviewedUser.Average(r => r.LikelinessOfRepeat),
                    AvgCommunication = reviewsOfReviewedUser.Average(r => r.Communication),
                    AvgOverallScore = avgOverall,                  
                    User = new UserForProfileDto
                    {
                        Id = reviewedUserId,
                        FirstName = reviewedUser.FirstName,
                        LastName = reviewedUser.LastName,
                        IsWorker = reviewedUser.IsWorker,
                        UserName = reviewedUser.UserName,
                        PhoneNumber = reviewedUser.PhoneNumber,
                        Email = reviewedUser.Email,
                        Area = reviewedUser.Area,
                        SkillLevel = reviewedUser.SkillLevel,
                        Availability = reviewedUser?.Availability,
                        PayPerHour = reviewedUser.PayPerHour,
                        BusinessDescription = reviewedUser?.BusinessDescription,
                        IsAvailNow = reviewedUser?.IsAvailNow,
                        TotalReviews = userReviewCount
                    }
                }; 
                if (reviewedUser.IsWorker)
                {
                    newResultsSummaryWithUserDto.AvgQuality = reviewsOfReviewedUser.Average(r => r.WorkQuality).GetValueOrDefault();
                    newResultsSummaryWithUserDto.AvgAdaptability = reviewsOfReviewedUser.Average(r => r.AdaptabilityRate);
                }              
               
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

                data.ReviewedUserId = reviewedUserId;
                data.Users.Add(reviewedUser);
                data.Users.Add(reviewCreator);
                
                _context.Reviews.Add(data);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.SaveChanges();

               

                var newReviewAsDto = new ReviewAsDto
                {
                    
                    AdherenceToAgreement = data.AdherenceToAgreement,
                    WorkQuality = data.WorkQuality,
                    Timeliness = data.Timeliness,
                    LikelinessOfRepeat = data.LikelinessOfRepeat,
                    Communication = data.Communication,
                    AdaptabilityRate = data.AdaptabilityRate,
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
