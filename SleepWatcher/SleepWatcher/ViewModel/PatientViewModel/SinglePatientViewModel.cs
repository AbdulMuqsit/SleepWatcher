using System.Linq;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    public class SinglePatientViewModel : ViewModelBase, ISinglePatientViewModel
    {
        private Patient _patient;
        private Step _step;
        private Note _note;

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
        public Step SelectedStep
        {
            get
            {
                return _step;
            }
            set
            {
                if (Equals(value, _step)) return;
                _step = value;
                OnPropertyChanged();
            }
        }
        public Note SelectedNote
        {
            get
            {
                return _note;
            }
            set
            {
                if (Equals(value, _note)) return;
                _note = value;
                OnPropertyChanged();
            }
        }
        public ActionCommand SwitchToAddPatientViewModelCommand { get; }
        public ActionCommand AddNewNoteCommand { get; private set; }
        public ActionCommand MarkCompleteCommand { get; private set; }
        public ActionCommand MarkCanceledCommand { get; private set; }
        public ActionCommand MarkUnCanceledCommand { get; private set; }
        public ActionCommand ClearView { get; private set; }
        public SinglePatientViewModel()
        {
            //initiating command which clears the data of current selected patient and shows the add new patient option
            ClearView = new ActionCommand(() =>
            {
                Patient = null;
            });
            // initializing command which marks a step as canceled
            MarkCanceledCommand = new ActionCommand(() =>
              {
                  SelectedStep.IsCancled = true;
                  Context.SaveChangesAsync();
              });
            //Initializing command which marks a step as completed
            MarkCompleteCommand = new ActionCommand(() =>
            {
                SelectedStep.IsCompleted = true;
            });
            //Initializing command which marks a canceled step as uncanceled
            MarkUnCanceledCommand= new ActionCommand(() =>
            {
                SelectedStep.IsCancled = false;
            });
            //inititating command which swithces to add patinet view
            SwitchToAddPatientViewModelCommand = new ActionCommand(() =>
            {
                Locator.PatientViewModel.CurrentViewModel = Locator.AddPatientViewModel;
            });
        }

    }
}
