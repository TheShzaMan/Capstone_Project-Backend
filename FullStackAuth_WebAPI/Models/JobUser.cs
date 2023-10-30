namespace FullStackAuth_WebAPI.Models
{
    public class JobUser
    {
        public int JobId { get; set; }
        public Job Job { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
