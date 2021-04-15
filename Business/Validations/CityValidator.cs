using Entities.Concrete;
using FluentValidation;

namespace Business.Validations
{
    public class CityValidator : AbstractValidator<City>
    {
        public CityValidator()
        {
            RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(500);
        }
    }
}