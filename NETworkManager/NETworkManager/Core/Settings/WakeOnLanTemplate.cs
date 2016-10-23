using System;

namespace NETworkManager.Core.Settings
{
    [Serializable]
    public class WakeOnLanTemplate
    {
        public string MAC { get; set; }
        public string Description { get; set; }

        public WakeOnLanTemplate()
        {

        }

        public WakeOnLanTemplate(string mac, string description)
        {
            MAC = mac;
            Description = description;
        }
    }
}
