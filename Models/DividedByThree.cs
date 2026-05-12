using System.ComponentModel.DataAnnotations;

namespace MVC02.Models
{
    public class DividedByThree:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value == null)
                return null;

            int valueInput = (int)value;
            if (valueInput % 3 == 0)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("You Should enter a num diviable by 3!");
        }
    }
}
