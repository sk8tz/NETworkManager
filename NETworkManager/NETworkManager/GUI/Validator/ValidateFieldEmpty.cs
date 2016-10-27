using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace NETworkManager.GUI.Validator
{
    public class ValidateFieldEmpty : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string text = value as string;

            if (string.IsNullOrEmpty(text))
                return new ValidationResult(false, Application.Current.Resources["LocalizedString_ValidateError_FieldEmpty"] as string);

            return ValidationResult.ValidResult;
        }
    }
}
