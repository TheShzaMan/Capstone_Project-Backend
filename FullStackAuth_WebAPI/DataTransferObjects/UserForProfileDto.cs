using System.ComponentModel.DataAnnotations;

namespace FullStackAuth_WebAPI.DataTransferObjects
{
    public class UserForProfileDto
    {
        //DTO used when displaying User linked with FK
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsWorker { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber {  get; set; }
        public string Area { get; set; }
        public string SkillLevel { get; set; }
        public string Availability { get; set; }
        public double PayPerHour { get; set; }
        public string Experience { get; set; }
        public string BusinessDescription { get; set; }
        public bool? IsAvailNow { get; set; }
        //public List<DisplayReviewSummaryDto> ReviewsOfUser { get; set; }
    }
}
