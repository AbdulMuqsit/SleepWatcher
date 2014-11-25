using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;

namespace SleepWatcher.ViewModel.PatientView
{
    public class SinglePatientViewModel : ViewModelBase, ISinglePatientViewModel
    {
        private Patient _patient;

        public Patient Patient
        {
            get { return _patient; }
            set
            {
                if (Equals(value, _patient)) return;
                _patient = value;
                OnPropertyChanged();

            }
        }

        public ActionCommand ClearView { get; private set; }
        public SinglePatientViewModel()
        {
           
            //initiating command which clears the data of current selected patient and shows the add new patient option
            ClearView = new ActionCommand(() =>
            {
                Patient = null;
            });
        }

    }
}
