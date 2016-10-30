using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NETworkManager.Core.Appearance;
using NETworkManager.Core.Localization;
using NETworkManager.GUI;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using NETworkManager.Core.Settings;

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
            
            // Check Settings
            try
            {
                SettingsController.VerifySettings();
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message);
            }

            InitializeComponent();           

            // Set a filter for ListView Apps and sort them
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvApps.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
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
                TestFlyout.IsOpen = true;
                string localizedHeader = Application.Current.Resources["LocalizedString_RestartRequired"] as string;
                string localizedMessage = Application.Current.Resources["LocalizedString_RestartRequiredAfterSettingsChanged"] as string;

                await this.ShowMessageAsync(localizedHeader, localizedMessage, MessageDialogStyle.Affirmative);
            }
        }
        #endregion

        #region Events
        private void listViewApps_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ApplicationController.OpenApplication(lvApps.SelectedItem as ApplicationInfo);
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
                return ((item as ApplicationInfo).Name.IndexOf(txtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        #endregion
    }
}
