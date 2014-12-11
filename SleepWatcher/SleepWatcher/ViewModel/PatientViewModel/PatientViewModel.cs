using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;
using SleepWatcher.Infrastructure;
using SleepWatcher.Model;
using SleepWatcher.View.Notifications;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    internal class PatientViewModel : ViewModelBase, IPatientViewModel
    {
        private IViewModelBase _currentViewModel;
        private Patient _patient = new Patient();
        private RangeObservableCollection<PatientModel> _patients;
        private const double TopOffset = 20;
        private const double LeftOffset = 350;
        private NotificationModel _notificationModel;
        private RangeObservableCollection<PatientModel> _overDuePatients = new RangeObservableCollection<PatientModel>();
        private readonly GrowlNotifiactions _growlNotificaitons = new GrowlNotifiactions();
        private bool _showOverDue = true;
        private bool _showCanceled = true;
        private bool _showCompleted = true;
        private bool _showOngoing = true;
        private bool _isBusy = false;
        private string _busyMessage
            ;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (Equals(value, IsBusy)) return;
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public string BusyMessage
        {
            get { return _busyMessage; }
            set
            {
                if (Equals(value, BusyMessage)) return;
                _busyMessage = value;
                OnPropertyChanged();
            }
        }

        public ActionCommand GetOverDuePatientsCommand { get; set; }
        public Patient Patient
        {
            get { return _patient; }
            set
            {
                if (Equals(value, _patient)) return;
                _patient = value;
                OnPropertyChanged();
            }
        }

        public ActionCommand GetAllPatients { get; set; }

        public ActionCommand ExitCommand { get; set; }

        public RangeObservableCollection<PatientModel> Patients
        {
            get { return _patients; }
            set
            {
                if (Equals(value, Patients)) return;
                _patients = value;
                OnPropertyChanged();
            }
        }

        public ActionCommand SwitchToAddPatientViewCommmand { get; set; }

        public IViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                if (Equals(value, CurrentViewModel)) return;
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }
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
        public ActionCommand FilterPatientsCommand { get; set; }
        public ActionCommand SubscribeNotificationsCommand { get; set; }
        public PatientViewModel()
        {
            CurrentViewModel = new SinglePatientViewModel();
            _growlNotificaitons.Top = SystemParameters.WorkArea.Top + TopOffset;
            _growlNotificaitons.Left = SystemParameters.WorkArea.Left + SystemParameters.WorkArea.Width - LeftOffset;
            InitializeCommands();

            SubscribeNotificationsCommand.Execute(null);
            //GetAllPatients.Execute(null);
        }

        private void InitializeCommands()
        {
            FilterPatientsCommand = new ActionCommand((obj) =>
             {
                 if (obj is int)
                 {
                     Locator.SinglePatientViewModel.Patient = Patients.First(e => e.Id == ((int)obj));

                 }
                 else
                 {
                     Patients = OverDuePatients;
                 }

             });
            FilterCnacled = new ActionCommand(() =>
            {
                Task.Run(() =>
                {
                    if (ShowCanceled)
                    {
                        Patients.AddRange(Context.Patients.Local.Where(e => e.CurrentStep.IsCancled).Select(Mapper.Map<PatientModel>));
                    }
                });
            });

            //initiate switch to add pateint view model command
            SwitchToAddPatientViewCommmand = new ActionCommand(() => { CurrentViewModel = Locator.AddPatientViewModel; });
            //initiate get all patients command
            GetAllPatients =
                new ActionCommand(
                     async () =>
                     {
                         await Task.Run(async () =>
                         {
                             Patients =
                             new RangeObservableCollection<PatientModel>(
                                 (await Context.Patients.Include(e => e.Steps).ToListAsync()).Select(Mapper.Map<Patient, PatientModel>));
                         });
                     });

            //getting patients from database to populate listbox


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

            ExitCommand= new ActionCommand(() =>
            {
                Application.Current.Shutdown();
            });
            SubscribeNotificationsCommand = new ActionCommand(async () =>
            {

                await Task.Run(async () =>
                {
                    while (IsBusy) {}

                    IsBusy = true;
                    BusyMessage = "Loading Patients";
                    Patients =
                    new RangeObservableCollection<PatientModel>(
                        (await Context.Patients.Include(e => e.Steps).ToListAsync()).Select(Mapper.Map<Patient, PatientModel>));

                    BusyMessage = "Checking for overdue Steps";


                    OverDuePatients.Clear();
                    OverDuePatients.AddRange(
                        Patients.Where(
                            e =>
                                e.CurrentStep.AlarmTime < DateTime.Now + new TimeSpan(1, 0, 0, 0) &&
                                !e.CurrentStep.IsCompleted && !e.CurrentStep.IsCancled).Select(Mapper.Map<PatientModel>));
                });

                foreach (var item in OverDuePatients.Where(e => e.CurrentStep.AlarmTime > DateTime.Now))
                {
                    var singlePatientTimer =
                        Observable.Timer(new DateTimeOffset(item.CurrentStep.AlarmTime))
                            .ObserveOn(DispatcherScheduler.Current);
                    var patientModel = item;
                    singlePatientTimer.Subscribe(
                        k =>
                        {
                            NotificationModel = new PatientNotificaitonModel { PatientId = patientModel.Id, Name = patientModel.FullName };
                        });
                }
                var interval = Observable.Interval(new TimeSpan(0, 10, 0), DispatcherScheduler.Current);

                interval.Subscribe(
                     e =>
                     {
                         Notify();
                     });
                IsBusy = false;
                Notify();
                await interval;

            });

        }

        private void Notify()
        {
            var overDuePatientCount =
                OverDuePatients.Count();
            if (overDuePatientCount > 1)
            {
                NotificationModel =
                    new NotificationModel
                    {
                        Message = overDuePatientCount + " patients have a step overdue"
                    };
            }
            else if (overDuePatientCount == 1)
            {
                NotificationModel = new PatientNotificaitonModel
                {
                    PatientId = OverDuePatients[0].Id,
                    Name = OverDuePatients[0].FullName
                };
            }
        }

        public ActionCommand FilterCnacled { get; set; }

        public bool ShowOverDue
        {
            get { return _showOverDue; }
            set
            {
                if (Equals(value, ShowOverDue)) return;
                _showOverDue = value;
                OnPropertyChanged();
            }
        }

        public bool ShowCanceled
        {
            get { return _showCanceled; }
            set
            {
                if (Equals(value, ShowCanceled)) return;
                _showCanceled = value;
                FilterCnacled.Execute(null);
                OnPropertyChanged();
            }
        }

        public bool ShowCompleted
        {
            get { return _showCompleted; }
            set
            {
                if (Equals(value, ShowCompleted)) return;
                _showCompleted = value;
                OnPropertyChanged();
            }
        }

        public bool ShowOngoing
        {
            get { return _showOngoing; }
            set
            {
                if (Equals(value, ShowOngoing)) return; ;
                _showOngoing = value;
                OnPropertyChanged();
            }
        }
    }
}