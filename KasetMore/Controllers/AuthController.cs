using KasetMore.ApplicationCore.Models;
using KasetMore.Data.Models;
using KasetMore.Data.Repositories.Interfaces;
using KasetMore.Services;
using Microsoft.AspNetCore.Mvc;

namespace KasetMore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public AuthController(IConfiguration configuration, IAuthService authService, IUserRepository userRepository)
        {
            _configuration = configuration;
            _authService = authService;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            (string jwt, string profilePicture) = await _authService.Authenticate(loginModel);
            return jwt is not null
                ? Ok(new { jwt, profilePicture})
                : BadRequest();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel user)
        {
            try
            {
                await _userRepository.Register(user);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("update-profile-picture")]
        public async Task<IActionResult> UpdateProfilePicture(IFormFile file, string email)
        {
            try
            {
                if (email is null)
                {
                    return BadRequest();
                }
                await _userRepository.UpdateProfilePicture(file, email);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("update-verify-flag")]
        public async Task<IActionResult> UpdateVerifyFlag(string email, string flag)
        {
            try
            {
                if (email is null || flag is null)
                {
                    return BadRequest();
                }
                await _userRepository.UpdateVerifyFlag(email, flag);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("update-profile")]
        public async Task<IActionResult> UpdateProfile(UserDto userDto)
        {
            try
            {
                await _userRepository.UpdateProfile(userDto);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("user-by-email")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                return Ok(await _userRepository.GetUserByEmail(email));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPost("user-by-usertype")]
        public async Task<IActionResult> GetUserByUserType(string userType, string? verifiedStatus)
        {
            try
            {
                return Ok(await _userRepository.GetUserByUserType(userType, verifiedStatus));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
