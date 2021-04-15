using Entities.Concrete;
using FluentValidation;

namespace Business.Validations
{
    public class PatientDiseaseValidator : AbstractValidator<PatientDisease>
    {
        public PatientDiseaseValidator()
        {
            RuleFor(x => x.DiseaseId).NotEmpty().NotNull();
            RuleFor(x => x.PatientId).NotEmpty().NotNull();
        }
    }
}