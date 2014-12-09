using System;
using System.Data.Entity;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Infrastructure;
using SleepWatcher.Model;
using SleepWatcher.View.Notifications;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    public class OverDuePatientsViewModel : ViewModelBase
    {
        private const double TopOffset = 20;
        private const double LeftOffset = 380;
        private NotificationModel _notificationModel;
        private RangeObservableCollection<PatientModel> _overDuePatients = new RangeObservableCollection<PatientModel>();
        private readonly GrowlNotifiactions _growlNotificaitons = new GrowlNotifiactions();
        public ActionCommand GetOverDuePatientsCommand { get; set; }

        public RangeObservableCollection<PatientModel> OverDuePatients
        {
            get { return _overDuePatients; }
            set
            {
                if (Equals(value, OverDuePatients)) return;
                _overDuePatients = value;
                OnPropertyChanged();
            }
        }

        public NotificationModel NotificationModel
        {
            get { return _notificationModel; }
            set
            {
                if (Equals(value, NotificationModel)) return;
                _notificationModel = value;
                OnPropertyChanged();
                _growlNotificaitons.AddNotification(value);
            }
        }

        public ActionCommand SubscribeNotificationsCommand { get; set; }
        public OverDuePatientsViewModel()
        {
            _growlNotificaitons.Top = SystemParameters.WorkArea.Top + TopOffset;
            _growlNotificaitons.Left = SystemParameters.WorkArea.Left + SystemParameters.WorkArea.Width - LeftOffset;
            InitializeCommands();


            
            //GetOverDuePatientsCommand.Execute(null);
            SubscribeNotificationsCommand.Execute(null);
        }

        private void InitializeCommands()
        {
            GetOverDuePatientsCommand = new ActionCommand(async () =>
            {
                await Task.Run(async () =>
                {
                    OverDuePatients.Clear();
                    var patients = await Context.Patients.ToListAsync();
                    OverDuePatients.Clear();
                    OverDuePatients.AddRange(patients.Where(e => e.CurrentStep.AlarmTime < DateTime.Now + new TimeSpan(1, 0, 0, 0) && !e.CurrentStep.IsCompleted && !e.CurrentStep.IsCancled).Select(Mapper.Map<PatientModel>));
                });
            });

            SubscribeNotificationsCommand = new ActionCommand(async () =>
            {
                await Task.Run(async () =>
                {
                    OverDuePatients.Clear();
                    var patients = await Context.Patients.ToListAsync();
                    OverDuePatients.Clear();
                    OverDuePatients.AddRange(patients.Where(e => e.CurrentStep.AlarmTime < DateTime.Now + new TimeSpan(1, 0, 0, 0) && !e.CurrentStep.IsCompleted && !e.CurrentStep.IsCancled).Select(Mapper.Map<PatientModel>));

                    foreach (var item in OverDuePatients.Where(e => e.CurrentStep.AlarmTime > DateTime.Now))
                    {
                        var singlePatientTimer =
                            Observable.Timer(new DateTimeOffset(item.CurrentStep.AlarmTime))
                                .ObserveOn(DispatcherScheduler.Current);
                        singlePatientTimer.Subscribe(
                            k =>
                            {
                                NotificationModel = new NotificationModel { PatientId = item.Id, Name = item.FullName };
                            });
                    }
                    var ev = Observable.Interval(new TimeSpan(0, 0, 5),DispatcherScheduler.Current);
                    ev.Subscribe(
                        e =>
                        {
                            var overDuePatientCount =
                                OverDuePatients.Count(k => !k.CurrentStep.IsCancled && !k.CurrentStep.IsCompleted);
                            if (overDuePatientCount > 1)
                            {
                                NotificationModel =
                                    new NotificationModel
                                    {
                                        Message = overDuePatientCount + "Patients have a step overdue"
                                    };
                            }
                        });
                    await ev;
                });
            });
        }


    }
}