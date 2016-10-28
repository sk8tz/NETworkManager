using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETworkManager.GUI.ViewModels
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        // Path to the folder, where the settings are stored
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
    }
}
