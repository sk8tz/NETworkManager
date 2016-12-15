using Microsoft.Win32;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace NETworkManager.Core.Autostart
{
    public static class AutostartManager
    {
        private static string AppName = Assembly.GetEntryAssembly().GetName().Name;
        private const string RunKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

        public static bool IsEnabled
        {
            get
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(RunKey);

                if (registryKey.GetValue(AppName) != null)
                    return true;

                return false;
            }
        }

        public static void Enable()
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(RunKey, true);

            string command = string.Format("{0} /{1}", Assembly.GetExecutingAssembly().Location, Properties.Resources.StartParameter_Autostart);

            registryKey.SetValue(AppName, command);
            registryKey.Close();
        }

        public static void Disable()
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(RunKey, true);

            registryKey.DeleteValue(AppName);
            registryKey.Close();
        }
    }
}
