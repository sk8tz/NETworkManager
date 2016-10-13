using System.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using NETworkManager.GUI;

namespace NETworkManager.Core.AppLauncher
{
    public static class AppLauncher
    {
        // List of all applications
        public static List<AppInfo> AppList
        {
            get
            {
                return new List<AppInfo>
                {
                    new AppInfo(Application.Current.Resources["LocalizedString_AppLauncherTranslatedName_IPScanner"] as string, 1, Application.Current.Resources["appbar_acorn"] as Canvas),
                    new AppInfo(Application.Current.Resources["LocalizedString_AppLauncherTranslatedName_SubnetCalculator"] as string, 2, Application.Current.Resources["appbar_acorn"] as Canvas),
                    new AppInfo(Application.Current.Resources["LocalizedString_AppLauncherTranslatedName_PortScanner"] as string, 3, Application.Current.Resources["appbar_acorn"] as Canvas)
                };
            }
        }

        public static void StartApp(AppInfo appInfo)
        {
            switch (appInfo.ID)
            {
                case 1:
                    IPScanner window = new IPScanner();
                    window.Show();
                    break;
                default:
                    break;
            }
        }
    }

}
