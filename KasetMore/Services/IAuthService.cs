using FluentValidation.Results;
using KasetMore.ApplicationCore.Models;

namespace KasetMore.Services
{
    public interface IAuthService
    {
        Task<(string?, string?)> Authenticate(LoginModel loginModel);
    }
}