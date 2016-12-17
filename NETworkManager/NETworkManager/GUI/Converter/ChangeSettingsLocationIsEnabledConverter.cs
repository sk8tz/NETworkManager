using System;
using System.Globalization;
using System.Windows.Data;
using NETworkManager.Core.Settings;

namespace NETworkManager.GUI.Converter
{
    public sealed class ChangeSettingsLocationIsEnabledConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool isChecked = true;
            bool hasError = true;

            bool.TryParse(values[1] as string, out isChecked);
            bool.TryParse(values[2] as string, out hasError);

            if (values[0] as string != SettingsController.SettingsLocation && !isChecked && !hasError)
                return true;

            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
