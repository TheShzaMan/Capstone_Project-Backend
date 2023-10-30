using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using FullStackAuth_WebAPI.DataTransferObjects;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FullStackAuth_WebAPI.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string JobName { get; set; }

        [Required]
        public string SkillLevel { get; set; }

        [Required]
        public string JobDescription { get; set; }

        [Required]
        public double PayPerHour { get; set; }
        public string? PostingUserId { get; set; }

        //public User postingUser { get; set; }
       
        public virtual ICollection<JobUser> JobUsers { get; set; }



        //[ForeignKey("User")]
        //public string PostedByUserId { get; set; }
        //public User PostedByUser { get; set; }

        //[ForeignKey("User")]
        //public string? AcceptedByUserId { get; set; }
        //public User AcceptedByUser { get; set; }

    }
}
