using Entities.Concrete;
using FluentValidation;

namespace Business.Validations
{
    public class DegreeValidator : AbstractValidator<Degree>
    {
        public DegreeValidator()
        {
            RuleFor(x => x.Description).NotEmpty().NotNull().MaximumLength(100);
        }
    }
}