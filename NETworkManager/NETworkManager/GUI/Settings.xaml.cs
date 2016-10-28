using MahApps.Metro;
using MahApps.Metro.Controls;
using NETworkManager.Core.Appearance;
using NETworkManager.Core.Localization;
using NETworkManager.Core.Settings;
using NETworkManager.GUI.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace NETworkManager.GUI
{
    /// <summary>
    /// Interaktionslogik für Settings.xaml
    /// </summary>
    public partial class Settings : MetroWindow
    {
        // Prevent execution of event while loading the window
        private bool _isLoading = true;

        // Indicates whether the settings have been changed
        private bool _settingsChanged;
        
        // Some settings need a restarts of the application to apply them
        private bool _restartRequiered;
        public bool RestartRequiered
        {
            get { return _restartRequiered; }
            set
            {
                if (value != _restartRequiered)
                {
                    _restartRequiered = value;
        
                }
            }
        }

        private ViewModelSettings viewModel = new ViewModelSettings();

        public Settings()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSettings();

            // Enable executing of events, when loading is finished
            _isLoading = false;
        }

        private void LoadSettings()
        {
            // Appearance
            listViewAppTheme.SelectedItem = ThemeManager.DetectAppStyle().Item1;
            listViewAccent.SelectedItem = ThemeManager.DetectAppStyle().Item2;

            // Localization 
            listBoxLanguage.SelectedIndex = LocalizationController.LocalizationList.FindIndex(a => a.Code == LocalizationController.CurrentLocalization.Code);

            viewModel.SettingsLocationFolder = SettingsController.GetSettingsLocation();
        }

        private void MetroWindow_Closing(object sender, CancelEventArgs e)
        {
            // Save settings if they have changed
            if (_settingsChanged)
                Properties.Settings.Default.Save();
        }

        private void listViewAppTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isLoading)
                return;

            _settingsChanged = true;

            string appThemeName = (listViewAppTheme.SelectedItem as AppTheme).Name;

            AppearanceController.ChangeAppTheme(appThemeName);

            Properties.Settings.Default.Appearance_AppTheme = appThemeName;
        }

        private void listViewAccent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isLoading)
                return;

            _settingsChanged = true;

            string accentName = (listViewAccent.SelectedItem as Accent).Name;

            AppearanceController.ChangeAccent(accentName);

            Properties.Settings.Default.Appearance_Accent = accentName;
        }

        private void listBoxLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Prevent executing while loading
            if (_isLoading)
                return;

            // Indicates that the settings are saved when closing the settings dialog
            _settingsChanged = true;
            RestartRequiered = true;

            // Get selected localization and apply it
            LocalizationInfo info = listBoxLanguage.SelectedItem as LocalizationInfo;
            LocalizationController.ChangeLocalization(info);

            // Save selected culture code in settings
            Properties.Settings.Default.Localization_CultureCode = info.Code;
        }

        private void btnChangeSettingsLocation_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
