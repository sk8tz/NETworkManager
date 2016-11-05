using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace NETworkManager.GUI.Validator
{
    public class EmptyFieldValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(value as string))
                return new ValidationResult(false, Application.Current.Resources["LocalizedString_ValidateError_FieldEmpty"] as string);

            return ValidationResult.ValidResult;
        }
    }
}
