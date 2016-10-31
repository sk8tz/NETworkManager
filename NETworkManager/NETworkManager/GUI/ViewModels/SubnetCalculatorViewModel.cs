using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETworkManager.GUI.ViewModels
{
    class SubnetCalculatorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private string _ipAddress;
        public string IPAddress
        {
            get { return _ipAddress; }
            set
            {
                if(value != _ipAddress)
                {
                    _ipAddress = value;
                    OnPropertyChanged("IPAddress");
                }
            }
        }

        private string _subnetmaskCDIR;
            public string SubnetmaskCDIR
        {
            get { return _subnetmaskCDIR; }
            set
            {
                if(value != _subnetmaskCDIR)
                {
                    _subnetmaskCDIR = value;
                    OnPropertyChanged("SubnetmaskCDIR");
                }
            }
        }
    }
}
