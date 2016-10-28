using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace NETworkManager.Core.Settings
{
    public static class SettingsController
    {
        public static void LoadSettings()
        {

        }

        public static void VerifySettings()
        {
            string settingsLocation = GetSettingsLocation();

            if (settingsLocation == DefaultSettingsLocation())
            {
                if (!Directory.Exists(settingsLocation))
                    Directory.CreateDirectory(settingsLocation);
            }
            else
            {
                if (!Directory.Exists(settingsLocation))
                    throw new DirectoryNotFoundException(string.Format("Cannot find application settings folder:\n{0}", settingsLocation));
            }
        }

        public static string GetSettingsLocation()
        {
            string customLocation = Properties.Settings.Default.Settings_Location;

            if (!string.IsNullOrEmpty(customLocation))
                return customLocation;

            return DefaultSettingsLocation();
        }

        private static string DefaultSettingsLocation()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"NETworkManager\Settings");
        }

        private static List<WakeOnLanTemplate> DeserializeWakeOnLanTempaltes(string filePath)
        {
            List<WakeOnLanTemplate> list = new List<WakeOnLanTemplate>();

            XmlSerializer serializer = new XmlSerializer(typeof(List<WakeOnLanTemplate>));

            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                var test = (List<WakeOnLanTemplate>)(serializer.Deserialize(stream));
                list.AddRange(test);
            }

            return list;
        }

        public static List<WakeOnLanTemplate> GetWakeOnLanTemplates()
        {
            string filePath = Path.Combine(GetSettingsLocation(), Properties.Settings.Default.FileName_WakeOnLanTemplates);

            if (File.Exists(filePath))
                return DeserializeWakeOnLanTempaltes(filePath);

            return new List<WakeOnLanTemplate>();
        }
         
        private static void SerializeWakeOnLanTemplates(List<WakeOnLanTemplate> list, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<WakeOnLanTemplate>));

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, list);
            }
        }

        public static void SaveWakeOnLanTemplates(List<WakeOnLanTemplate> list)
        {
            string filePath = Path.Combine(GetSettingsLocation(), Properties.Settings.Default.FileName_WakeOnLanTemplates);

            SerializeWakeOnLanTemplates(list, filePath);
        }
    }
}
