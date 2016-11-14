using System.ComponentModel;

namespace NETworkManager.GUI.ViewModels
{
    class SubnetCalculatorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private string _ipAddress;
        public string IPAddress
        {
            get { return _ipAddress; }
            set
            {
                if (value == _ipAddress)
                    return;

                _ipAddress = value;
                OnPropertyChanged("IPAddress");
            }
        }

        private string _subnetmaskCDIR;
        public string SubnetmaskCDIR
        {
            get { return _subnetmaskCDIR; }
            set
            {
                if (value == _subnetmaskCDIR)
                    return;

                _subnetmaskCDIR = value;
                OnPropertyChanged("SubnetmaskCDIR");
            }
        }
    }
}
