using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace NETworkManager.GUI
{
    public static class ApplicationController
    {
        // List of all applications
        public static List<ApplicationInfo> ApplicationList
        {
            get
            {
                return new List<ApplicationInfo>
                {
                    new ApplicationInfo(Application.Current.Resources["LocalizedString_ApplicationName_IPScanner"] as string, 1, Application.Current.Resources["appbar_diagram"] as Canvas),
                    new ApplicationInfo(Application.Current.Resources["LocalizedString_ApplicationName_NetworkInterface"] as string, 2, Application.Current.Resources["appbar_network"] as Canvas),
                    new ApplicationInfo(Application.Current.Resources["LocalizedString_ApplicationName_PortScanner"] as string, 3, Application.Current.Resources["appbar_add"] as Canvas),
                    new ApplicationInfo(Application.Current.Resources["LocalizedString_ApplicationName_SubnetCalculator"] as string, 4, Application.Current.Resources["appbar_calculator"] as Canvas),
                    new ApplicationInfo(Application.Current.Resources["LocalizedString_ApplicationName_WakeOnLan"] as string, 5, Application.Current.Resources["appbar_control_play"] as Canvas)
                };
            }
        }

        public static void OpenApplication(ApplicationInfo appInfo)
        {
            switch (appInfo.ID)
            {
                case 1:
                    IPScanner ipScanner = new IPScanner();
                    ipScanner.Show();
                    break;
                case 2:
                    NetworkInterface networkInterface = new NetworkInterface();
                    networkInterface.Show();
                    break;
                case 3:
                    PortScanner portScanner = new PortScanner();
                    portScanner.Show();
                    break;
                case 4:
                    SubnetCalculator subnetCalculator = new SubnetCalculator();
                    subnetCalculator.Show();
                    break;
                case 5:
                    WakeOnLAN wakeOnLAN = new WakeOnLAN();
                    wakeOnLAN.Show();
                    break;
                default:
                    break;
            }
        }
    }

}
