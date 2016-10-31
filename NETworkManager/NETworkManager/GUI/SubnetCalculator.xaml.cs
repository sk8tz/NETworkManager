using System;
using System.Collections.Generic;
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
using MahApps.Metro.Controls;
using NETworkManager.GUI.ViewModels;

namespace NETworkManager.GUI
{
    /// <summary>
    /// Interaktionslogik für SubnetCalculator.xaml
    /// </summary>
    public partial class SubnetCalculator : MetroWindow
    {
        private SubnetCalculatorViewModel viewModel = new SubnetCalculatorViewModel();

        public string IPAddress { get; set; }

        public SubnetCalculator()
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
