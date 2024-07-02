using KasetMore.ApplicationCore.Extensions;
using KasetMore.ApplicationCore.Models;
using KasetMore.Data.Models;
using KasetMore.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace KasetMore.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly KasetMoreContext _context;

        public UserRepository(KasetMoreContext context)
        {
            _context = context;
        }

        public async Task<UserDto?> GetUserByEmail(string email)
        {
            return await _context.Users
                .Where(u => u.Email == email)
                .Select(u => new UserDto
                {
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    DisplayName = u.DisplayName,
                    PhoneNumber = u.PhoneNumber,
                    Address = u.Address,
                    IsVerified = u.IsVerified,
                    UserType = u.UserType,
                    ProfilePicture = u.ProfilePicture,
                })
                .FirstOrDefaultAsync();
        }
        public async Task<List<User>> GetUserByUserType(string userType, string? flag = null)
        {
            return await _context.Users
                .Where(u => u.UserType == userType)
                .WhereIf(!string.IsNullOrEmpty(flag), u => u.IsVerified == flag)
                .ToListAsync();
        }
        public async Task<User?> AuthenticateUser(string email)
        {
            return await _context.Users
                .Where(u => u.Email == email)
                .Select(user => new User
                {
                    Password = user.Password,
                    UserType = user.UserType,
                    DisplayName = user.DisplayName,
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
        public async Task Register(RegisterModel user)
        {
            try
            {
                _context.Users.Add(new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address,
                    DisplayName = user.DisplayName,
                    ProfilePicture = await ToBase64Image(user.ProfilePicture),
                    Email = user.Email,
                    Password = user.Password,
                    PhoneNumber = user.PhoneNumber,
                    UserType = "user",
                });
                await _context.SaveChangesAsync();
            } 
            catch(Exception)
            {
                throw;
            }
        }
        public async Task UpdateProfile(UserDto userRequest)
        {
            try
            {
                _context.Users
                    .Where(u => u.Email == userRequest.Email)
                    .ExecuteUpdate(u => u.SetProperty(u => u.FirstName, userRequest.FirstName)
                                          .SetProperty(u => u.LastName, userRequest.LastName)
                                          .SetProperty(u => u.DisplayName, userRequest.DisplayName)
                                          .SetProperty(u => u.PhoneNumber, userRequest.PhoneNumber)
                                          .SetProperty(u => u.Address, userRequest.Address));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateProfilePicture(IFormFile profilePicture, string email)
        {
            try
            {
                var base64 = await ToBase64Image(profilePicture);
                await _context.Users
                    .Where(u => u.Email == email)
                    .ExecuteUpdateAsync(u => u.SetProperty(u => u.ProfilePicture, base64));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task UpdateVerifyFlag(string email, string flag)
        {
            try
            {
                await _context.Users
                    .Where(u => u.Email == email)
                    .ExecuteUpdateAsync(u => u.SetProperty(u => u.IsVerified, flag));
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<string> ToBase64Image(IFormFile profilePicture)
        {
            if (profilePicture is null)
            {
                return string.Empty;
            }
            using var memoryStream = new MemoryStream();
            await profilePicture.CopyToAsync(memoryStream);
            return $"data:image / jpeg; base64,{Convert.ToBase64String(memoryStream.ToArray())}";
        }
    }
}
