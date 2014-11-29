using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;

namespace SleepWatcher.ViewModel.PatientViewModel
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

        public ActionCommand SwitchToAddPatientViewModelCommand { get; }

        public ActionCommand ClearView { get; private set; }
        public SinglePatientViewModel()
        {
           
            //initiating command which clears the data of current selected patient and shows the add new patient option
            ClearView = new ActionCommand(() =>
            {
                Patient = null;
            });

            //inititating command which swithces to add patinet view
            SwitchToAddPatientViewModelCommand = new ActionCommand(() =>
            {
                Locator.PatientViewModel.CurrentViewModel = Locator.AddPatientViewModel;
            });
        }

    }
}
