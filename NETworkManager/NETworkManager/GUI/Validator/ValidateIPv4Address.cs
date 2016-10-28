using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace NETworkManager.GUI.Validator
{
    public class ValidateIPv4Address : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (Regex.IsMatch(value as string, @"\b((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.|$)){4}\b"))
                return ValidationResult.ValidResult;

            return new ValidationResult(false, Application.Current.Resources["LocalizedString_ValidateError_EnterValidIPv4Address"] as string);
        }
    }
}
