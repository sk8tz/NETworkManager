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

       




    }
}
