using Microsoft.Win32;
using System.Reflection;

namespace NETworkManager.Core.Settings
{
    public static class AutostartManager
    {
        private static string ApplicationName = Assembly.GetEntryAssembly().GetName().Name;
        private const string RunKeyCurrentUser = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

        public static bool IsAutostartEnabled
        {
            get
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(RunKeyCurrentUser);

                if (registryKey.GetValue(ApplicationName) != null)
                    return true;

                return false;
            }
        }

        public static void EnableAutostart()
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(RunKeyCurrentUser, true);

            string command = string.Format("{0} --{1}", Assembly.GetExecutingAssembly().Location, Properties.Resources.StartParameter_Autostart);

            registryKey.SetValue(ApplicationName, command);
            registryKey.Close();
        }

        public static void DisableAutostart()
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(RunKeyCurrentUser, true);

            registryKey.DeleteValue(ApplicationName);
            registryKey.Close();
        }
    }
}
