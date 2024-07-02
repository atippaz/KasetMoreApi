using FluentValidation;
using KasetMore.ApplicationCore.Models;
using KasetMore.Data.Repositories.Interfaces;

namespace KasetMore.Validators
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        private readonly IUserRepository _userRepository;

        public LoginValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.Email)
               .NotEmpty()
               .WithMessage("Email is required")
               .MustAsync((email, _) => IsValidEmail(email))
                    .WithMessage("Email or password is not valid");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .MustAsync((user, password, _) => IsValidPassword(user))
                    .WithMessage("Email or password is not valid");
        }

        private async Task<bool> IsValidEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            return user is not null;
        }
        private async Task<bool> IsValidPassword(LoginModel model)
        {
            var user = await _userRepository.AuthenticateUser(model.Email);
            if(user is null)
            {
                return false;
            }
            return user.Password == model.Password;
        }
    }
}
