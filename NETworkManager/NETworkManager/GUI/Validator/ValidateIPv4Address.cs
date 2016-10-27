using System.Globalization;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace NETworkManager.GUI.Validator
{
    public class ValidateIPv4Address : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            IPAddress ipAddr;

            if (IPAddress.TryParse(value as string, out ipAddr))
            {
                switch (ipAddr.AddressFamily)
                {
                    case System.Net.Sockets.AddressFamily.InterNetwork:
                        return ValidationResult.ValidResult;
                    case System.Net.Sockets.AddressFamily.InterNetworkV6:
                        return new ValidationResult(false, Application.Current.Resources["LocalizedString_ValidateError_IPv6NotSupported"] as string);
                }
            }

            return new ValidationResult(false, Application.Current.Resources["LocalizedString_ValidateError_EnterValidIPv4Address"] as string);
        }
    }
}
