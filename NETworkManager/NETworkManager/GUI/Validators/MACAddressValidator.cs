using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace NETworkManager.GUI.Validators
{
    public class MACAddressValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {            
            if (Regex.IsMatch(value as string, "^[A-Fa-f0-9]{12}$|^[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}$"))
                return ValidationResult.ValidResult;

            return new ValidationResult(false, Application.Current.Resources["LocalizedString_ValidateError_EnterValidMACAddress"] as string);
        }
    }
}
