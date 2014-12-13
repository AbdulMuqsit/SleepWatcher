using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
        private string _busyMessage;
        private bool _isBusy;
        private PatientModel _patient;
        private bool _startFirstStep;

        public AddPatientViewModel()
        {
            Patient = new PatientModel();
            AddPatinetCommand = new ActionCommand(async () =>
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

                    //make state busy
                    Busy();
                    BusyMessage = "Saving Data";

                    //assgn first step
                    var step = GetStep();
                    patient.Steps = new List<Step>();
                    patient.Steps.Add(step);
                    patient.CurrentStep = Mapper.Map<CurrentStep>(step);

                    //make parent state busy     
                    Locator.PatientViewModel.IsBusy = true;

                    //save changes to database
                    await Context.SaveChangesAsync();

                    //update patients list
                    var patients = new RangeObservableCollection<PatientModel>(Locator.PatientViewModel.Patients);
                    patients.Add(Mapper.Map<PatientModel>(Mapper.Map<PatientModel>(patient)));
                    Locator.PatientViewModel.Patients = patients;

                    //make state free
                    Free();
                    Locator.PatientViewModel.IsBusy = false;
                });
            });

            //initiate command which swithes the add pateint view back to the main view
            SwitchToSinglePatientViewCommand =
                new ActionCommand(() => { Locator.PatientViewModel.CurrentViewModel = Locator.SinglePatientViewModel; });
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
                StepName = StepName.Approved,
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
    }
}