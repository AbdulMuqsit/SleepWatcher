using System.Collections.Generic;
using SleepWatcher.Entites;
using SleepWatcher.ViewModel;
using SleepWatcher.ViewModel.PatientView;

namespace SleepWatcher.Design
{
    class DesignSinglePatientViewModel : ISinglePatientViewModel, IViewModelBase
    {
        public Patient Patient
        {
            get
            {
                //return new Patient
                //{
                //    FirstName = "Patient",
                //    LastName = "Kzam",
                //    Steps = new List<Step>() { new Step() { StepName = StepName.Approved } }

                return null;
            }
            set { }
        }
    }
}