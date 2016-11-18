using MahApps.Metro;
using NETworkManager.Core.Appearance;
using NETworkManager.Core.Localization;
using NETworkManager.Core.Settings;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Forms;
using NETworkManager.GUI.Interface;

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

        private AppTheme _appThemeSelectedItem;
        public AppTheme AppThemeSelectedItem
        {
            get { return _appThemeSelectedItem; }
            set
            {
                if (value == _appThemeSelectedItem)
                    return;

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

                _localizationSelectedIndex = value;
                OnPropertyChanged("LocalizationSelectedIndex");
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
                if (value != _settingsPortable)
                    return;

                _settingsPortable = value;
                OnPropertyChanged("SettingsPortable");
            }
        }

        public SettingsViewModel()
        {
            LoadSettings();
        }

        public void LoadSettings()
        {
            // Apperance
            AppThemeSelectedItem = ThemeManager.DetectAppStyle().Item1;
            AccentSelectedItem = ThemeManager.DetectAppStyle().Item2;

            // Localization 
            LocalizationSelectedIndex = LocalizationController.LocalizationList.FindIndex(a => a.Code == LocalizationController.CurrentLocalization.Code);

            // Settings
            SettingsLocationSelectedPath = SettingsController.GetSettingsLocation();
            SettingsPortable = SettingsController.IsPortable();
        }

        public void SaveSettings()
        {
            if (SettingsChanged)
                Properties.Settings.Default.Save();
        }

        public void ChangeAppThemeOnSelectionChanged()
        {
            AppearanceController.ChangeAppTheme(AppThemeSelectedItem.Name);

            SettingsChanged = true;

            Properties.Settings.Default.Appearance_AppTheme = AppThemeSelectedItem.Name;
        }

        public void ChangeAccentOnSelectionChanged()
        {
            AppearanceController.ChangeAccent(AccentSelectedItem.Name);

            SettingsChanged = true;

            Properties.Settings.Default.Appearance_Accent = AccentSelectedItem.Name;
        }

        public void ChangeLocalizationOnSelectionChanged()
        {
            LocalizationController.ChangeLocalization(LocalizationController.LocalizationList[LocalizationSelectedIndex]);

            SettingsChanged = true;
            RestartRequired = true;

            Properties.Settings.Default.Localization_CultureCode = LocalizationController.CurrentLocalization.Code;
        }

        public ICommand BrowseFolderCommand
        {
            get { return new RelayCommand(p => BrowseFolderAction()); }
        }

        public void BrowseFolderAction()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            DialogResult dialogResult = dialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
                SettingsLocationSelectedPath = dialog.SelectedPath;
        }       
    }
}
