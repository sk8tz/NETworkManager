using System;

namespace NETworkManager.Core.Settings
{
    [Serializable]
    public class WakeOnLanTemplate
    {
        public string MAC { get; set; }
        public string Hostname { get; set; }
        public string Port { get; set; }
        public string Broadcast { get; set; }
        public string Description { get; set; }

        public WakeOnLanTemplate()
        {

        }

        public WakeOnLanTemplate(string mac, string hostname, string broadcast, string port, string description)
        {
            MAC = mac;
            Hostname = hostname;
            Broadcast = broadcast;
            Port = port;
            Description = description;
        }

        public override string ToString()
        {
            return MAC;
        }
    }
}
