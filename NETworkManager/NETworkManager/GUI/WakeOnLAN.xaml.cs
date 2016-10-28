using System;
using System.Text;
using System.Windows;
using MahApps.Metro.Controls;
using NETworkManager.Core.Network;
using System.Text.RegularExpressions;
using System.Net;
using NETworkManager.GUI.ViewModels;

namespace NETworkManager.GUI
{
    /// <summary>
    /// Interaktionslogik für WakeOnLAN.xaml
    /// </summary>
    public partial class WakeOnLAN : MetroWindow
    {
        private WakeOnLanViewModel viewModel = new WakeOnLanViewModel();

        public WakeOnLAN()
        {
            InitializeComponent();
            DataContext = viewModel;

            viewModel.LoadTemplates();
        }

        private void MetroWindowWakeOnLAN_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSettings();
        }

        private void LoadSettings()
        {
            viewModel.BroadcastAddress = Properties.Settings.Default.WakeOnLan_Broadcast;
            viewModel.Port = Convert.ToString(Properties.Settings.Default.WakeOnLan_Port);
        }
               
        #region Buttons
        private void btnWakeUp_Click(object sender, RoutedEventArgs e)
        {
            WakeUp();
        }

        private void btnSaveTemplates_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveTemplates();
        }
        #endregion

        private void WakeUp()
        {
            string mac = viewModel.MACAddress;
            string broadcast = viewModel.BroadcastAddress;
            string port = viewModel.Port;

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
    }
}
