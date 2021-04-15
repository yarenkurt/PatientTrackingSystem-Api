using Entities.Concrete;
using FluentValidation;

namespace Business.Validations
{
    public class HospitalValidator : AbstractValidator<Hospital>
    {
        public HospitalValidator()
        {
            RuleFor(x => x.Description).NotEmpty().NotNull().MaximumLength(255);
            RuleFor(x => x.Address).NotEmpty().NotNull().MaximumLength(300);
            RuleFor(x => x.DistrictId).NotEmpty().NotNull();
            RuleFor(x => x.Phone).NotEmpty().NotNull();
        }
    }
}