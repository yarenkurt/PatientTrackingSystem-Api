using Entities.Concrete;
using FluentValidation;

namespace Business.Validations
{
    public class LoginHistoryValidator : AbstractValidator<PersonLoginHistory>
    {
        public LoginHistoryValidator()
        {
            RuleFor(x => x.Date).NotEmpty().NotEmpty();
            RuleFor(x => x.Message).NotEmpty().NotEmpty().MaximumLength(250);
            RuleFor(x => x.IpAddress).NotEmpty().NotEmpty().MaximumLength(50);
            RuleFor(x => x.PersonId).NotEmpty().NotEmpty();
            RuleFor(x => x.IsSuccess).NotEmpty().NotNull();
        }
    }
}