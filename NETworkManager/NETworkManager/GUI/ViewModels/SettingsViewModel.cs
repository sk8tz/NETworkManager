using MahApps.Metro;
using NETworkManager.Core.Appearance;
using NETworkManager.Core.Localization;
using NETworkManager.Core.Settings;
using System.ComponentModel;

namespace NETworkManager.GUI.ViewModels
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private bool _settingsChanged;
        public bool SettingsChanged
        {
            get { return _settingsChanged; }
            set
            {
                if (value != _settingsChanged)
                    _settingsChanged = value;
            }
        }

        private bool _restartRequired;
        public bool RestartRequired
        {
            get { return _restartRequired; }
            set
            {
                if (value != _restartRequired)
                    _restartRequired = value;
            }
        }

        #region Apperance 
        private AppTheme _selectedAppTheme;
        public AppTheme SelectedAppTheme
        {
            get { return _selectedAppTheme; }
            set
            {
                if (value != _selectedAppTheme)
                {
                    _selectedAppTheme = value;

                    AppearanceController.ChangeAppTheme(_selectedAppTheme.Name);
                    Properties.Settings.Default.Appearance_AppTheme = _selectedAppTheme.Name;
                    SettingsChanged = true;

                    OnPropertyChanged("SelectedAppTheme");
                }
            }
        }

        private Accent _selectdAccent;
        public Accent SelectedAccent
        {
            get { return _selectdAccent; }
            set
            {
                if (value != _selectdAccent)
                {
                    _selectdAccent = value;

                    AppearanceController.ChangeAccent(_selectdAccent.Name);
                    Properties.Settings.Default.Appearance_Accent = _selectdAccent.Name;
                    SettingsChanged = true;

                    OnPropertyChanged("SelectedAccent");
                }
            }
        }

        #endregion

        #region Localization
        private LocalizationInfo _selectedLocalization;
        public LocalizationInfo SelectedLocalization
        {
            get { return _selectedLocalization; }
            set
            {
                if (value != _selectedLocalization)
                {
                    _selectedLocalization = value;

                    LocalizationController.ChangeLocalization(_selectedLocalization);
                    Properties.Settings.Default.Localization_CultureCode = _selectedLocalization.Code;
                    SettingsChanged = true;
                    RestartRequired = true;

                    OnPropertyChanged("SelectedLocalization");
                }
            }
        }
        #endregion

        #region Settings
        private string _settingsLocationFolder;
        public string SettingsLocationFolder
        {
            get { return _settingsLocationFolder; }
            set
            {
                if (value != _settingsLocationFolder)
                {
                    _settingsLocationFolder = value;
                    OnPropertyChanged("SettingsLocationFolder");
                }
            }
        }

        private bool _settingsPortable;
        public bool SettingsPortable
        {
            get { return _settingsPortable; }
            set
            {
                if (value != _settingsPortable)
                {
                    _settingsPortable = value;
                    OnPropertyChanged("SettingsPortable");
                }
            }
        }
        #endregion

        public void LoadSettings()
        {
            // Apperance
            SelectedAppTheme = ThemeManager.DetectAppStyle().Item1;
            SelectedAccent = ThemeManager.DetectAppStyle().Item2;

            // Localization 
            SelectedLocalization = LocalizationController.LocalizationList.Find(a => a.Code == LocalizationController.CurrentLocalization.Code);

            // Settings
            SettingsLocationFolder = SettingsController.GetSettingsLocation();
            SettingsPortable = SettingsController.IsPortable();
        }

        public void SaveSettings()
        {
            if (SettingsChanged)
                Properties.Settings.Default.Save();
        }
    }
}
