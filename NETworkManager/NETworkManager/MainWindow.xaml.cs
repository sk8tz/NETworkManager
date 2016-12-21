using MahApps.Metro.Controls;
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
using System.Windows.Forms;
using NETworkManager.GUI.Interface;
using System.Reflection;
using NETworkManager.Core.Appearance;
using NETworkManager.Core.CommandLine;

namespace NETworkManager
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged
    {
        #region PropertyChangedEventHandler
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion

        #region Variables        
        NotifyIcon notifyIcon = new NotifyIcon();
        CommandLineArgsInfo commandLineArgs;

        private bool _isInTray;

        private bool _flyoutRestartRequiredIsOpen;
        public bool FlyoutRestartRequiredIsOpen
        {
            get { return _flyoutRestartRequiredIsOpen; }
            set
            {
                if (value == _flyoutRestartRequiredIsOpen)
                    return;

                _flyoutRestartRequiredIsOpen = value;
                OnPropertyChanged("FlyoutRestartRequiredIsOpen");
            }
        }
        #endregion

        #region Window load and close events
        public MainWindow()
        {
            // Get command line arguments
            commandLineArgs = CommandLineParser.GetCommandLineArgs();

            // Load localization
            LocalizationController.LoadLocalization();

            // Load appearance
            AppearanceController.LoadAppearance();
            InitializeComponent();
            DataContext = this;

            // Set a filter for ListView Apps and sort them
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvApps.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            view.Filter = SearchFilter;

            // Init notify icon
            InitNotifyIcon();

            if (commandLineArgs.Autostart && Properties.Settings.Default.Application_StartApplicationMinimized)
                HideWindowToTray();
        }

        private void MetroWindowMain_Closing(object sender, CancelEventArgs e)
        {
            if (Properties.Settings.Default.Application_MinimizeToTrayOnClose && !_isInTray)
            {
                e.Cancel = true;

                HideWindowToTray();

                return;
            }

            // Dispose the notify icon to prevent errors
            if (notifyIcon != null)
                notifyIcon.Dispose();
        }
        #endregion

        #region NotifyIcon
        private void InitNotifyIcon()
        {
            // Get the application icon for the tray
            using (Stream iconStream = System.Windows.Application.GetResourceStream(new Uri("pack://application:,,,/NETworkManager.ico")).Stream)
            {
                notifyIcon.Icon = new Icon(iconStream);
                notifyIcon.Text = Title;
                notifyIcon.DoubleClick += new EventHandler(NotifyIcon_DoubleClick);
                notifyIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(NotifyIcon_MouseDown);
                notifyIcon.Visible = Properties.Settings.Default.Application_AlwaysShowIconInTray;
            }
        }

        private void NotifyIcon_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                System.Windows.Controls.ContextMenu trayMenu = (System.Windows.Controls.ContextMenu)FindResource("contextMenuNotifyIcon");
                trayMenu.IsOpen = true;
            }
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            ShowWindowFromTray();
        }

        private void MetroWindowMain_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                if (!_isInTray && Properties.Settings.Default.Application_MinimizeToTrayOnMinimize)
                    HideWindowToTray();
            }
        }

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.ContextMenu menu = sender as System.Windows.Controls.ContextMenu;
            menu.DataContext = this;
        }

        private void HideWindowToTray()
        {
            _isInTray = true;

            notifyIcon.Visible = true;

            Hide();
        }

        private void ShowWindowFromTray()
        {
            _isInTray = false;

            Show();

            if (!Properties.Settings.Default.Application_AlwaysShowIconInTray)
                notifyIcon.Visible = false;
        }

        private void BringWindowToFront()
        {
            if (WindowState == WindowState.Minimized)
                WindowState = WindowState.Normal;

            Activate();
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

        #region ListView search
        private bool SearchFilter(object item)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
                return true;
            else
                return ((item as ApplicationInfo).Name.IndexOf(txtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void MetroWindowMain_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!txtSearch.IsKeyboardFocused && ((e.Key >= Key.A && e.Key <= Key.Z)))
            {
                txtSearch.Focus();
            }
        }
        #endregion

        #region Commands & Actions
        public ICommand OpenGithubProjectCommand
        {
            get { return new RelayCommand(p => OpenGithubProjectAction()); }
        }

        private void OpenGithubProjectAction()
        {
            Process.Start(Properties.Resources.GitHub_ProjectURL);
        }

        public ICommand OpenSettingsCommand
        {
            get { return new RelayCommand(p => OpenSettingsAction()); }
        }

        private void OpenSettingsAction()
        {
            Settings settingsWindow = new Settings();

            if (_isInTray)
                settingsWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            else
                settingsWindow.Owner = this;

            settingsWindow.ShowDialog();

            if (!_isInTray)
                notifyIcon.Visible = Properties.Settings.Default.Application_AlwaysShowIconInTray;

            if (settingsWindow.RestartRequired)
                FlyoutRestartRequiredIsOpen = true;
        }

        public ICommand ShowWindowCommand
        {
            get { return new RelayCommand(p => ShowWindowAction()); }
        }

        private void ShowWindowAction()
        {
            if (_isInTray)
                ShowWindowFromTray();
            else
                BringWindowToFront();
        }

        public ICommand CloseApplicationCommand
        {
            get { return new RelayCommand(p => CloseApplicationAction()); }
        }

        private void CloseApplicationAction()
        {
            Close();
        }

        public ICommand HideFlyoutRestartReqiredCommand
        {
            get { return new RelayCommand(p => HideFlyoutRestartReqiredAction()); }
        }

        private void HideFlyoutRestartReqiredAction()
        {
            FlyoutRestartRequiredIsOpen = false;
        }

        public ICommand RestartApplicationCommand
        {
            get { return new RelayCommand(p => RestartApplicationAction()); }
        }

        private void RestartApplicationAction()
        {
            Process.Start(Assembly.GetExecutingAssembly().Location);
            Close();
        }
        #endregion
    }
}
