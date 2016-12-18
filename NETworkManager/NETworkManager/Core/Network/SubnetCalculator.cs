﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NETworkManager.Core.Network
{
    public static class SubnetCalculator
    {
        public static List< SubnetmaskInfo> SubnetmaskList
        {
            get
            {
                return new List<SubnetmaskInfo>
                {
                    new SubnetmaskInfo(24,"255.255.255.255.0"),
                    new SubnetmaskInfo(25,"255.255.255.255.128")
                };
            }
        }
    }
}
