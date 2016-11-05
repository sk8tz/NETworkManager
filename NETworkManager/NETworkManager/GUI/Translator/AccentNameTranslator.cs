using MahApps.Metro;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NETworkManager.GUI.Translator
{
    public sealed class AccentNameTranslator : IValueConverter
    {
        /* Translate the name of the accent */
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Accent accent = value as Accent;

            string name = Application.Current.Resources["LocalizedString_Accent_" + accent.Name] as string;

            if (string.IsNullOrEmpty(name))
                name = accent.Name;

            return name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
