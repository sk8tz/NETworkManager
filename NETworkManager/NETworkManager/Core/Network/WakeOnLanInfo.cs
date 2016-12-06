using System;

namespace NETworkManager.Core.Network
{
    [Serializable]
    public class WakeOnLanInfo
    {
        public string MAC { get; set; }
        public string Hostname { get; set; }
        public string Port { get; set; }
        public string Broadcast { get; set; }

        public WakeOnLanInfo()
        {

        }

        public WakeOnLanInfo(string mac, string hostname, string broadcast, string port)
        {
            MAC = mac;
            Hostname = hostname;
            Broadcast = broadcast;
            Port = port;
        }

        public override string ToString()
        {
            return MAC;
        }
    }
}
