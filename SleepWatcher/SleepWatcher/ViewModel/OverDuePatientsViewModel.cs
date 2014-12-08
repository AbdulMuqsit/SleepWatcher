using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows;
using SleepWatcher.View.Notifications;

namespace SleepWatcher.ViewModel
{
    public class OverDuePatientsViewModel : ViewModelBase
    {
        private const double TopOffset = 20;
        private const double LeftOffset = 380;

        private readonly GrowlNotifiactions _growlNotificaitons = new GrowlNotifiactions();
        private Notification _notification;

        public Notification Notification
        {
            get { return _notification; }
            set
            {
                if (Equals(value, Notification)) return;
                _notification = value;
                OnPropertyChanged();
                _growlNotificaitons.AddNotification(value);
            }
        }

        public OverDuePatientsViewModel()
        {
            _growlNotificaitons.Top = SystemParameters.WorkArea.Top + TopOffset;
            _growlNotificaitons.Left = SystemParameters.WorkArea.Left + SystemParameters.WorkArea.Width - LeftOffset;
            SetEvent();
        }
        public async void SetEvent()
        {
            var something = Context.Patients.Where(e => e.Steps.Last().AlarmTime < DateTime.Now + new TimeSpan(1, 0, 0, 0));
            var ev = Observable.Interval(new TimeSpan(0, 0, 5)).ObserveOn(DispatcherScheduler.Current);
            ev.Subscribe(e => Notification = new Notification { Message = "fuck you", Title = "again" });
            await ev;
        }
    }
}
