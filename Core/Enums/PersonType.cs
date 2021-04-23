using System.ComponentModel.DataAnnotations;

namespace Core.Enums
{
    public enum PersonType
    {
        [Display(Name = "Null")]Null = 0,
        [Display(Name = "Patient")]Patient = 1,
        [Display(Name = "Doctor")]Doctor = 2,
        [Display(Name = "Admin")]Admin = 3
    }
}