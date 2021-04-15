using Entities.Concrete;
using FluentValidation;

namespace Business.Validations
{
    public class DepartmentValidator : AbstractValidator<Department>
    {
        public DepartmentValidator()
        {
            RuleFor(x => x.Description).NotEmpty().NotNull().MaximumLength(250);
            RuleFor(x => x.HospitalId).NotEmpty().NotNull();
        }   
    }
}