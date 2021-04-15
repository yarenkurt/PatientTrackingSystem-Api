using Entities.Concrete;
using FluentValidation;

namespace Business.Validations
{
    public class DistrictValidator : AbstractValidator<District>
    {
        public DistrictValidator()
        {
            RuleFor(x => x.Description).NotEmpty().NotNull().MaximumLength(150);
            RuleFor(x => x.CityId).NotEmpty().NotNull();
        }
    }
}