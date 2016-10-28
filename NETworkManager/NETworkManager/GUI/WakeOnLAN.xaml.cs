using System;
using System.Windows;
using MahApps.Metro.Controls;
using NETworkManager.GUI.ViewModels;

namespace NETworkManager.GUI
{
    /// <summary>
    /// Interaktionslogik für WakeOnLAN.xaml
    /// </summary>
    public partial class WakeOnLAN : MetroWindow
    {
        private ViewModelWakeOnLan viewModel = new ViewModelWakeOnLan();

        public WakeOnLAN()
        {
            InitializeComponent();
            DataContext = viewModel;
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
            try
            {
                viewModel.WakeUp();
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
    }
}
