using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.EF;
using SleepWatcher.Entites;

namespace SleepWatcher.ViewModel.PatientView
{
    public class AddPatientViewModel : ViewModelBase, IAddPatientViewModel
    {
        private Patient _patient;

        public Patient Patient
        {
            get { return _patient; }
            set
            {
                if(Equals(value,Patient))return;
                _patient = value;
                OnPropertyChanged();
            }
        }

        public ActionCommand AddPatinetCommand { get; private set; }
        public ActionCommand SwitchToSinglePatientView { get; private set; }
        public AddPatientViewModel()
        {
            AddPatinetCommand = new ActionCommand(async () =>
            {
                Context.Patients.Add(Patient);
                await Context.SaveChangesAsync();
            });
            //initiate command which swithes the add pateint view back to the main view
            SwitchToSinglePatientView = new ActionCommand(() =>
            {
                Locator.PatientViewModel.CurrentViewModel = Locator.SinglePatientViewModel;
            });
        }
    }
}
