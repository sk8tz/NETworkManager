using System.Windows;

namespace NETworkManager
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ShutdownMode = ShutdownMode.OnLastWindowClose;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();

            // Detect start parameters
            foreach (string arg in e.Args)
            {
                if (arg.StartsWith("-") || arg.StartsWith("/"))
                {
                    string argument = arg.ToLower().TrimStart('-').TrimStart('/');

                    if (argument == NETworkManager.Properties.Resources.StartParameter_Autostart.ToLower())
                    {
                        mainWindow.Autostart = true;
                        mainWindow.Visibility = Visibility.Hidden;
                    }
                }
            }

            mainWindow.Show();
        }
    }
}
