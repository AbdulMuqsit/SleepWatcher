using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;
using SleepWatcher.Infrastructure;
using SleepWatcher.Model;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    public class SinglePatientViewModel : ViewModelBase, ISinglePatientViewModel
    {
        private NoteModel _note;
        private RangeObservableCollection<NoteModel> _notes = new RangeObservableCollection<NoteModel>();
        private PatientModel _patient;
        private StepModel _step;
        private RangeObservableCollection<StepModel> _steps = new RangeObservableCollection<StepModel>();
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (Equals(value, IsBusy)) return;
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public SinglePatientViewModel()
        {
            //Initializing command which adds a new note for selected step
            AddNewNoteCommand = new ActionCommand(async () =>
            {
                await Context.SaveChangesAsync();
            });
            //initiating command which clears the data of current selected patient and shows the add new patient option
            ClearView = new ActionCommand(() => { Patient = null; });
            // initializing command which marks a step as canceled
            MarkCanceledCommand = new ActionCommand(async () =>
            {
                SelectedStep.IsCancled = !SelectedStep.IsCancled;
                (await Context.Steps.FirstAsync(e => e.Id == SelectedStep.Id)).IsCancled =
                    SelectedStep.IsCancled;
                await Context.SaveChangesAsync();
            });
            //Initializing command which marks a step as completed
            MarkCompleteCommand = new ActionCommand(async () =>
            {
                SelectedStep.IsCompleted = true;
                Patient.Steps.Last().IsCompleted = true;
                if (SelectedStep.StepName != StepName.FollowUp)
                {
                    StepModel nextStep = GetNextStep();
                    (await Context.Patients.FirstAsync(e => e.Id == Patient.Id)).Steps.Add(Mapper.Map<StepModel, Step>(nextStep));
                    await Context.SaveChangesAsync();
                    Patient.StepModels.Add(Mapper.Map<StepModel>(Context.Patients.First(e => e.Id == Patient.Id).Steps.Last()));

                }
                await Context.SaveChangesAsync();


            });

            //inititating command which swithces to add patinet view
            SwitchToAddPatientViewModelCommand =
                new ActionCommand(() => { Locator.PatientViewModel.CurrentViewModel = Locator.AddPatientViewModel; });
        }

        public RangeObservableCollection<StepModel> Steps
        {
            get { return _steps; }
            set
            {
               
                if (Equals(_steps, value)) return;
                _steps = value;
                OnPropertyChanged();
            }
        }

        public RangeObservableCollection<NoteModel> Notes
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

        public PatientModel Patient
        {
            get { return _patient; }
            set
            {
                if (Equals(value, _patient)) return;
                _patient = value;
                LoadSteps();
                OnPropertyChanged();
            }
        }

        private async void LoadSteps()
        {
            await Task.Run(() =>
            {
                Busy();
                if (Patient == null) return;
                var steps = new RangeObservableCollection<StepModel>( Context.Steps.Where(e => e.PatientId == Patient.Id).Select(Mapper.Map<StepModel>));
                Patient.StepModels = steps;
                Free();
            });
        }

        private void Free()
        {
            IsBusy = false;
        }

        private void Busy()
        {
            IsBusy = true;
        }

        public StepModel SelectedStep
        {
            get { return _step; }
            set
            {
                if (Equals(value, _step)) return;
                _step = value;
                OnPropertyChanged();
            }
        }

        public NoteModel SelectedNote
        {
            get { return _note; }
            set
            {
                if (Equals(value, _note)) return;
                _note = value;
                OnPropertyChanged();
            }
        }

        public ActionCommand SwitchToAddPatientViewModelCommand { get; set; }
        public ActionCommand AddNewNoteCommand { get; set; }
        public ActionCommand MarkCompleteCommand { get; set; }
        public ActionCommand MarkCanceledCommand { get; set; }
        public ActionCommand ClearView { get; set; }

        private StepModel GetNextStep()
        {
            return new StepModel
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