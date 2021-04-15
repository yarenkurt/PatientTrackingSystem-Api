using Entities.Concrete;
using FluentValidation;

namespace Business.Validations
{
    public class PatientAnswerValidator : AbstractValidator<PatientAnswer>
    {
        public PatientAnswerValidator()
        {
            RuleFor(x => x.Score).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.PatientId).NotEmpty().NotNull();
            RuleFor(x => x.QuestionPoolId).NotEmpty().NotNull();
        }
    }
}