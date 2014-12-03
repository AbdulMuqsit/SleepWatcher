using System.Collections.ObjectModel;
using System.Data.Entity;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    class PatientViewModel : ViewModelBase, IPatientViewModel
    {
        private Patient _patient = new Patient();
        private IViewModelBase _currentViewModel;

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

        public IViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                if (Equals(value,CurrentViewModel)) return;
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public ActionCommand GetAllPatients { get; private set; }

        public PatientViewModel()
        {
            
            CurrentViewModel = new SinglePatientViewModel();
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
