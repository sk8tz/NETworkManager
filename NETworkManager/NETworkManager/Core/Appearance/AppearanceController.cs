using MahApps.Metro;
using System;
using System.Windows;

namespace NETworkManager.Core.Appearance
{
    public static class AppearanceController
    {
        /// <summary>
        /// Load Appearance (AppTheme and Accent) from the user settings.
        /// </summary>
        public static void LoadAppearance()
        {
            // Change the AppTheme if it is not empty and different from the currently loaded
            string appThemeName = Properties.Settings.Default.Appearance_AppTheme;

            if (!string.IsNullOrEmpty(appThemeName) && appThemeName != ThemeManager.DetectAppStyle().Item2.Name)
                ChangeAppTheme(appThemeName);

            // Change the Accent if it is not empty and different from the currently loaded
            string accentName = Properties.Settings.Default.Appearance_Accent;

            if (!string.IsNullOrEmpty(accentName) && accentName != ThemeManager.DetectAppStyle().Item2.Name)
                ChangeAccent(accentName);
        }

        /// <summary>
        /// Changes the AppTheme
        /// </summary>
        /// <param name="appThemeName">Name of the AppTheme</param>
        public static void ChangeAppTheme(string appThemeName)
        {
            ThemeManager.ChangeAppTheme(Application.Current, appThemeName);
        }

        public static void ChangeAccent(string accentName)
        {
            Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);
            Accent accent = ThemeManager.GetAccent(accentName);

            ThemeManager.ChangeAppStyle(Application.Current, accent, appStyle.Item1);
        }
    }
}
