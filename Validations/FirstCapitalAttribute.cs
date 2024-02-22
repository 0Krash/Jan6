using System.ComponentModel.DataAnnotations;

namespace Jan6.Validations;

public class FirstCapitalAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var fistLetter = value.ToString()[0].ToString();
        if (fistLetter != fistLetter.ToUpper()) return new ValidationResult("First Letter must be capital");

        return ValidationResult.Success;
    }
}