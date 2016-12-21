using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETworkManager.Core.CommandLine
{
    [Serializable]
    public class CommandLineArgsInfo
    {
        public bool Autostart { get; set; }

        public CommandLineArgsInfo()
        {

        }

        public CommandLineArgsInfo(bool autostart)
        {
            Autostart = autostart;
        }
    }
}
