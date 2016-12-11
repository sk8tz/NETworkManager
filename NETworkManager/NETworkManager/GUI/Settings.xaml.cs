﻿using MahApps.Metro.Controls;
using NETworkManager.GUI.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NETworkManager.GUI
{
    /// <summary>
    /// Interaktionslogik für Settings.xaml
    /// </summary>
    public partial class Settings : MetroWindow
    {
        // Prevents the execution of events during loading
        private bool _isLoading = true;

        private SettingsViewModel viewModel = new SettingsViewModel();

        public bool RestartRequired
        {
            get { return viewModel.RestartRequired; }
        }

        public Settings()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _isLoading = false;
        }

        private void MetroWindow_Closing(object sender, CancelEventArgs e)
        {
            if (viewModel.SettingsChanged)
                viewModel.SaveSettings();
        }

        private void MetroWindowSettings_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void toggleSwitchSettingsPortable_IsCheckedChanged(object sender, System.EventArgs e)
        {
            if (_isLoading)
                return;

            viewModel.SettingsPortableIsCheckedChanged();
        }
    }
}
