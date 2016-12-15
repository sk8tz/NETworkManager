using MahApps.Metro;
using NETworkManager.Core.Appearance;
using NETworkManager.Core.Localization;
using NETworkManager.Core.Settings;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Forms;
using NETworkManager.GUI.Interface;
using System.IO;
using NETworkManager.Core.Autostart;

namespace NETworkManager.GUI.ViewModels
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private bool _isLoading = true;

        private bool _settingsChanged;
        public bool SettingsChanged
        {
            get { return _settingsChanged; }
            set
            {
                if (value == _settingsChanged)
                    return;

                _settingsChanged = value;
            }
        }

        private bool _restartRequired;
        public bool RestartRequired
        {
            get { return _restartRequired; }
            set
            {
                if (value == _restartRequired)
                    return;

                _restartRequired = value;               
            }
        }

        private bool _startApplicationWithWindows;
        public bool StartApplicationWithWindows
        {
            get { return _startApplicationWithWindows; }
            set
            {
                if (value == _startApplicationWithWindows)
                    return;

                if (!_isLoading)
                {
                    if (value)
                        AutostartManager.Enable();
                    else
                        AutostartManager.Disable();
                }

                _startApplicationWithWindows = value;
                OnPropertyChanged("StartApplicationWithWindows");
            }
        }

        private bool _startApplicationMinimized;
        public bool StartApplicationMinimized
        {
            get { return _startApplicationMinimized; }
            set
            {
                if (value == _startApplicationMinimized)
                    return;

                if (!_isLoading)
                {
                    Properties.Settings.Default.Application_StartApplicationMinimized = value;
                    SettingsChanged = true;
                }

                _startApplicationMinimized = value;
                OnPropertyChanged("StartApplicationMinimized");
            }
        }

        private bool _minimizeToTrayOnClose;
        public bool MinimizeToTrayOnClose
        {
            get { return _minimizeToTrayOnClose; }
            set
            {
                if (value == _minimizeToTrayOnClose)
                    return;

                if (!_isLoading)
                {
                    Properties.Settings.Default.Application_MinimizeToTrayOnClose = value;
                    SettingsChanged = true;
                }

                _minimizeToTrayOnClose = value;
                OnPropertyChanged("MinimizeToTrayOnClose");
            }
        }

        private bool _minimizeToTrayOnMinimize;
        public bool MinimizeToTrayOnMinimize
        {
            get { return _minimizeToTrayOnMinimize; }
            set
            {
                if (value == _minimizeToTrayOnMinimize)
                    return;

                if (!_isLoading)
                {
                    Properties.Settings.Default.Application_MinimizeToTrayOnMinimize = value;
                    SettingsChanged = true;
                }

                _minimizeToTrayOnMinimize = value;
                OnPropertyChanged("MinimizeToTrayOnMinimize");
            }
        }

        private AppTheme _appThemeSelectedItem;
        public AppTheme AppThemeSelectedItem
        {
            get { return _appThemeSelectedItem; }
            set
            {
                if (value == _appThemeSelectedItem)
                    return;

                if (!_isLoading)
                {
                    AppearanceController.ChangeAppTheme(value.Name);

                    Properties.Settings.Default.Appearance_AppTheme = value.Name;
                    SettingsChanged = true;
                }

                _appThemeSelectedItem = value;
                OnPropertyChanged("AppThemeSelectedItem");
            }
        }

        private Accent _accentSelectedItem;
        public Accent AccentSelectedItem
        {
            get { return _accentSelectedItem; }
            set
            {
                if (value == _accentSelectedItem)
                    return;

                if (!_isLoading)
                {
                    AppearanceController.ChangeAccent(value.Name);

                    Properties.Settings.Default.Appearance_Accent = value.Name;
                    SettingsChanged = true;
                }

                _accentSelectedItem = value;
                OnPropertyChanged("AccentSelectedItem");
            }
        }

        private int _localizationSelectedIndex;
        public int LocalizationSelectedIndex
        {
            get { return _localizationSelectedIndex; }
            set
            {
                if (value == _localizationSelectedIndex)
                    return;

                if (!_isLoading)
                {
                    LocalizationInfo info = LocalizationController.LocalizationList[value];
                    LocalizationController.ChangeLocalization(info);

                    Properties.Settings.Default.Localization_CultureCode = info.Code;
                    RestartRequired = true;
                    SettingsChanged = true;
                }

                _localizationSelectedIndex = value;
                OnPropertyChanged("LocalizationSelectedItem");
            }
        }

        private string _settingsLocationSelectedPath;
        public string SettingsLocationSelectedPath
        {
            get { return _settingsLocationSelectedPath; }
            set
            {
                if (value == _settingsLocationSelectedPath)
                    return;

                _settingsLocationSelectedPath = value;
                OnPropertyChanged("SettingsLocationSelectedPath");
            }
        }

        private bool _settingsPortable;
        public bool SettingsPortable
        {
            get { return _settingsPortable; }
            set
            {
                if (value == _settingsPortable)
                    return;

                if (!_isLoading)
                {
                    // Save settings before moving them
                    SaveSettings();

                    SettingsController.MakeSettingsPortable(value, true);

                    if (!SettingsController.IsPortable)
                    {
                        SettingsLocationSelectedPath = SettingsController.SettingsLocation;

                        Properties.Settings.Default.Settings_Location = SettingsLocationSelectedPath;
                        SettingsChanged = true;
                    }
                }

                _settingsPortable = value;
                OnPropertyChanged("SettingsPortable");
            }
        }

        public SettingsViewModel()
        {
            LoadSettings();

            _isLoading = false;
        }

        public void LoadSettings()
        {
            // General
            StartApplicationWithWindows = AutostartManager.IsEnabled;
            StartApplicationMinimized = Properties.Settings.Default.Application_StartApplicationMinimized;
            MinimizeToTrayOnClose = Properties.Settings.Default.Application_MinimizeToTrayOnClose;
            MinimizeToTrayOnMinimize = Properties.Settings.Default.Application_MinimizeToTrayOnMinimize;

            // Apperance
            AppThemeSelectedItem = ThemeManager.DetectAppStyle().Item1;
            AccentSelectedItem = ThemeManager.DetectAppStyle().Item2;

            // Localization 
            LocalizationSelectedIndex = LocalizationController.LocalizationList.FindIndex(a => a.Code == LocalizationController.CurrentLocalization.Code);

            // Settings
            SettingsLocationSelectedPath = SettingsController.SettingsLocation;
            SettingsPortable = SettingsController.IsPortable;
        }

        public void SaveSettings()
        {
            if (SettingsChanged)
                Properties.Settings.Default.Save();
        }

        public ICommand BrowseFolderCommand
        {
            get { return new RelayCommand(p => BrowseFolderAction()); }
        }

        private void BrowseFolderAction()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (Directory.Exists(SettingsLocationSelectedPath))
                dialog.SelectedPath = SettingsLocationSelectedPath;

            DialogResult dialogResult = dialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
                SettingsLocationSelectedPath = dialog.SelectedPath;
        }

        public ICommand ChangeSettingsCommand
        {
            get { return new RelayCommand(p => ChangeSettingsAction()); }
        }

        private void ChangeSettingsAction()
        {
            SettingsController.ChangeSettingsLocation(SettingsLocationSelectedPath, true);

            Properties.Settings.Default.Settings_Location = SettingsLocationSelectedPath;
            Properties.Settings.Default.Save();
        }
    }
}
