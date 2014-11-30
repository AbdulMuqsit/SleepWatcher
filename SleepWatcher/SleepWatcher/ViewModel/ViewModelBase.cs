using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SleepWatcher.EF;

namespace SleepWatcher.ViewModel
{
    public interface IViewModelBase
    {
    }

    public class ViewModelBase : INotifyPropertyChanged, IViewModelBase
    {
        
        public static SleepWatcherDbContext Context { get; set; }
        static  ViewModelBase()
        {
            
            Context= new SleepWatcherDbContext();

        }

        public ViewModelBase()
        {
            

        }

        protected ViewModelLocator Locator
        {

            get
            {
                var _loator = Application.Current.Resources["Locator"];
                return _loator as ViewModelLocator;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
