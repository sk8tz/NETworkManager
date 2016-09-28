using MahApps.Metro.Controls;
using NETworkManager.Core.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace NETworkManager.GUI
{
    /// <summary>
    /// Interaktionslogik für Settings.xaml
    /// </summary>
    public partial class Settings : MetroWindow
    {

        // Prevent execution of event while loading the window
        private bool _isLoading = true;

        // Indicates whether the settings have been changed
        private bool _settingsChanged;

        public Settings()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {            
            listBoxLanguage.SelectedIndex = LocalizationController.LocalizationList.FindIndex(a => a.Code == LocalizationController.CurrentLocalization.Code);

            // Enable executing of events, when loading is finished
            _isLoading = false;
        }

        private void MetroWindow_Closing(object sender, CancelEventArgs e)
        {
            // Save settings if they have changed
            if (_settingsChanged)
                Properties.Settings.Default.Save();
        }

        private void listBoxLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Prevent executing while loading
            if (_isLoading)
                return;

            // Get selected localization and apply it
            LocalizationInfo info = listBoxLanguage.SelectedItem as LocalizationInfo;
            LocalizationController.ChangeLocalization(info);

            // Save selected culture code in settings
            Properties.Settings.Default.Localization_CultureCode = info.Code;

            // Indicates that the settings are saved when closing the settings dialog
            _settingsChanged = true;
        }
    }
}
