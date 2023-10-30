using System.ComponentModel.DataAnnotations;

namespace FullStackAuth_WebAPI.DataTransferObjects
{
    public class UserForRegistrationDto
    {
        //DTO used when registering
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }
        
        
        public string? LastName { get; set; }
        
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        
        public string? Email { get; set; }
        
        public string? PhoneNumber { get; set; }
        
        [Required(ErrorMessage = "Must select user type")]
        public bool IsWorker {  get; set; }
       
        public string? Availability { get; set; }

        
        public double WagePerHour { get; set; }
        
        public string? Experience { get; set; }
        
        public string? BusinessDescription { get; set; }     






    }
}
