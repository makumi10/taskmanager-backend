using System.ComponentModel.DataAnnotations;

namespace TaskManager.Validators
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success; 

            if (value is DateTime dateTime)
            {
                if (dateTime < DateTime.UtcNow)
                    return new ValidationResult("Due date cannot be in the past.");
            }

            return ValidationResult.Success;
        }
    }
}