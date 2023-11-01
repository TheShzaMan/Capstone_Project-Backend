namespace FullStackAuth_WebAPI.Models
{
    public class ReviewUser
    {
        public int ReviewId { get; set; }
        public Review Review { get; set; }
       
        public string UserId { get; set; }  
        public User User { get; set; }

        public bool IsCreator { get; set; }
    }
}
