using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleepWatcher.Entites;
using SleepWatcher.ViewModel.PatientView;

namespace SleepWatcher.Design
{
    public class DesignAddPatientViewModel :IAddPatientViewModel
    {
        public Patient Patient { get; set; }
    }
}
