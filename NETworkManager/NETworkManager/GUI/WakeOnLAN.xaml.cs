using System;
using System.Windows;
using MahApps.Metro.Controls;
using NETworkManager.GUI.ViewModels;
using System.Windows.Controls;
using NETworkManager.Core.Network;
using System.Windows.Input;

namespace NETworkManager.GUI
{
    /// <summary>
    /// Interaktionslogik für WakeOnLAN.xaml
    /// </summary>
    public partial class WakeOnLAN : MetroWindow
    {
        private WakeOnLanViewModel viewModel = new WakeOnLanViewModel();

        private double savedWidth;
        private double savedHeight;

        private bool _isLoading = true;

        public WakeOnLAN()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void MetroWindowWakeOnLAN_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSettings();

            _isLoading = false;
        }

        #region Load and save window specific settings
        private void LoadSettings()
        {
            viewModel.BroadcastAddress = Properties.Settings.Default.WakeOnLan_Broadcast;
            viewModel.Port = Convert.ToString(Properties.Settings.Default.WakeOnLan_Port);
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.WakeOnLan_Broadcast = viewModel.BroadcastAddress;
            Properties.Settings.Default.WakeOnLan_Port = Convert.ToInt32(viewModel.Port);
            Properties.Settings.Default.Save();
        }
        #endregion

        #region Buttons
        private void btnWakeUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                viewModel.WakeUp();
                SaveSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.Current.Resources["LocalizedString_Error"] as string, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSaveTemplates_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                viewModel.SaveTemplates();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.Current.Resources["LocalizedString_Error"] as string, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isLoading)
                return;

            // Resize the window for better template view/edit
            if (tabControlTemplate.SelectedIndex == 0)
            {
                if (savedWidth != 0)
                    Width = savedWidth;

                if (savedHeight != 0)
                    Height = savedHeight;
            }
            else if (tabControlTemplate.SelectedIndex == 1)
            {
                savedWidth = Width;
                savedHeight = Height;

                if (Width < 700)
                    Width = 700;

                if (Height < 450)
                    Height = 450;
            }
        }

        private void cbMACAddress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WakeOnLanInfo template = cbMACAddress.SelectedItem as WakeOnLanInfo;

            if (template == null)
                return;

            if (!string.IsNullOrEmpty(template.Broadcast))
                viewModel.BroadcastAddress = template.Broadcast;

            if (!string.IsNullOrEmpty(template.Port))
                viewModel.Port = template.Port;
        }

        private void MetroWindowWakeOnLAN_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
