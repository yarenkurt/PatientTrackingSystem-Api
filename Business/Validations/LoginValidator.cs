using Core.Models;
using FluentValidation;

namespace Business.Validations
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}