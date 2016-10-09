using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace NETworkManager.Core.AppLauncher
{
    [Serializable]
    public class AppInfo
    {
        [XmlIgnore]
        public string Name { get; set; }

        [XmlIgnore]
        public int ID { get; set; }

        [XmlIgnore]
        public Canvas Icon { get; set; }

        public AppInfo()
        {

        }

        public AppInfo(int id)
        {
            ID = id;
        }

        public AppInfo(string name, int id, Canvas icon)
        {
            Name = name;
            ID = id;
            Icon = icon;
        }
    }

}
