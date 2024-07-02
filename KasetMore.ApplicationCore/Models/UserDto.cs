

namespace KasetMore.ApplicationCore.Models
{
    public class UserDto
    {
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DisplayName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? IsVerified { get; set; }
        public string? UserType { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
