using FluentValidation;

namespace Belle.Services.Authentication.Models
{
    public class LoginViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .NotNull()
                .Length(3, 63);

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .Length(3, 63);
        }
    }
}