using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        #region Fields

        private const double TopOffset = 20;
        private const double LeftOffset = 350;
        private readonly GrowlNotifiactions _growlNotificaitons = new GrowlNotifiactions();
        private string _busyMessage;
        private IViewModelBase _currentViewModel;
        private bool _isBusy;
        private NotificationModel _notificationModel;
        private RangeObservableCollection<PatientModel> _overDuePatients = new RangeObservableCollection<PatientModel>();
        private Patient _patient = new Patient();
        private RangeObservableCollection<PatientModel> _patients;
        private string _searchText;
        private bool _showCanceled = true;
        private bool _showCompleted = true;
        private bool _showOngoing = true;
        private bool _showOverDue = true;
        private string _stepNameFilterString;

        #endregion

        #region Properties

        public StepName? StepNameFilter
        {
            get
            {
                if (StepNameFilterString == StepFilters[1])
                {
                    return StepName.PaperWorkDone;
                }
                if (StepNameFilterString == StepFilters[2])
                {
                    return StepName.Approved;
                }
                if (StepNameFilterString == StepFilters[3])
                {
                    return StepName.Exam;
                }
                if (StepNameFilterString == StepFilters[4])
                {
                    return StepName.Impression;
                }
                if (StepNameFilterString == StepFilters[5])
                {
                    return StepName.Delivery;
                }
                if (StepNameFilterString == StepFilters[6])
                {
                    return StepName.FollowUp;
                }
                return null;
            }
        }

        public ActionCommand FilterCompleted { get; set; }

        public ActionCommand FilterOngoing { get; set; }

        public ActionCommand FilterOverdue { get; set; }
        public ActionCommand ReverseSortCommand { get; set; }
        public ActionCommand SearchCommand { get; set; }
        public ActionCommand FilterStepCommand { get; set; }


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
                if (Equals(value, ShowOngoing)) return;
                ;
                _showOngoing = value;
                OnPropertyChanged();
            }
        }

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

        public List<string> StepFilters
        {
            get
            {
                return new List<string>
                {
                    "All Steps",
                    "Paper Work",
                    "Approval",
                    "Examinatin",
                    "Impression",
                    "Delivery",
                    "Follow Up"
                };
            }
        }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (Equals(value, SearchText)) return;
                _searchText = value;
                OnPropertyChanged();
            }
        }

        public string StepNameFilterString
        {
            get { return _stepNameFilterString; }
            set
            {
                if (Equals(value, StepNameFilterString)) return;
                _stepNameFilterString = value;
                OnPropertyChanged();
            }
        }

        public ActionCommand ShowWindowCommand { get; set; }

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

        private void Free()
        {
            IsBusy = false;
        }

        private void Busy()
        {
            while (IsBusy)
            {
            }
            IsBusy = true;
        }

        #endregion

        #region Methods

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
            InitializeFilterCommands();
            //initiate switch to add pateint view model command
            SwitchToAddPatientViewCommmand = new ActionCommand(() => { CurrentViewModel = Locator.AddPatientViewModel; });
            //initiate get all patients command
            InitializeGetCommands();
            ShowWindowCommand = new ActionCommand(() =>
            {
                Application.Current.MainWindow.Show();
                Application.Current.MainWindow.Activate();
            });
            ExitCommand = new ActionCommand(() => Application.Current.Shutdown());
            SubscribeNotificationsCommand = new ActionCommand(async () =>
            {
                await Task.Run(async () =>
                {


                    Busy();
                    BusyMessage = "Loading Patients";
                    Patients =
                        new RangeObservableCollection<PatientModel>(
                            (await
                                Context.Patients.OrderByDescending(e => e.CurrentStep.AlarmTime)
                                    .Include(e => e.CurrentStep)
                                    .ToListAsync()).Select(PatientSelector));

                    BusyMessage = "Checking for overdue Steps";
                    OverDuePatients.Clear();
                    OverDuePatients.AddRange(
                        Patients.Where(
                            e =>
                                e.CurrentStep.AlarmTime < DateTime.Now + new TimeSpan(1, 0, 0, 0) &&
                                !e.CurrentStep.IsCompleted && !e.CurrentStep.IsCancled));
                });
                foreach (PatientModel item in OverDuePatients.Where(e => e.CurrentStep.AlarmTime > DateTime.Now))
                {
                    IObservable<long> singlePatientTimer =
                        Observable.Timer(new DateTimeOffset(item.CurrentStep.AlarmTime))
                            .ObserveOn(DispatcherScheduler.Current);
                    PatientModel patientModel = item;
                    singlePatientTimer.Subscribe(
                        k =>
                        {
                            NotificationModel = new PatientNotificaitonModel
                            {
                                PatientId = patientModel.Id,
                                Name = patientModel.FullName
                            };
                        });
                }
                IObservable<long> interval = Observable.Interval(new TimeSpan(0, 10, 0), DispatcherScheduler.Current);

                interval.Subscribe(
                    e => { Notify(); });
                Free();
                Notify();
                await interval;
            });
        }

        private void InitializeGetCommands()
        {
            GetAllPatients =
                new ActionCommand(
                    async () =>
                    {
                        await Task.Run(async () =>
                        {

                            Busy();
                            BusyMessage = "Loading Patients";

                            Patients =
                                new RangeObservableCollection<PatientModel>(
                                    (await
                                        Context.Patients.OrderByDescending(e => e.CurrentStep.AlarmTime)
                                            .Include(e => e.Steps)
                                            .ToListAsync()).Select(PatientSelector));
                            Free();
                        });
                    });

            //getting patients from database to populate listbox


            GetOverDuePatientsCommand = new ActionCommand(async () =>
            {
                await Task.Run(async () =>
                {

                    Busy();
                    BusyMessage = "Applying Filter";
                    OverDuePatients.Clear();
                    List<Patient> patients = await Context.Patients.ToListAsync();
                    OverDuePatients.Clear();
                    OverDuePatients.AddRange(
                        patients.Where(
                            e =>
                                e.CurrentStep.AlarmTime < DateTime.Now + new TimeSpan(1, 0, 0, 0) &&
                                !e.CurrentStep.IsCompleted &&
                                !e.CurrentStep.IsCancled).Select(PatientSelector));
                    Free();
                });
            });
        }

        private PatientModel PatientSelector(Patient patient)
        {
            return new PatientModel
            {
                FirstName = patient.FirstName,
                CurrentStep = Mapper.Map<StepModel>(patient.CurrentStep),
                LastName = patient.LastName,
                Id = patient.Id,
                Notes = patient.Notes,
                Steps = patient.Steps
            };
        }

        private void InitializeFilterCommands()
        {
            SearchCommand = new ActionCommand(async () =>
            {
                await Task.Run(() =>
                {
                    Busy();
                    var patients = new RangeObservableCollection<PatientModel>(Context.Patients.Local.Where(e => e.FirstName.Contains(SearchText) || e.LastName.Contains(SearchText)).Select(PatientSelector));
                    Patients = patients;
                    Free();
                });
            });
            ReverseSortCommand = new ActionCommand(async () =>
            {
                await Task.Run(() =>
                {
                    Busy();
                    Patients = new RangeObservableCollection<PatientModel>(Patients.Reverse());
                    Free();
                });
            });
            FilterStepCommand = new ActionCommand(async () =>
            {
                await Task.Run(() =>
                {
                    Busy();
                    if (StepNameFilter != null)
                    {
                        var pateints =
                            new RangeObservableCollection<PatientModel>(
                                Patients.OrderByDescending(e => e.CurrentStep.AlarmTime)
                                    .Where(e => e.CurrentStep.StepName == StepNameFilter));
                    }
                    else
                    {
                        var pateints =
                            new RangeObservableCollection<PatientModel>(
                                Context.Patients.OrderByDescending(e => e.CurrentStep.AlarmTime).Select(PatientSelector));
                    }
                    Patients = Patients;
                    Free();
                });
            });
            FilterPatientsCommand = new ActionCommand(async obj =>
            {
                var patients = new RangeObservableCollection<PatientModel>();

                await Task.Run(() =>
                {

                    Busy();

                    if (obj is int)
                    {
                        BusyMessage = "Loading Patient";
                        Locator.SinglePatientViewModel.Patient = Patients.First(e => e.Id == ((int)obj));
                    }
                    else
                    {
                        BusyMessage = "Loading Patients";
                        ShowOngoing = false;
                        ShowCanceled = false;
                        ShowCompleted = false;
                        patients.AddRange(OverDuePatients.OrderBy(e => e.CurrentStep.AlarmTime));
                        Patients = patients;
                    }

                    Free();
                });
            });
            FilterCnacled = new ActionCommand(async () =>
            {
                var patients = new RangeObservableCollection<PatientModel>();

                await Task.Run(() =>
                {

                    Busy();
                    BusyMessage = "Applying Filter";
                    if (ShowCanceled)
                    {
                        patients.AddRange(Patients);
                        patients.AddRange(
                            Context.Patients.Local.Where(e => e.CurrentStep.IsCancled).Select(PatientSelector));
                        patients =
                            new RangeObservableCollection<PatientModel>(
                                patients.OrderByDescending(e => e.CurrentStep.AlarmTime));
                    }
                    else
                    {
                        for (int i = 0; i < Patients.Count; i++)
                        {
                            PatientModel patient = Patients[i];
                            if (!patient.CurrentStep.IsCancled)
                            {
                                patients.Add(patient);
                            }
                        }
                    }
                    Patients = patients;
                    Free();
                });
            });
            FilterCompleted = new ActionCommand(async () =>
            {
                var patients = new RangeObservableCollection<PatientModel>();

                await Task.Run(() =>
                {

                    Busy();
                    BusyMessage = "Applying Filter";
                    if (ShowCompleted)
                    {
                        patients.AddRange(Patients);
                        patients.AddRange(
                            Context.Patients.Local.Where(e => e.CurrentStep.IsCompleted).Select(PatientSelector));
                        patients =
                            new RangeObservableCollection<PatientModel>(
                                patients.OrderByDescending(e => e.CurrentStep.AlarmTime));
                    }
                    else
                    {
                        for (int i = 0; i < Patients.Count; i++)
                        {
                            PatientModel patient = Patients[i];
                            if (!patient.CurrentStep.IsCompleted)
                            {
                                patients.Add(patient);
                            }
                        }
                    }
                    Patients = patients;
                    Free();
                });
            });
            FilterOngoing = new ActionCommand(async () =>
            {
                var patients = new RangeObservableCollection<PatientModel>();

                await Task.Run(() =>
                {

                    Busy();
                    BusyMessage = "Applying Filter";
                    if (ShowOngoing)
                    {
                        patients.AddRange(Patients);
                        patients.AddRange(
                            Context.Patients.Local.Where(e => e.CurrentStep.Status == Status.Ongoing)
                                .Select(PatientSelector));
                        patients =
                            new RangeObservableCollection<PatientModel>(
                                patients.OrderByDescending(e => e.CurrentStep.AlarmTime));
                    }
                    else
                    {
                        for (int i = 0; i < Patients.Count; i++)
                        {
                            PatientModel patient = Patients[i];
                            if (patient.CurrentStep.Status != Status.Ongoing)
                            {
                                patients.Add(patient);
                            }
                        }
                    }
                    Patients = patients;
                    Free();
                });
            });

            FilterOverdue = new ActionCommand(async () =>
            {
                var patients = new RangeObservableCollection<PatientModel>();
                await Task.Run(() =>
                {

                    Busy();
                    BusyMessage = "Applying Filter";
                    if (ShowOverDue)
                    {
                        patients.AddRange(Patients);
                        patients.AddRange(
                            Context.Patients.Local.Where(e => e.CurrentStep.Status == Status.Overdue)
                                .Select(PatientSelector));
                        patients =
                            new RangeObservableCollection<PatientModel>(
                                patients.OrderByDescending(e => e.CurrentStep.AlarmTime));
                    }
                    else
                    {
                        for (int i = 0; i < Patients.Count; i++)
                        {
                            PatientModel patient = Patients[i];
                            if (patient.CurrentStep.Status != Status.Overdue)
                            {
                                patients.Add(patient);
                            }
                        }
                    }
                    Patients = patients;
                    Free();
                });
            });
        }


        private void Notify()
        {
            int overDuePatientCount =
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

        #endregion
    }
}