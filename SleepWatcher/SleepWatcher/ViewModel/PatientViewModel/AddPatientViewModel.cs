using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;
using SleepWatcher.Model;
using AutoMapper;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    public class AddPatientViewModel : ViewModelBase, IAddPatientViewModel
    {
        private string _busyMessage;
        private bool _isBusy;
        private Patient _patient;
        private bool _startFirstStep;

        public AddPatientViewModel()
        {
            Patient = new Patient();
            AddPatinetCommand = new ActionCommand(async () =>
            {
                await Task.Run(async () =>
                {
                    Patient.Id = 0;
                    DbEntityEntry entry = Context.Entry(Patient);
                    if (entry.State == EntityState.Detached)
                    {
                        Context.Patients.Attach(Patient);
                    }
                    Busy();
                    BusyMessage = "Saving Data";
                    var step = GetStep();
                    Patient.Steps = new List<Step>();
                    Patient.Steps.Add(step);
                    Patient.CurrentStep = Mapper.Map<CurrentStep>(step);
                    Context.Patients.Add(Patient);
                    Locator.PatientViewModel.IsBusy = true;
                    await Context.SaveChangesAsync();
                    Locator.PatientViewModel.Patients.Add(Mapper.Map<PatientModel>(Patient));
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

        public Patient Patient
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