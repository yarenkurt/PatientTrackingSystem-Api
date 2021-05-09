using Entities.Concrete;
using FluentValidation;

namespace Business.Validations
{
    public class AdviceValidator : AbstractValidator<DoctorAdvice>
    {
        public AdviceValidator()
        {
            RuleFor(x => x.Description).NotEmpty().NotNull().MaximumLength(500);
        }
    }
}