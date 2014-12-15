using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using SleepWatcher.EF;

namespace SleepWatcher.ViewModel
{
    public interface IViewModelBase
    {
    }

    public class ViewModelBase : INotifyPropertyChanged, IViewModelBase
    {
        static ViewModelBase()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory",
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            Context = new SleepWatcherDbContext();
        }

        public static SleepWatcherDbContext Context { get; set; }

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
            try
            {
                var handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex)
            {

                try
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);

                }
            }
        }
    }
}