﻿using MahApps.Metro.Controls;
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
using System.IO;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Forms;

namespace NETworkManager
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        System.Windows.Forms.NotifyIcon notifyIcon = new System.Windows.Forms.NotifyIcon();
        WindowState windowState = new WindowState();

        #region Load
        public MainWindow()
        {
            // Load localization
            LocalizationController.LoadLocalization();

            // Load appearance
            AppearanceController.LoadAppearance();

            InitializeComponent();

            // Set a filter for ListView Apps and sort them
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvApps.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            view.Filter = SearchFilter;
        }

        private void MetroWindowMain_Loaded(object sender, RoutedEventArgs e)
        {
            InitNotifyIcon();
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

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Settings settingsWindow = new Settings()
            {
                Owner = this
            };

            settingsWindow.ShowDialog();
        }
        #endregion

        #region NotifyIcon
        private void InitNotifyIcon()
        {
            // Get the application icon for the tray
            using (Stream iconStream = System.Windows.Application.GetResourceStream(new Uri("pack://application:,,,/NETworkManager.ico")).Stream)
            {
                notifyIcon.Icon = new Icon(iconStream);
                notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
                notifyIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(NotifyIcon_MouseDown);
            }
        }

        private void NotifyIcon_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                System.Windows.Controls.ContextMenu menu = (System.Windows.Controls.ContextMenu)FindResource("contextMenuNotifyIcon");
                menu.IsOpen = true;
            }
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            ShowTrayIcon(false);
        }


        private void ShowTrayIcon(bool show)
        {
            // Restore window (normal / maximized)     
            if (!show)
                WindowState = windowState;

            notifyIcon.Visible = show;
            ShowInTaskbar = !show;
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

        private void MetroWindowMain_Closing(object sender, CancelEventArgs e)
        {
            if (Properties.Settings.Default.Application_CloseToTray || true)
            {
                e.Cancel = true;

                windowState = WindowState;

                WindowState = WindowState.Minimized;
                ShowTrayIcon(true);

                return;
            }

            // Dispose the notify icon to prevent errors
            if (notifyIcon != null)
                notifyIcon.Dispose();
        }

        private void MetroWindowMain_StateChanged(object sender, EventArgs e)
        {
            // Handle tray icon
            if (WindowState == WindowState.Minimized)
            {
                if (Properties.Settings.Default.Application_MinimizeToTray)
                    ShowTrayIcon(true);
            }
        }
    }
}
