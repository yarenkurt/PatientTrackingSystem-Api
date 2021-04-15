using Entities.Dtos;
using FluentValidation;

namespace Business.Validations
{
    public class AdminInsertValidator : AbstractValidator<InsertAdminDto>
    {
        public AdminInsertValidator()
        {
            RuleFor(x => x.Gsm).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotNull().NotEmpty();
            RuleFor(x => x.FirstName).NotNull().NotEmpty().MaximumLength(150);
            RuleFor(x => x.LastName).NotNull().NotEmpty().MaximumLength(150);
        }
    }
}