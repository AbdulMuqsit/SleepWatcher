using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;
using Model = SleepWatcher.Model;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    public class SinglePatientViewModel : ViewModelBase, ISinglePatientViewModel
    {
        private Model.Note _note;
        private ObservableCollection<Model.Note> _notes;
        private Patient _patient;
        private Model.Step _step;
        private ObservableCollection<Model.Step> _steps;

        public SinglePatientViewModel()
        {
            //Initializing command which adds a new note for selected step
            AddNewNoteCommand = new ActionCommand(async () =>
            {
                SelectedStep.Notes.Add(new Model.Note { Text = "" });
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

        public ObservableCollection<Model.Step> Steps
        {
            get { return _steps; }
            set
            {
                if (Equals(_steps, value)) return;
                _steps = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Model.Note> Notes
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
                Steps = new ObservableCollection<Model.Step>(_patient.Steps.Select(e => { return new Model.Step() { AlarmTime = e.AlarmTime, DateAdded = e.DateAdded, IsCompleted = e.IsCompleted, IsCancled = e.IsCancled, ModifiedOn = e.ModifiedOn, PatientId = e.PatientId, }; }).ToList());
                OnPropertyChanged();
            }
        }

        public Model.Step SelectedStep
        {
            get { return _step; }
            set
            {
                if (Equals(value, _step)) return;
                _step = value;
                Notes = new ObservableCollection<Model.Note>(SelectedStep.Notes.Select(e=> { return new Model.Note(); }));

                OnPropertyChanged();
            }
        }

        public Model.Note SelectedNote
        {
            get { return _note; }
            set
            {
                if (Equals(value, _note)) return;
                _note = value;
                OnPropertyChanged();
            }
        }

        public ActionCommand SwitchToAddPatientViewModelCommand { get; private set; }
        public ActionCommand AddNewNoteCommand { get; private set; }
        public ActionCommand MarkCompleteCommand { get; private set; }
        public ActionCommand MarkCanceledCommand { get; private set; }
        public ActionCommand ClearView { get; private set; }

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