using System.Windows.Controls;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using NETworkManager.Core.Settings;
using System.Text.RegularExpressions;

namespace NETworkManager.GUI.Validator
{
    public class ValidateWakeOnLanTemplate : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            WakeOnLanTemplate template = (value as BindingGroup).Items[0] as WakeOnLanTemplate;

            if (string.IsNullOrEmpty(template.MAC))
                return new ValidationResult(false, Application.Current.Resources["LocalizedString_Validate_MACAddressEmpty"] as string);

            if (!Regex.IsMatch(template.MAC, "^[A-Fa-f0-9]{12}$|^[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}$"))
                return new ValidationResult(false, Application.Current.Resources["LocalizedString_Validate_EnterValidMACAddress"] as string);

            if (string.IsNullOrEmpty(template.Hostname))
                return new ValidationResult(false, Application.Current.Resources["LocalizedString_Validate_HostnameEmpty"] as string);

            return ValidationResult.ValidResult;
        }
    }
}
