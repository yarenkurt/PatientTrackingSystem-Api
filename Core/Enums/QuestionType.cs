using System.ComponentModel.DataAnnotations;

namespace Core.Enums
{
    public enum QuestionType
    {
        [Display(Name = "Multiple")]MultipleChoice = 1,
        [Display(Name = "Numeric")]NumericInput = 2
    }
}