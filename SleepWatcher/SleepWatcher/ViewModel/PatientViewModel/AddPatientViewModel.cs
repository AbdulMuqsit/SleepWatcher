using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    public class AddPatientViewModel : ViewModelBase, IAddPatientViewModel
    {
        private Patient _patient;
        private bool _startFirstStep;

        public Patient Patient
        {
            get { return _patient; }
            set
            {
                if (Equals(value, Patient)) return;
                _patient = value;
                OnPropertyChanged();
            }
        }

        public ActionCommand AddPatinetCommand { get; }

        public ActionCommand SwitchToSinglePatientViewCommand { get; }

        public bool StartFirstStep
        {
            get { return _startFirstStep; }
            set
            {
                if (Equals(value, StartFirstStep)) return;
                _startFirstStep = value;
                OnPropertyChanged();
            }
        }

        public AddPatientViewModel()
        {
            AddPatinetCommand = new ActionCommand(async () =>
            {
                if (StartFirstStep)
                {
                    Patient.Steps.Add(new Step { StepName = StepName.Approved });
                    Context.Patients.Add(Patient);
                }
                else
                {
                    Context.Patients.Add(Patient);
                }

                await Context.SaveChangesAsync();
            });
             Patient = new Patient();
            //initiate command which swithes the add pateint view back to the main view
            SwitchToSinglePatientViewCommand = new ActionCommand(() =>
            {
                Locator.PatientViewModel.CurrentViewModel = Locator.SinglePatientViewModel;
            });


        }
    }
}
