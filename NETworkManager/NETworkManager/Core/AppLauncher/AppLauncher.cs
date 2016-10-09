using System.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace NETworkManager.Core.AppLauncher
{
    public static class AppLauncher
    {
        // List
        public static List<AppInfo> AppList
        {
            get
            {
                return new List<AppInfo>
                {
                    new AppInfo("IP-Scanner", 1, Application.Current.Resources["appbar_acorn"] as Canvas),
                    new AppInfo("Subnet-Calculator", 1, Application.Current.Resources["appbar_acorn"] as Canvas),
                    new AppInfo("Port-Scanner", 1, Application.Current.Resources["appbar_acorn"] as Canvas)
                };
            }
        }
    }
}