using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using NETworkManager.Core.Network;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace NETworkManager.GUI
{
    /// <summary>
    /// Interaktionslogik für WakeOnLAN.xaml
    /// </summary>
    public partial class WakeOnLAN : MetroWindow, INotifyPropertyChanged
    {
        public WakeOnLAN()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string MACAddress { get; set; }

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

            WakeUp(comboBoxMACAddress.Text);
        }

        private void WakeUp(string mac)
        {
            // Regex to replace "-" and ":" in MAC-Address
            Regex regex = new Regex("-|:");

            // Convert string into byte array
            byte[] macBytes = Encoding.ASCII.GetBytes(regex.Replace(mac, ""));

            // Create a magic packet
            byte[] magicPacket = MagicPacket.Create(macBytes);

            // Send the magic packet
        }
    }
}
