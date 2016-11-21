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

        // Portable settings location (PROGRAMFOLDER\Settings)
        private static string PortableSettingsLocation
        {
            get { return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), SettingsFolderName); }
        }

        // Custom location (wherever the use want to store the files)
        private static string CustomSettingsLocation
        {
            get { return Properties.Settings.Default.Settings_Location; }
        }

        // Path to the file which indicates that the application is portable
        private static string IsPortableFilePath
        {
            get { return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), IsPortableFileName); }
        }

        public static string SettingsLocation
        {
            get
            {
                if (IsPortable)
                    return PortableSettingsLocation;

                if (!string.IsNullOrEmpty(CustomSettingsLocation))
                    return CustomSettingsLocation;

                return DefaultSettingsLocation;
            }
        }

        public static void MoveSettings(string sourceLocation, string targedLocation)
        {
            if (!Directory.Exists(targedLocation))
                Directory.CreateDirectory(targedLocation);

            if (Directory.Exists(sourceLocation))
            {
                string[] files = Directory.GetFiles(sourceLocation);

                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);

                    File.Move(file, Path.Combine(targedLocation, fileName));
                }
            }
        }

        public static bool IsPortable
        {
            get { return File.Exists(IsPortableFilePath); }
        }

        public static void MakeSettingsPortable()
        {
            string oldLocation = GetSettingsLocation();

            if (!File.Exists(IsPortableFilePath))
                File.Create(IsPortableFilePath);

            MoveSettings(oldLocation, PortableSettingsLocation);
        }

        public static void RestoreSettingsDefault()
        {
            File.Delete(IsPortableFilePath);

            MoveSettings(PortableSettingsLocation, DefaultSettingsLocation);
        }

        #region WakeOnLan
        private static List<WakeOnLanInfo> DeserializeWakeOnLanTempaltes(string filePath)
        {
            List<WakeOnLanInfo> list = new List<WakeOnLanInfo>();

            XmlSerializer serializer = new XmlSerializer(typeof(List<WakeOnLanInfo>));

            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                var test = (List<WakeOnLanInfo>)(serializer.Deserialize(stream));
                list.AddRange(test);
            }

            return list;
        }

        public static List<WakeOnLanInfo> GetWakeOnLanTemplates()
        {
            string filePath = Path.Combine(GetSettingsLocation(), Properties.Settings.Default.FileName_WakeOnLanTemplates);

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
            string filePath = Path.Combine(GetSettingsLocation(), Properties.Settings.Default.FileName_WakeOnLanTemplates);

            SerializeWakeOnLanTemplates(list, filePath);
        }
        #endregion
    }
}
