using KasetMore.ApplicationCore.Models;
using KasetMore.Data.Models;
using Microsoft.AspNetCore.Http;

namespace KasetMore.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDto?> GetUserByEmail(string email);
        Task<List<User>> GetUserByUserType(string userType, string? flag = null);
        Task<User?> AuthenticateUser(string email);
        Task Register(RegisterModel user);
        Task UpdateProfile(UserDto userRequest);
        Task UpdateProfilePicture(IFormFile profilePicture, string email);
        Task UpdateVerifyFlag(string email, string flag);
    }
}