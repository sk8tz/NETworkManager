using NETworkManager.Core.Network;
using NETworkManager.Core.Settings;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;

namespace NETworkManager.GUI.ViewModels
{
    class WakeOnLanViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private string _MACAddress;
        public string MACAddress
        {
            get { return _MACAddress; }
            set
            {
                if (value == _MACAddress)
                    return;

                _MACAddress = value;
                OnPropertyChanged("MACAddress");
            }
        }

        private string _broadcastAddress;
        public string BroadcastAddress
        {
            get { return _broadcastAddress; }
            set
            {
                if (value == _broadcastAddress)
                    return;

                _broadcastAddress = value;
                OnPropertyChanged("BroadcastAddress");
            }
        }

        private string _port;
        public string Port
        {
            get { return _port; }
            set
            {
                if (value == _port)
                    return;

                _port = value;
                OnPropertyChanged("Port");
            }
        }

        private ObservableCollection<WakeOnLanInfo> _wakeOnLanTemplates = new ObservableCollection<WakeOnLanInfo>();
        public ObservableCollection<WakeOnLanInfo> WakeOnLanTemplates
        {
            get { return _wakeOnLanTemplates; }
            set
            {
                if (value == _wakeOnLanTemplates)
                    return;

                _wakeOnLanTemplates = value;
                OnPropertyChanged("WakeOnLanTemplates");
            }
        }

        public WakeOnLanViewModel()
        {
            LoadTemplates();
        }

        public void LoadTemplates()
        {
            foreach (WakeOnLanInfo template in SettingsController.GetWakeOnLanTemplates())
            {
                WakeOnLanTemplates.Add(template);
            }
        }

        public void SaveTemplates()
        {
            SettingsController.SaveWakeOnLanTemplates(new List<WakeOnLanInfo>(WakeOnLanTemplates));
        }

        public void WakeUp()
        {
            MagicPacket.Send(MagicPacket.Create(MACAddress), IPAddress.Parse(BroadcastAddress), int.Parse(Port));
        }
    }
}
