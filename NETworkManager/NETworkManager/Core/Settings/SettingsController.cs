using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using NETworkManager.Core.Network;

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
            return @"Settings";
        }

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
    }
}
