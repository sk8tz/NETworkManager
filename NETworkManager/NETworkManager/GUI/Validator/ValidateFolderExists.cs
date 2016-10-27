using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace NETworkManager.GUI.Validator
{
    public class ValidateFolderExists : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string path = value as string;

            if (Directory.Exists(path))
                return ValidationResult.ValidResult;

            return new ValidationResult(false, Application.Current.Resources["LocalizedString_ValidateError_FolderDoesNotExist"] as string);
        }
    }
}
