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

namespace NETworkManager.GUI
{
    /// <summary>
    /// Interaktionslogik für WakeOnLAN.xaml
    /// </summary>
    public partial class WakeOnLAN : MetroWindow, INotifyPropertyChanged
    {
        public string MACAddress { get; set; }
        public string BroadcastAddress { get; set; }
        public string Port { get; set; }

        public WakeOnLAN()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void MetroWindowWakeOnLAN_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSettings();
        }

        private void LoadSettings()
        {
            txtBroadcast.Text = Properties.Settings.Default.WakeOnLan_Broadcast;
            txtPort.Text = Convert.ToString(Properties.Settings.Default.WakeOnLan_Port);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private void btnWakeUp_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(comboBoxMACAddress))
                return;

            WakeUp(comboBoxMACAddress.Text, txtBroadcast.Text, txtPort.Text);
        }

        private void WakeUp(string mac, string broadcast, string port)
        {
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
