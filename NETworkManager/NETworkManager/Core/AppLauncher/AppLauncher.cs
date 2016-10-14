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
                    new AppInfo(Application.Current.Resources["LocalizedString_AppName_IPScanner"] as string, 1, Application.Current.Resources["appbar_diagram"] as Canvas),
                    new AppInfo(Application.Current.Resources["LocalizedString_AppName_SubnetCalculator"] as string, 2, Application.Current.Resources["appbar_calculator"] as Canvas),
                    new AppInfo(Application.Current.Resources["LocalizedString_AppName_PortScanner"] as string, 3, Application.Current.Resources["appbar_acorn"] as Canvas),
                    new AppInfo(Application.Current.Resources["LocalizedString_AppName_WakeOnLan"] as string, 4, Application.Current.Resources["appbar_control_play"] as Canvas)
                };
            }
        }

        public static void StartApp(AppInfo appInfo)
        {
            switch (appInfo.ID)
            {
                case 1:
                    IPScanner ipScanner = new IPScanner();
                    ipScanner.Show();
                    break;
                case 2:
                    SubnetCalculator subnetCalculator = new SubnetCalculator();
                    subnetCalculator.Show();
                    break;
                case 4:
                    WakeOnLAN wakeOnLAN = new WakeOnLAN();
                    wakeOnLAN.Show();
                    break;
                default:
                    break;
            }
        }
    }

}
