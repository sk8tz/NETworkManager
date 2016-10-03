using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NETworkManager.Core.Appearance
{
    public static class AppearanceController
    {
        public static void LoadAppearance()
        {
            string appThemeName = Properties.Settings.Default.Appearance_AppTheme;

            if (appThemeName != Properties.Resources.Appearance_DefaultAppTheme)
                ChangeAppTheme(appThemeName);

            string accentName = Properties.Settings.Default.Appearance_Accent;

            if (accentName != Properties.Resources.Appearance_DefaultAccent)
                ChangeAccent(accentName);
        }

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
