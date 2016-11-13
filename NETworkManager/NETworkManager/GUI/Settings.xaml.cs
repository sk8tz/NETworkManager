using MahApps.Metro;
using MahApps.Metro.Controls;
using NETworkManager.Core.Appearance;
using NETworkManager.Core.Localization;
using NETworkManager.Core.Settings;
using NETworkManager.GUI.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NETworkManager.GUI
{
    /// <summary>
    /// Interaktionslogik für Settings.xaml
    /// </summary>
    public partial class Settings : MetroWindow
    {
        private SettingsViewModel viewModel = new SettingsViewModel();

        public Settings()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel.LoadSettings();
        }
      
        private void MetroWindow_Closing(object sender, CancelEventArgs e)
        {
            viewModel.SaveSettings();
            // Dialog if changed...
        }
        

        private void btnChangeSettingsLocation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MetroWindowSettings_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
