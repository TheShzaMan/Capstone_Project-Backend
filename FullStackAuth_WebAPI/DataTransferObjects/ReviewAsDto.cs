using FullStackAuth_WebAPI.Models;

namespace FullStackAuth_WebAPI.DataTransferObjects
{
    public class ReviewAsDto
    {
        public int Id { get; set; }
        public int AdherenceToAgreement { get; set; }

        public int? WorkQuality { get; set; }

        public int Timeliness { get; set; }

        public int LikelinessOfRepeat { get; set; }

        public int Communication { get; set; }

        public int? AdaptabilityRate { get; set; }

        public UserForDisplayDto UserReviewed { get; set; }
        public UserForDisplayDto ReviewCreator { get; set; }
        
    }
}   
