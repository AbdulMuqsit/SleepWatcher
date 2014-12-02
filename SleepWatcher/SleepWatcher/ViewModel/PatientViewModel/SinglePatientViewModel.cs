using System;
using System.Collections.ObjectModel;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    public class SinglePatientViewModel : ViewModelBase, ISinglePatientViewModel
    {
        private Note _note;
        private ObservableCollection<Note> _notes;
        private Patient _patient;
        private Step _step;
        private ObservableCollection<Step> _steps;

        public SinglePatientViewModel()
        {
            //Initializing command which adds a new note for selected step
            AddNewNoteCommand = new ActionCommand(async () =>
            {
                SelectedStep.Notes.Add(new Note { Text = "" });
                await Context.SaveChangesAsync();
            });
            //initiating command which clears the data of current selected patient and shows the add new patient option
            ClearView = new ActionCommand(() => { Patient = null; });
            // initializing command which marks a step as canceled
            MarkCanceledCommand = new ActionCommand(async () =>
            {
                SelectedStep.IsCancled = true;
                await Context.SaveChangesAsync();
            });
            //Initializing command which marks a step as completed
            MarkCompleteCommand = new ActionCommand(async () =>
            {
                SelectedStep.IsCompleted = true;
                if (SelectedStep.StepName != StepName.FollowUp)
                {
                    Patient.Steps.Add(GetNextStep());
                }
                await Context.SaveChangesAsync();
            });
            //Initializing command which marks a canceled step as uncanceled
            MarkUnCanceledCommand = new ActionCommand(async () =>
            {
                SelectedStep.IsCancled = !SelectedStep.IsCancled;
                await Context.SaveChangesAsync();
            });
            //inititating command which swithces to add patinet view
            SwitchToAddPatientViewModelCommand =
                new ActionCommand(() => { Locator.PatientViewModel.CurrentViewModel = Locator.AddPatientViewModel; });
        }

        public ObservableCollection<Step> Steps
        {
            get { return _steps; }
            set
            {
                if (Equals(_steps, value)) return;
                _steps = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Note> Notes
        {
            get { return _notes; }
            set
            {
                if (Equals(_notes, value)) return;
                _notes = value;
                OnPropertyChanged();
            }
        }

        public ActionCommand MarkUnCanceledCommand { get; private set; }

        public Patient Patient
        {
            get { return _patient; }
            set
            {
                if (Equals(value, _patient)) return;
                _patient = value;
                Steps = new ObservableCollection<Step>(_patient.Steps);
                OnPropertyChanged();
            }
        }

        public Step SelectedStep
        {
            get { return _step; }
            set
            {
                if (Equals(value, _step)) return;
                _step = value;
                Notes = new ObservableCollection<Note>(SelectedStep.Notes);

                OnPropertyChanged();
            }
        }

        public Note SelectedNote
        {
            get { return _note; }
            set
            {
                if (Equals(value, _note)) return;
                _note = value;
                OnPropertyChanged();
            }
        }

        public ActionCommand SwitchToAddPatientViewModelCommand { get; }
        public ActionCommand AddNewNoteCommand { get; }
        public ActionCommand MarkCompleteCommand { get; }
        public ActionCommand MarkCanceledCommand { get; }
        public ActionCommand ClearView { get; }

        private Step GetNextStep()
        {
            return new Step
            {
                DateAdded = DateTime.Now,
                StepName = SelectedStep.StepName + 1,
                AlarmTime =
                    SelectedStep.StepName == StepName.Delivery
                        ? DateTime.Now + new TimeSpan(30, 0, 0, 0)
                        : DateTime.Now + new TimeSpan(14, 0, 0, 0)
            };
        }
    }
}