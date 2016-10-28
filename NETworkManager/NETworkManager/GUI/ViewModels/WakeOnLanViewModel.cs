using NETworkManager.Core.Settings;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace NETworkManager.GUI.ViewModels
{
    class WakeOnLanViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        string _MACAddress;
        public string MACAddress
        {
            get { return _MACAddress; }
            set
            {
                if (value != _MACAddress)
                {
                    _MACAddress = value;
                    OnPropertyChanged("MACAddress");
                }
            }
        }

        private string _broadcastAddress;
        public string BroadcastAddress
        {
            get { return _broadcastAddress; }
            set
            {
                if (value != _broadcastAddress)
                {
                    _broadcastAddress = value;
                    OnPropertyChanged("BroadcastAddress");
                }
            }
        }

        string _port;
        public string Port
        {
            get { return _port; }
            set
            {
                if (value != _port)
                {
                    _port = value;
                    OnPropertyChanged("Port");
                }
            }
        }

        ObservableCollection<WakeOnLanTemplate> _wakeOnLanTemplates = new ObservableCollection<WakeOnLanTemplate>();
        public ObservableCollection<WakeOnLanTemplate> WakeOnLanTemplates
        {
            get { return _wakeOnLanTemplates; }
            set { _wakeOnLanTemplates = value; }
        }

        string path = "WakeOnLanTemplates.xml";

        public void LoadTemplates()
        {
            if (File.Exists(path))
            {
                List<WakeOnLanTemplate> list = SettingsController.DeserializeWakeOnLanTempaltes(path);

                foreach (WakeOnLanTemplate template in list)
                {
                    WakeOnLanTemplates.Add(template);
                }
            }
        }

        public void SaveTemplates()
        {
            List<WakeOnLanTemplate> list = new List<WakeOnLanTemplate>(WakeOnLanTemplates);

            SettingsController.SerializeWakeOnLanTemplates(list, path);
        }
    }
}
