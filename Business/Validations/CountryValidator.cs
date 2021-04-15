using Entities.Concrete;
using FluentValidation;

namespace Business.Validations
{
    public class CountryValidator : AbstractValidator<Country>
    {
        public CountryValidator()
        {
            RuleFor(x => x.Description)
                .NotNull()
                .Length(1, 100);
            RuleFor(x => x.CountryCode)
                .NotNull()
                .Length(1, 5);

        }
    }
}