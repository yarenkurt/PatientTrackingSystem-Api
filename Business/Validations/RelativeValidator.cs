using System.Data;
using Entities.Concrete;
using FluentValidation;

namespace Business.Validations
{
    public class RelativeValidator : AbstractValidator<PatientRelative>
    {
        public RelativeValidator()
        {
            RuleFor(x => x.Gsm).NotNull().NotEmpty();
            RuleFor(x => x.PatientId).NotNull().NotEmpty();
            RuleFor(x => x.FirstName).NotNull().NotEmpty().MaximumLength(150);
            RuleFor(x => x.LastName).NotNull().NotEmpty().MaximumLength(150);
        }
    }
}