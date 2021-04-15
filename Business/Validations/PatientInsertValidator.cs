using System.Data;
using Entities.Dtos;
using FluentValidation;

namespace Business.Validations
{
    public class PatientInsertValidator : AbstractValidator<InsertPatientDto>
    {
        public PatientInsertValidator()
        {
            RuleFor(x => x.IdentityNumber).NotNull().NotEmpty().Length(11);
            RuleFor(x => x.Gsm).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotNull().NotEmpty();
            RuleFor(x => x.FirstName).NotNull().NotEmpty().MaximumLength(150);
            RuleFor(x => x.LastName).NotNull().NotEmpty().MaximumLength(150);
        }
    }
}