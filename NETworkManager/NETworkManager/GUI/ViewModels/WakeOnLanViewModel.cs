using NETworkManager.Core.Network;
using NETworkManager.Core.Settings;
using NETworkManager.GUI.Interface;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
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

        private bool _isLoading = true;
        private bool _templatesChanged = true;

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

        private string _broadcast;
        public string Broadcast
        {
            get { return _broadcast; }
            set
            {
                if (value == _broadcast)
                    return;

                _broadcast = value;
                OnPropertyChanged("Broadcast");
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

        private string _addTemplateMACAddress;
        public string AddTemplateMACAddress
        {
            get { return _addTemplateMACAddress; }
            set
            {
                if (value == _addTemplateMACAddress)
                    return;

                _addTemplateMACAddress = value;
                OnPropertyChanged("AddTemplateMACAddress");
            }
        }

        private string _addTemplateHostname;
        public string AddTemplateHostname
        {
            get { return _addTemplateHostname; }
            set
            {
                if (value == _addTemplateHostname)
                    return;

                _addTemplateHostname = value;
                OnPropertyChanged("AddTemplateHostname");
            }
        }

        private string _addTemplateBroadcast;
        public string AddTemplateBroadcast
        {
            get { return _addTemplateBroadcast; }
            set
            {
                if (value == _addTemplateBroadcast)
                    return;

                _addTemplateBroadcast = value;
                OnPropertyChanged("AddTemplateBroadcast");
            }
        }

        private string _addTemplatePort;
        public string AddTemplatePort
        {
            get { return _addTemplatePort; }
            set
            {
                if (value == _addTemplatePort)
                    return;

                _addTemplatePort = value;
                OnPropertyChanged("AddTemplatePort");
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

                if (!_isLoading)
                    _templatesChanged = true;

                _wakeOnLanTemplates = value;
                OnPropertyChanged("WakeOnLanTemplates");
            }
        }

        private WakeOnLanInfo _selectedItemWakeOnLanInfo = new WakeOnLanInfo();
        public WakeOnLanInfo SelectedItemWakeOnLanInfo
        {
            get { return _selectedItemWakeOnLanInfo; }
            set
            {
                if (value == _selectedItemWakeOnLanInfo)
                    return;

                if (value != null)
                {
                    Port = value.Port;
                    Broadcast = value.Broadcast;
                }

                _selectedItemWakeOnLanInfo = value;
                OnPropertyChanged("SelectedItemWakeOnLanInfo");
            }
        }

        public WakeOnLanViewModel()
        {
            LoadTemplates();

            Port = Properties.Resources.WakeOnLan_DefaultPort;

            _isLoading = false;
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
            MagicPacket.Send(MagicPacket.Create(MACAddress), IPAddress.Parse(Broadcast), int.Parse(Port));
        }

        public ICommand AddTemplateCommand
        {
            get { return new RelayCommand(p => AddTemplateAction()); }
        }

        public void AddTemplateAction()
        {
            WakeOnLanInfo template = new WakeOnLanInfo
            {
                MAC = AddTemplateMACAddress,
                Broadcast = AddTemplateBroadcast,
                Hostname = AddTemplateHostname,
                Port = AddTemplatePort,
            };

            WakeOnLanTemplates.Add(template);
        }

        public void SaveTemplates()
        {
            if (_templatesChanged)
                SettingsController.SaveWakeOnLanTemplates(new List<WakeOnLanInfo>(WakeOnLanTemplates));
        }
    }
}
