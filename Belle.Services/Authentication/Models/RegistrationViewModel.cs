using FluentValidation;

namespace Belle.Services.Authentication.Models
{
    public class RegistrationViewModel
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegistrationViewModelValidator : AbstractValidator<RegistrationViewModel>
    {
        public RegistrationViewModelValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .NotNull()
                .Length(3, 63);

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .Length(3, 63);

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .Length(3, 63);
        }
    }
}