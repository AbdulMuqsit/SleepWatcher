using System;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;
using SleepWatcher.ViewModel.PatientViewModel;

namespace SleepWatcher.Design
{

    public class DesignAddPatientViewModel :IAddPatientViewModel
    {
        public Patient Patient { get; set; }

        public ActionCommand AddPatinetCommand {get { return null; }}

        public ActionCommand SwitchToSinglePatientViewCommand
        {
            get { throw new NotImplementedException(); }
        }

        public bool StartFirstStep
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
