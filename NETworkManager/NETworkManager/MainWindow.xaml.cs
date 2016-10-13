﻿using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NETworkManager.Core.Appearance;
using NETworkManager.Core.AppLauncher;
using NETworkManager.Core.Localization;
using NETworkManager.Core.Settings;
using NETworkManager.GUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NETworkManager
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        #region Load
        public MainWindow()
        {
            // Load localization
            LocalizationController.LoadLocalization();

            // Load appearance
            AppearanceController.LoadAppearance();
            
            InitializeComponent();

            // Set filter for ListView Apps 
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvApps.ItemsSource);
            view.Filter = SearchFilter;
        }

        private void MetroWindowMain_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
        #endregion

        #region RightWindowCommands
        private async void btnGithub_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(Properties.Resources.GitHubProjectURL);
            }
            catch (Exception ex)
            {
                string dialogMessage = string.Format("Fehler beim öffnen der Github Project Seite.\n\nException:\n{0}", ex.Message);

                await this.ShowMessageAsync("Error", dialogMessage, MessageDialogStyle.Affirmative);
            }
        }

        private async void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Settings settingsWindow = new Settings()
            {
                Owner = this
            };

            settingsWindow.ShowDialog();

            if (settingsWindow.RestartRequiered)
            {
                string localizedHeader = Application.Current.Resources["LocalizedString_RestartRequired"] as string;
                string localizedMessage = Application.Current.Resources["LocalizedString_RestartRequiredAfterSettingsChanged"] as string;

                await this.ShowMessageAsync(localizedHeader, localizedMessage, MessageDialogStyle.Affirmative);
            }
        }
        #endregion

        #region Events
        private void listViewApps_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AppLauncher.StartApp(lvApps.SelectedItem as AppInfo);
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvApps.ItemsSource).Refresh();
        }
        #endregion

        #region ListView Apps Filter
        private bool SearchFilter(object item)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
                return true;
            else
                return ((item as AppInfo).Name.IndexOf(txtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        #endregion
    }
}
