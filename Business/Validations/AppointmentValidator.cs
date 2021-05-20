using Entities.Concrete;
using FluentValidation;

namespace Business.Validations
{
    public class AppointmentValidator : AbstractValidator<Appointment>
    {
        public AppointmentValidator()
        {
            RuleFor(x => x.Date).NotNull().NotEmpty();
            RuleFor(x => x.DoctorId).NotNull().NotEmpty();
            RuleFor(x => x.PatientId).NotNull().NotEmpty();
        }
    }
}