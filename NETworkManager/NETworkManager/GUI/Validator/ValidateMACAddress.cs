using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NETworkManager.GUI.Validator
{
    public class ValidateMACAddress : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string mac = value as string;

            if (string.IsNullOrEmpty(mac))
                return new ValidationResult(false, Application.Current.Resources["LocalizedString_Validate_EmptyMACAddress"] as string);

            if (! Regex.IsMatch(mac, "^[A-Fa-f0-9]{12}$|^[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}(:|-){1}[A-Fa-f0-9]{2}$"))
                return new ValidationResult(false, Application.Current.Resources["LocalizedString_Validate_WrongMACAddressFormat"] as string);

            return ValidationResult.ValidResult;
        }
    }
}
