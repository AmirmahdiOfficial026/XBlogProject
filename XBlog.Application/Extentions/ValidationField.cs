using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace XBlog.Application.Extentions;

public class ValidationField : ValidationAttribute
{
    private readonly bool _allowNumber;
    private readonly bool _allowPersion;
    private readonly bool _allowEnglish;

    public ValidationField(bool allowNumber = false, bool allowPersion = false, bool allowEnglish = false)
    {
        _allowNumber = allowNumber;
        _allowPersion = allowPersion;
        _allowEnglish = allowEnglish;
        ErrorMessage = "فقط حروف انگلیسی مجاز است.";
    }
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
            return ValidationResult.Success;
        string stringValue = value.ToString();
        string pattern = string.Empty;
        if (_allowNumber)
        {
            pattern = @"^[0-9 ]+$";
            //ErrorMessage = "This field only accepts numbers";
        }
        else if (_allowEnglish)
        {
            pattern = @"^[a-zA-Z ]+$";
            //ErrorMessage = "This field only accepts English letters";
        }
        else if (_allowPersion)
        {
            pattern = @"^[ا-ی ]+$";
            //ErrorMessage = "This field only accepts Persion letters";
        }
        else if (_allowNumber && _allowPersion)
        {
            pattern = @"^[ا-ی 0-9]+$";
            //ErrorMessage = "This field only accepts Persion letters and numbers";
        }
        else if (_allowNumber && _allowEnglish)
        {
            pattern = @"^[a-zA-Z0-9 ]+$";
            //ErrorMessage = "This field only accepts English letters and numbers";
        }
        else if (_allowEnglish && _allowPersion)
        {
            pattern = @"^[a-zA-Zا-ی ]+$";
            //ErrorMessage = "This field only accepts Persion letters and English letters";
        }
        else
        {
            pattern = @"^[a-zA-Z0-9ا-ی ]+$";
            //ErrorMessage = "Please enter the field correctly";
        }
        if (Regex.IsMatch(stringValue, pattern))
            return ValidationResult.Success;
        else
            return new ValidationResult(ErrorMessage);
    }
}
