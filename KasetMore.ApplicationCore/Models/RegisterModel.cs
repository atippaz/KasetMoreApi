using Microsoft.AspNetCore.Http;

namespace KasetMore.ApplicationCore.Models
{
    public class RegisterModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public string Address { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string? UserType { get; set; }
    }
}
