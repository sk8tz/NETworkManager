using System;

namespace NETworkManager.Core.CommandLine
{
    public static class CommandLineParser
    {
        public static CommandLineArgsInfo GetCommandLineArgs()
        {
            string[] args = Environment.GetCommandLineArgs();

            CommandLineArgsInfo commandLineArgs = new CommandLineArgsInfo();

            // Detect start parameters
            foreach (string arg in args)
            {
                if (arg.StartsWith("--"))
                {
                    string argument = arg.ToLower().TrimStart('-');

                    if (string.Equals(argument, Properties.Resources.StartParameter_Autostart, StringComparison.OrdinalIgnoreCase))
                        commandLineArgs.Autostart = true;
                }
            }

            return commandLineArgs;
        }
    }
}
