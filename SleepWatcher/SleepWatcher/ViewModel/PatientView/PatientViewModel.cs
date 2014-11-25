using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Annotations;
using SleepWatcher.EF;
using SleepWatcher.Entites;

namespace SleepWatcher.ViewModel.PatientView
{
    class PatientViewModel : ViewModelBase, IPatientViewModel
    {
        private Patient _patient = new Patient();

        public ObservableCollection<Patient> Patients { get; set; }
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
        public ActionCommand SwitchToAddPatientViewCommmand { get; private set; }
        public IViewModelBase CurrentViewModel { get; set; }
        public ActionCommand GetAllPatients { get; }

        public PatientViewModel()
        {
            //initiate switch to add pateint view model command
            SwitchToAddPatientViewCommmand = new ActionCommand(() =>
            {
                CurrentViewModel = Locator.AddPatientViewModel;
            });
            //initiate get all patients command
            GetAllPatients = new ActionCommand(async () =>
             {

                 await Context.Patients.ToListAsync();

             });

            //getting patients from database to populate listbox
            GetAllPatients.Execute(null);
            Patients = Context.Patients.Local;
        }

     
    }
}
