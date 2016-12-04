using NETworkManager.Core.Network;
using NETworkManager.Core.Settings;
using NETworkManager.GUI.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

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

        private WakeOnLanInfo _wakeOnLanInfoSelectedItem;
        public WakeOnLanInfo WakeOnLanInfoSelectedItem
        {
            get { return _wakeOnLanInfoSelectedItem; }
            set
            {
                if (value == _wakeOnLanInfoSelectedItem)
                    return;

                _wakeOnLanInfoSelectedItem = value;
                OnPropertyChanged("WakeOnLanInfoSelectedItem");
            }
        }

        public WakeOnLanViewModel()
        {
            LoadSettings();
            LoadTemplates();
        }

        public void WakeOnLanInfoSelectedItemChanged()
        {
            if (WakeOnLanInfoSelectedItem == null)
                return;

            BroadcastAddress = WakeOnLanInfoSelectedItem.Broadcast;
            Port = WakeOnLanInfoSelectedItem.Port;
        }

        public void LoadSettings()
        {
            MACAddress = Properties.Settings.Default.WakeOnLan_MAC;
            BroadcastAddress = Properties.Settings.Default.WakeOnLan_Broadcast;
            Port = Convert.ToString(Properties.Settings.Default.WakeOnLan_Port);
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.WakeOnLan_MAC = MACAddress;
            Properties.Settings.Default.WakeOnLan_Broadcast = BroadcastAddress;
            Properties.Settings.Default.WakeOnLan_Port = Convert.ToInt32(Port);
            Properties.Settings.Default.Save();
        }

        public void LoadTemplates()
        {
            foreach (WakeOnLanInfo template in SettingsController.GetWakeOnLanTemplates())
            {
                WakeOnLanTemplates.Add(template);
            }
        }

        public ICommand WakeUpCommand
        {
            get { return new RelayCommand(p => WakeUpAction()); }
        }

        private void WakeUpAction()
        {
            MagicPacket.Send(MagicPacket.Create(MACAddress), IPAddress.Parse(BroadcastAddress), int.Parse(Port));

            SaveSettings();
        }

        public ICommand SaveTemplatesCommand
        {
            get { return new RelayCommand(p => SaveTemplatesAction()); }
        }

        private void SaveTemplatesAction()
        {
            SettingsController.SaveWakeOnLanTemplates(new List<WakeOnLanInfo>(WakeOnLanTemplates));
        }
    }
}
