using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FullStackAuth_WebAPI.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
       
        [Required]
        public int AdherenceToAgreement { get; set; }
        
        public int? WorkQuality { get; set; }
        
        public int Timeliness { get; set; }
        
        public int LikelinessOfRepeat { get; set; }
        
        public int Communication { get; set; }
        
        public  int? AdaptabilityRate { get; set; }
        
        public string? ReviewedUserId {  get; set; }   // Even though Reviews stores pointers to Users, this makes querying easier.

        public List<User> Users { get; } = new();
       



        //[ForeignKey("User")]
        //public string ReviewedUserId { get; set; }
        //public User User { get; set; }

        //[ForeignKey("User")]
        //public string ReviewMakerId { get; set; }
        //public User ReviewMaker { get; set; }

    }
}
