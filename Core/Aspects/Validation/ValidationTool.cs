using System.Linq;
using FluentValidation;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.Aspects.Validation
{
    public static class ValidationTool
    {
        public static void Validate(IValidator validator, object obj)
        {
            var result = validator.Validate(obj);
            if (result.IsValid) return;
            var errors = result.Errors.Aggregate("", (current, error) => $"{current}{error.ErrorMessage}\n");
            throw new ValidationException(errors);
        }
    }
}