using System.ComponentModel;

namespace Robotics.GUI.ViewModel {
    internal class BaseViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void OnClosing(object sender, CancelEventArgs e) {
        }
    }
}
