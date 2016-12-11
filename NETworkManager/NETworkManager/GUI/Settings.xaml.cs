using MahApps.Metro.Controls;
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

        public bool RestartRequired
        {
            get { return viewModel.RestartRequired; }
        }

        public Settings()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void MetroWindow_Closing(object sender, CancelEventArgs e)
        {
            if (viewModel.SettingsChanged)
                viewModel.SaveSettings();
        }

        private void MetroWindowSettings_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
