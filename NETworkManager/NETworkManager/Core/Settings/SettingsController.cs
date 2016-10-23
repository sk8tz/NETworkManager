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

        public static List<WakeOnLanTemplate> GetWakeOnLanTempaltes()
        {
            List<WakeOnLanTemplate> wakeOnLanTemplates = new List<WakeOnLanTemplate>();

            XmlSerializer serializer = new XmlSerializer(typeof(WakeOnLanTemplate));

            if (File.Exists("WakeOnLanTemplates.xml"))
            {
                using (FileStream stream = new FileStream("WakeOnLanTempaltes.xml", FileMode.Open))
                {
                    IEnumerable<WakeOnLanTemplate> wakeOnLanTemplateData = (IEnumerable<WakeOnLanTemplate>)serializer.Deserialize(stream);

                    foreach (WakeOnLanTemplate _wakeOnLanTemplate in wakeOnLanTemplateData)
                    {
                        wakeOnLanTemplates.Add(_wakeOnLanTemplate);
                    }
                }
            }

            return wakeOnLanTemplates;
        }

        public static void SaveWakeOnLanTemplates(List<WakeOnLanTemplate> wakeOnLanTemplates)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(WakeOnLanTemplate));

            using (FileStream stream = new FileStream("WakeOnLanTempaltes.xml", FileMode.Create))
            {
                serializer.Serialize(stream, wakeOnLanTemplates);
            }
        }
    }
}
