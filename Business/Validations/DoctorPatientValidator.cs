using Entities.Concrete;
using FluentValidation;

namespace Business.Validations
{
    public class DoctorPatientValidator : AbstractValidator<DoctorPatient>
    {
        public DoctorPatientValidator()
        {
            RuleFor(x => x.PatientId).NotEmpty().NotNull();
            RuleFor(x => x.DoctorId).NotEmpty().NotNull();
        }
    }
}