using System.Collections.ObjectModel;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;
using SleepWatcher.ViewModel;

namespace SleepWatcher.Model
{
    public class NotificationModel : ViewModelBase
    {
        private int _id;
        private string message;
        private string title;
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
        public int Id
        {
            get { return _id; }

            set
            {
                if (_id == value) return;
                _id = value;
                OnPropertyChanged();
            }
        }
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
    public class Notifications : ObservableCollection<NotificationModel> { }
}