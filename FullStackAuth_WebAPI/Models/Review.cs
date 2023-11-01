using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using FullStackAuth_WebAPI.DataTransferObjects;
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
        
        public string? ReviewSubjectId {  get; set; }

        public virtual ICollection<User> Users { get; set; }
        //public virtual ICollection<ReviewUser> ReviewUsers { get; set; }



        //[ForeignKey("User")]
        //public string ReviewedUserId { get; set; }
        //public User User { get; set; }

        //[ForeignKey("User")]
        //public string ReviewMakerId { get; set; }
        //public User ReviewMaker { get; set; }

    }
}
