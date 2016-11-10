using NETworkManager.Core.Network;
using NETworkManager.Core.Settings;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

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

        private string _port;
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

        private ObservableCollection<WakeOnLanInfo> _wakeOnLanTemplates = new ObservableCollection<WakeOnLanInfo>();
        public ObservableCollection<WakeOnLanInfo> WakeOnLanTemplates
        {
            get { return _wakeOnLanTemplates; }
            set
            {
                if (value != _wakeOnLanTemplates)
                {
                    _wakeOnLanTemplates = value;
                    OnPropertyChanged("WakeOnLanTemplates");
                }
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
            // Regex to replace "-" and ":" in MAC-Address
            Regex regex = new Regex("-|:");

            // Convert string into byte array
            byte[] macBytes = Encoding.ASCII.GetBytes(regex.Replace(MACAddress, ""));

            // Create a magic packet
            byte[] magicPacket = MagicPacket.CreateFromBytes(macBytes);

            // Parse string into IP-Address
            IPAddress broadcastAddr = IPAddress.Parse(BroadcastAddress);

            // Convert the port from string to int
            int portNum = int.Parse(Port);

            // Send the magic packet
            MagicPacket.Send(magicPacket, broadcastAddr, portNum);
        }
    }
}
