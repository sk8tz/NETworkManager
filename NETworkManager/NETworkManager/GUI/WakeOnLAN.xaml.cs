using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using NETworkManager.Core.Network;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Net;
using System.Collections.ObjectModel;
using NETworkManager.Core.Settings;
using System.Collections.Generic;
using System.IO;

namespace NETworkManager.GUI
{
    /// <summary>
    /// Interaktionslogik für WakeOnLAN.xaml
    /// </summary>
    public partial class WakeOnLAN : MetroWindow, INotifyPropertyChanged
    {
        string _MACAddress;
        public string MACAddress
        {
            get { return _MACAddress; }
            set { _MACAddress = value; }
        }

        string _broadcastAddress;
        public string BroadcastAddress
        {
            get { return _broadcastAddress; }
            set { _broadcastAddress = value; }
        }

        string _port;
        public string Port
        {
            get { return _port; }
            set { _port = value; }
        }

        ObservableCollection<WakeOnLanTemplate> _wakeOnLanTemplates = new ObservableCollection<WakeOnLanTemplate>();
        public ObservableCollection<WakeOnLanTemplate> WakeOnLanTemplates
        {
            get { return _wakeOnLanTemplates; }
            set { _wakeOnLanTemplates = value; }
        }

        public WakeOnLAN()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void MetroWindowWakeOnLAN_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSettings();

            LoadTemplates();
        }

        private void LoadSettings()
        {
            txtBroadcast.Text = Properties.Settings.Default.WakeOnLan_Broadcast;
            txtPort.Text = Convert.ToString(Properties.Settings.Default.WakeOnLan_Port);
        }

        string path = "WakeOnLanTemplates.xml";



        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        #region Buttons
        private void btnWakeUp_Click(object sender, RoutedEventArgs e)
        {
            WakeUp();
        }

        private void btnSaveTemplates_Click(object sender, RoutedEventArgs e)
        {
                SaveTemplates();
        }
        #endregion

        private void WakeUp()
        {
            string mac = _MACAddress;
            string broadcast = _broadcastAddress;
            string port = _port;

            try
            {
                // Regex to replace "-" and ":" in MAC-Address
                Regex regex = new Regex("-|:");

                // Convert string into byte array
                byte[] macBytes = Encoding.ASCII.GetBytes(regex.Replace(mac, ""));

                // Create a magic packet
                byte[] magicPacket = MagicPacket.Create(macBytes);

                // Parse string into IP-Address
                IPAddress broadcastAddr = IPAddress.Parse(broadcast);

                // Convert the port from string to int
                int portNum = int.Parse(port);

                // Send the magic packet
                MagicPacket.Send(magicPacket, broadcastAddr, portNum);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.Current.Resources["LocalizedString_Error"] as string, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadTemplates()
        {
            if (File.Exists(path))
            {
                List<WakeOnLanTemplate> list = SettingsController.DeserializeWakeOnLanTempaltes(path);

                foreach (WakeOnLanTemplate template in list)
                {
                    _wakeOnLanTemplates.Add(template);
                }
            }
        }

        private void SaveTemplates()
        {
            List<WakeOnLanTemplate> list = new List<WakeOnLanTemplate>(_wakeOnLanTemplates);

            SettingsController.SerializeWakeOnLanTemplates(list, path);
        }
    }
}
