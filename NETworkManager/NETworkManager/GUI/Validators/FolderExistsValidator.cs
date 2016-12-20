﻿using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace NETworkManager.GUI.Validators
{
    public class FolderExistsValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (Directory.Exists(value as string))
                return ValidationResult.ValidResult;

            return new ValidationResult(false, Application.Current.Resources["LocalizedString_ValidateError_FolderDoesNotExist"] as string);
        }
    }
}
