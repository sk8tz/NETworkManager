using System;
using System.Globalization;
using System.Windows.Data;
using NETworkManager.Core.Settings;

namespace NETworkManager.GUI.Converter
{
    public sealed class SettingsPathIsDefaultLocationToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value as string == SettingsController.DefaultSettingsLocation)
                return true;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
