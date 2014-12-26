using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Expression.Interactivity.Core;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    public class AddPatientButtonViewModel : ViewModelBase
    {
        public ActionCommand SwitchToAddPatientViewModelCommand { get; set; }
        //inititating command which swithces to add patinet view
        public AddPatientButtonViewModel()
        {
            SwitchToAddPatientViewModelCommand =
           new ActionCommand(() => { Locator.PatientViewModel.CurrentViewModel = Locator.AddPatientViewModel; });

        }
    }

}
