using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleepWatcher.Entites;
using SleepWatcher.ViewModel;
using SleepWatcher.ViewModel.PatientView;

namespace SleepWatcher.Design
{
    public class DesignPatientViewModel :ViewModelBase, IPatientViewModel
    {
        public ObservableCollection<Patient> Patients { get; set; }
        public IViewModelBase CurrentViewModel { get; set; }
        public Patient Patient { get; set; }

        public DesignPatientViewModel()
        {
            Patient = new Patient
            {
                FirstName = "Patient",
                LastName = "Kzam",
                Steps = new List<Step>() { new Step() { StepName = StepName.Approved } }
            };
            //CurrentViewModel = Locator.SinglePatientViewModel;
            
            Patients = new ObservableCollection<Patient>
            {
                new Patient
                {
                    FirstName = "Patient",
                    LastName = "Kzam",
                    Steps = new List<Step>() {new Step() {StepName = StepName.Approved}}
                },
                 new Patient
                {
                    FirstName = "Patient",
                    LastName = "Kzam",
                    Steps = new List<Step>() {new Step() {StepName = StepName.Delivery, IsCancled=true}}
                },
                  new Patient
                {
                    FirstName = "Patient",
                    LastName = "Kzam",
                    Steps = new List<Step>() {new Step() {StepName = StepName.Approved, IsCompleted = true}}
                },
                  new Patient
                {
                    FirstName = "Patient",
                    LastName = "Kzam",
                    Steps = new List<Step>() {new Step() {StepName = StepName.Approved}}
                },
                 new Patient
                {
                    FirstName = "Patient",
                    LastName = "Kzam",
                    Steps = new List<Step>() {new Step() {StepName = StepName.Delivery, IsCancled=true}}
                },
                  new Patient
                {
                    FirstName = "Patient",
                    LastName = "Kzam",
                    Steps = new List<Step>() {new Step() {StepName = StepName.Approved, IsCompleted = true}}
                }
            };


        }

    }
}
