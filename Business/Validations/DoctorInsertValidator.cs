using Entities.Dtos;
using FluentValidation;

namespace Business.Validations
{
    public class DoctorInsertValidator : AbstractValidator<InsertDoctorDto>
    {
        public DoctorInsertValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().MaximumLength(150);
            RuleFor(x => x.FirstName).NotNull().NotEmpty().MaximumLength(150);
            RuleFor(x => x.LastName).NotNull().NotEmpty().MaximumLength(150);
            RuleFor(x => x.Gsm).NotNull().NotEmpty();
            RuleFor(x => x.DegreeId).NotNull().NotEmpty();
            RuleFor(x => x.DepartmentId).NotNull().NotEmpty();
        }
    }
}