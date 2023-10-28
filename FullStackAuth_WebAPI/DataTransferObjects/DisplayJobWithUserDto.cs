using System.ComponentModel.DataAnnotations;

namespace FullStackAuth_WebAPI.DataTransferObjects
{
    public class DisplayJobWithUserDto
    {
        public string Location { get; set; }
        public string JobName { get; set; }
        public string SkillLevel { get; set; }
        public string JobDescription { get; set; }
        public double PayPerHour { get; set; }
       

        public UserForDisplayDto PostedByUser { get; set; }
    }
}
