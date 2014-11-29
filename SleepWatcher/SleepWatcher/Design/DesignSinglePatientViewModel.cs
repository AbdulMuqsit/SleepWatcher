using System.Collections.Generic;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;
using SleepWatcher.ViewModel;

using SleepWatcher.ViewModel.PatientViewModel;

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

        public ActionCommand SwitchToAddPatientViewModelCommand
        {
            get
            {
                return null;

            }
        }
    }
}