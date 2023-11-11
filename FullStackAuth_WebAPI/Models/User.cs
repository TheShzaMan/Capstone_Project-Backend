using Microsoft.AspNetCore.Identity;


namespace FullStackAuth_WebAPI.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsWorker { get; set; }
        public string Area { get; set; }
        public string SkillLevel { get; set; }
        public string? Availability { get; set; }
        public double PayPerHour { get; set; }
        public string? BusinessDescription { get; set; }
        public bool? IsAvailNow { get; set; }

        // public virtual ICollection<ReviewUser> ReviewUsers { get; set; }
        // public virtual ICollection<JobUser> JobUsers { get; set; }
        public List<Review> Reviews { get; } = new();
        public List<Job> Jobs { get; } = new();

    }
}
