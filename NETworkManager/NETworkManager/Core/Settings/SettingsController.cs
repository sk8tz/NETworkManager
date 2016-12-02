using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using System.Reflection;
using NETworkManager.Core.Network;

namespace NETworkManager.Core.Settings
{
    public static class SettingsController
    {
        private const string SettingsFolderName = "Settings";
        private const string IsPortableFileName = "IsPortable.settings";

        private static string ApplicationName
        {
            get { return Assembly.GetEntryAssembly().GetName().Name; }
        }

        // Default settings location ("%AppData%\PRODUCTNAME\Settings")
        private static string DefaultSettingsLocation
        {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName, SettingsFolderName); }
        }

        // Custom location (wherever the use want to store the files)
        private static string CustomSettingsLocation
        {
            get { return Properties.Settings.Default.Settings_Location; }
        }

        public static string SettingsLocation
        {
            get
            {
                string settingsLocation = CustomSettingsLocation;

                if (!string.IsNullOrEmpty(settingsLocation) && Directory.Exists(settingsLocation))
                    return settingsLocation;

                return DefaultSettingsLocation;
            }
        }

        // Portable settings location (PROGRAMFOLDER\Settings)
        public static string PortableSettingsLocation
        {
            get { return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), SettingsFolderName); }
        }

        // Path to the file which indicates that the application is portable
        private static string IsPortableFilePath
        {
            get { return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), IsPortableFileName); }
        }

        public static bool IsPortable
        {
            get { return File.Exists(IsPortableFilePath); }
        }

        public static void MoveSettings(string sourceLocation, string targedLocation, bool overwriteExistingFiles)
        {
            if (!Directory.Exists(targedLocation))
                Directory.CreateDirectory(targedLocation);

            if (Directory.Exists(sourceLocation))
            {
                string[] files = Directory.GetFiles(sourceLocation);

                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);
                    string targedFile = Path.Combine(targedLocation, fileName);

                    // Delete file if it already exists
                    if (overwriteExistingFiles)
                        File.Delete(targedFile);

                    File.Move(file, targedFile);
                }
            }
        }

        public static void ChangeSettingsLocation(string targedLocation, bool overrideExistingFiles)
        {
            MoveSettings(SettingsLocation, targedLocation, overrideExistingFiles);
        }

        public static void MakeSettingsPortable(bool isPortable, bool overrideExistingFiles)
        {
            string sourceLocation = string.Empty;
            string targedLocation = string.Empty;

            if (isPortable)
            {
                sourceLocation = SettingsLocation;
                targedLocation = PortableSettingsLocation;

                // Create the file that indicates that the application is portable
                File.Create(IsPortableFilePath);
            }
            else
            {
                sourceLocation = PortableSettingsLocation;
                targedLocation = SettingsLocation;

                // Delete the file that indicates that the application is portable
                File.Delete(IsPortableFilePath);
            }

            // Move all existing settings to the targed location
            MoveSettings(sourceLocation, targedLocation, overrideExistingFiles);
        }

        #region WakeOnLan
        private static List<WakeOnLanInfo> DeserializeWakeOnLanTempaltes(string filePath)
        {
            List<WakeOnLanInfo> list = new List<WakeOnLanInfo>();

            XmlSerializer serializer = new XmlSerializer(typeof(List<WakeOnLanInfo>));

            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                List<WakeOnLanInfo> _list = (List<WakeOnLanInfo>)(serializer.Deserialize(stream));
                list.AddRange(_list);
            }

            return list;
        }

        public static List<WakeOnLanInfo> GetWakeOnLanTemplates()
        {
            string filePath = Path.Combine(SettingsLocation, Properties.Settings.Default.FileName_WakeOnLanTemplates);

            if (File.Exists(filePath))
                return DeserializeWakeOnLanTempaltes(filePath);

            return new List<WakeOnLanInfo>();
        }

        private static void SerializeWakeOnLanTemplates(List<WakeOnLanInfo> list, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<WakeOnLanInfo>));

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, list);
            }
        }

        public static void SaveWakeOnLanTemplates(List<WakeOnLanInfo> list)
        {
            string filePath = Path.Combine(SettingsLocation, Properties.Settings.Default.FileName_WakeOnLanTemplates);

            SerializeWakeOnLanTemplates(list, filePath);
        }
        #endregion
    }
}
