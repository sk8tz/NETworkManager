﻿using MahApps.Metro;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NETworkManager.GUI.Translator
{
    public sealed class AppThemeNameTranslator : IValueConverter
    {
        /* Translate the name of the app theme */
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            AppTheme theme = value as AppTheme;

            string name = Application.Current.Resources["LocalizedString_AppTheme_" + theme.Name] as string;

            if (string.IsNullOrEmpty(name))
                name = theme.Name;

            return name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
