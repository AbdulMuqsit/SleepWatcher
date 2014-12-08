using System.Collections.Generic;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;
using SleepWatcher.Infrastructure;
using SleepWatcher.Model;
using SleepWatcher.ViewModel;
using SleepWatcher.ViewModel.PatientViewModel;

namespace SleepWatcher.Design
{
    public class DesignPatientViewModel :ViewModelBase, IPatientViewModel
    {
        public RangeObservableCollection<PatientModel> Patients { get; set; }
        public IViewModelBase CurrentViewModel { get; set; }

        public ActionCommand SwitchToAddPatientViewCommmand { get; set; }

        public Patient Patient { get; set; }

        public DesignPatientViewModel()
        {
            CurrentViewModel= new DesignSinglePatientViewModel();
            Patient = new Patient
            {
                FirstName = "Patient",
                LastName = "Kzam",
                Steps = new List<Step> { new Step { StepName = StepName.Approved } }
            };
         
            
            Patients = new RangeObservableCollection<PatientModel>
            {
                new PatientModel
                {
                    FirstName = "Patient",
                    LastName = "Kzam",
                    Steps = new List<Step> {new Step {StepName = StepName.Approved}}
                },
                 new PatientModel
                {
                    FirstName = "Patient",
                    LastName = "Kzam",
                    Steps = new List<Step> {new Step {StepName = StepName.Delivery, IsCancled=true}}
                },
                  new PatientModel
                {
                    FirstName = "Patient",
                    LastName = "Kzam",
                    Steps = new List<Step> {new Step {StepName = StepName.Approved, IsCompleted = true}}
                },
                  new PatientModel
                {
                    FirstName = "Patient",
                    LastName = "Kzam",
                    Steps = new List<Step> {new Step {StepName = StepName.Approved}}
                },
                 new PatientModel
                {
                    FirstName = "Patient",
                    LastName = "Kzam",
                    Steps = new List<Step> {new Step {StepName = StepName.Delivery, IsCancled=true}}
                },
                  new PatientModel
                {
                    FirstName = "Patient",
                    LastName = "Kzam",
                    Steps = new List<Step> {new Step {StepName = StepName.Approved, IsCompleted = true}}
                }
            };


        }

    }
}
