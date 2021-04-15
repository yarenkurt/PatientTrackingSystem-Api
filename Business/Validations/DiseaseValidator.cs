using Entities.Concrete;
using FluentValidation;

namespace Business.Validations
{
    public class DiseaseValidator : AbstractValidator<Disease>
    {
        public DiseaseValidator()
        {
            RuleFor(x => x.DepartmentId).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotEmpty().NotNull().MaximumLength(100);
        }
    }
}