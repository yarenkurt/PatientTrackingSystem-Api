using Entities.Concrete;
using FluentValidation;

namespace Business.Validations
{
    public class QuestionValidator : AbstractValidator<QuestionPool>
    {
        public QuestionValidator()
        {
            RuleFor(x => x.LowerLimit).NotEmpty().NotNull();
            RuleFor(x => x.UpperLimit).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotEmpty().NotNull().MaximumLength(300);
            RuleFor(x => x.QuestionType).NotEmpty().NotNull();
        }
    }
}