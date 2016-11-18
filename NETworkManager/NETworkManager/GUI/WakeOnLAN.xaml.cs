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

        private bool _isLoading = true;

        public WakeOnLAN()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void MetroWindowWakeOnLAN_Loaded(object sender, RoutedEventArgs e)
        {
            _isLoading = false;
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
                viewModel.SaveSettings();
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
            if (tabControl.SelectedIndex == 0)
            {
                Width = 350;
                Height = 325;
            }
            else if (tabControl.SelectedIndex == 1)
            {
                if (Width < 700)
                    Width = 700;

                if (Height < 450)
                    Height = 450;
            }
        }

        private void cbMACAddress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.WakeOnLanInfoSelectedItemChanged();
        }

        private void MetroWindowWakeOnLAN_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape && tabControl.SelectedIndex != 1)
                Close();
        }
    }
}
