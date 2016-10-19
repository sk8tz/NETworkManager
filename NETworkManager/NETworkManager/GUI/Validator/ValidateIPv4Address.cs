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
    public class ValidateIPv4Address : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
         
            return ValidationResult.ValidResult;
        }
    }
}
