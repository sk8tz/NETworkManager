using System;

namespace NETworkManager.Core.Settings
{
    [Serializable]
    public class WakeOnLanTemplate
    {
        public string MAC;
        public string Description;

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
