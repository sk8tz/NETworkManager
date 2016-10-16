using System;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace NETworkManager.GUI
{
    [Serializable]
    public class ApplicationInfo
    {
        [XmlIgnore]
        public string Name { get; set; }

        [XmlIgnore]
        public int ID { get; set; }

        [XmlIgnore]
        public Canvas Icon { get; set; }

        public ApplicationInfo()
        {

        }

        public ApplicationInfo(int id)
        {
            ID = id;
        }

        public ApplicationInfo(string name, int id, Canvas icon)
        {
            Name = name;
            ID = id;
            Icon = icon;
        }
    }

}
