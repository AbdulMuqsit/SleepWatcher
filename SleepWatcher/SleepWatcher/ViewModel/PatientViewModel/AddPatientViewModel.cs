using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;
using SleepWatcher.Infrastructure;
using SleepWatcher.Model;
using AutoMapper;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    public class AddPatientViewModel : ViewModelBase, IAddPatientViewModel
    {
        #region Fields
        private string _busyMessage;
        private bool _isBusy;
        private PatientModel _patient;
        private bool _startFirstStep;
        #endregion

        #region Properties
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

        public PatientModel Patient
        {
            get { return _patient; }
            set
            {
                if (Equals(value, Patient)) return;
                _patient = value;
                OnPropertyChanged();
            }
        }

        public ActionCommand AddPatinetCommand { get; set; }

        public ActionCommand SwitchToSinglePatientViewCommand { get; set; }
        #endregion

        #region Methods
        public AddPatientViewModel()
        {
            Patient = new PatientModel();
            AddPatinetCommand = new ActionCommand(async () =>
            {
                if (!String.IsNullOrWhiteSpace(Patient.FirstName+Patient.LastName))
                {
                    await Task.Run(async () =>
                    {
                        //convert patient model to patient
                        var patient = Mapper.Map<Patient>(Patient);

                        //attach patient to context
                        DbEntityEntry entry = Context.Entry(patient);
                        if (entry.State == EntityState.Detached)
                        {
                            Context.Patients.Attach(patient);
                        }
                        Context.Patients.Add(patient);

                        //Make state busy
                        Busy();
                        BusyMessage = "Saving Data";

                        //Assgn first step
                        var step = GetStep();
                        patient.Steps = new List<Step> { step };
                        patient.CurrentStep = Mapper.Map<CurrentStep>(step);

                        //Make parent state busy     
                        Locator.PatientViewModel.IsBusy = true;

                        //save changes to database
                        await Context.SaveChangesAsync();

                        //Update patients list
                        var patients = new RangeObservableCollection<PatientModel>(Locator.PatientViewModel.Patients)
                    {
                        Mapper.Map<PatientModel>(Mapper.Map<PatientModel>(patient))
                    };

                        Locator.PatientViewModel.Patients = new RangeObservableCollection<PatientModel>(patients.OrderByDescending(e => e.Id));
                        Patient.FirstName = "";
                        Patient.LastName = "";
                        //Make state free
                        Free();
                        Locator.PatientViewModel.IsBusy = false;
                    });
                }
            });

            //Initiate command which swithes the add pateint view back to the main view
            SwitchToSinglePatientViewCommand = new ActionCommand(async() =>
            {
                await Task.Run(() =>
                {
                    Locator.PatientViewModel.CurrentViewModel = Locator.SinglePatientViewModel;

                });
            });
        }

        public bool StartFirstStep
        {
            get { return _startFirstStep; }
            set
            {
                if (Equals(value, StartFirstStep)) return;
                _startFirstStep = value;
                OnPropertyChanged();
            }
        }

        private Step GetStep()
        {
            return new Step
            {
                DateAdded = DateTime.Now,
                StepName = StepName.PaperWorkDone,
                AlarmTime = DateTime.Now + new TimeSpan(14, 0, 0, 0)
            };
        }

        private void Free()
        {
            IsBusy = false;
        }

        private void Busy()
        {
            while (IsBusy) { }
            IsBusy = true;
        }
        #endregion
    }
}