using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using SleepWatcher.ViewModel;

namespace SleepWatcher.Model
{
    public class ObjectBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        protected ViewModelLocator Locator
        {
            get
            {
                var loator = Application.Current.Resources["Locator"];
                return loator as ViewModelLocator;
            }
        }
    }
}
