using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NETworkManager.Core.Network
{
    public class SubnetInfo
    {
        public IPAddress NetworkID { get; set; }
        public IPAddress Broadcast { get; set; }
        public int IPs { get; set; }
        public int Hosts { get; set; }
    }
}
