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

        public static List<WakeOnLanTemplate> DeseializeWakeOnLanTempaltes(string path)
        {
            List<WakeOnLanTemplate> list = new List<WakeOnLanTemplate>();

            XmlSerializer serializer = new XmlSerializer(typeof(List<WakeOnLanTemplate>));

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                var test = (List<WakeOnLanTemplate>)(serializer.Deserialize(stream));
                list.AddRange(test);
            }

            return list;
        }

        public static void SerializeWakeOnLanTemplates(List<WakeOnLanTemplate> list, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<WakeOnLanTemplate>));

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, list);
            }
        }
    }
}
