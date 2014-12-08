using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Model;

namespace SleepWatcher.ViewModel
{
    public class Notification :ViewModelBase
    {
        public ActionCommand ShowPatientCommand { get; set; }
        private string message;
        public string Message
        {
            get { return message; }

            set
            {
                if (message == value) return;
                message = value;
                OnPropertyChanged();
            }
        }

        private int id;
        public int Id
        {
            get { return id; }

            set
            {
                if (id == value) return;
                id = value;
                OnPropertyChanged("Id");
            }
        }

        

        private string title;
        public string Title
        {
            get { return title; }

            set
            {
                if (title == value) return;
                title = value;
                OnPropertyChanged();
            }
        }

      
    }

    public class Notifications : ObservableCollection<Notification> { }
}