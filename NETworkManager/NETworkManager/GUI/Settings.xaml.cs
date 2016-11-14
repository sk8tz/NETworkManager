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
        // Prevents the execution of events during loading
        private bool _isLoading = true;

        private SettingsViewModel viewModel = new SettingsViewModel();

        public Settings()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel.LoadSettings();

            _isLoading = false;
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

        private void listViewAppTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isLoading)
                return;

            viewModel.ChangeAppThemeOnSelectionChanged();
        }

        private void listViewAccent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isLoading)
                return;

            viewModel.ChangeAccentOnSelectionChanged();
        }

        private void listBoxLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isLoading)
                return;

            viewModel.ChangeLocalizationOnSelectionChanged();
        }                
    }
}
