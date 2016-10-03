using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NETworkManager.Core.Appearance;
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
        public MainWindow()
        {
            InitializeComponent();
        }
                
        private void MetroWindowMain_Loaded(object sender, RoutedEventArgs e)
        {
            // Load localization
            LocalizationController.LoadLocalization();

            // Load appearance
            AppearanceController.LoadAppearance();
        }
        
        #region RightWindowCommands
        private async void btnGithub_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(Properties.Resources.GitHubProjectURL);
            }
            catch (Exception ex)
            {
                MetroDialogSettings dialogSettings = new MetroDialogSettings()
                {
                    AffirmativeButtonText = "OK",
                    AnimateShow = true,
                    AnimateHide = false
                };

                string dialogMessage = string.Format("Fehler beim öffnen der Github Project Seite.\n\nException:\n{0}", ex.Message);

                await this.ShowMessageAsync("Error", dialogMessage, MessageDialogStyle.Affirmative, dialogSettings);
            }
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Settings window = new Settings()
            {
                Owner = this
            };

            window.ShowDialog();
        }
        #endregion
    }
}
