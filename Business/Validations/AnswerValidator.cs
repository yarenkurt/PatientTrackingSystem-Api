using Entities.Concrete;
using FluentValidation;

namespace Business.Validations
{
    public class AnswerValidator : AbstractValidator<AnswerPool>
    {
        public AnswerValidator()
        {
            RuleFor(x => x.Description).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(x => x.Score).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.QuestionPoolId).NotEmpty().NotNull();
        }
    }
}