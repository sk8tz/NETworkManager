using System.Windows;
using MahApps.Metro.Controls;
using NETworkManager.GUI.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;

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

    public class ItemTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate template;

            if (item == CollectionView.NewItemPlaceholder)
                template = null;
            else
                template = base.SelectTemplate(item, container);

            return template;
        }
    }
}
