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
        private string _busyMessage;

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

        public string BusyMessage
        {
            get { return _busyMessage; }
            set
            {
                if (Equals(value, BusyMessage)) return;
                _busyMessage = value;
                OnPropertyChanged();
            }
        }

        public SinglePatientViewModel()
        {
            //Initializing command which adds a new note for selected step
            AddNewNoteCommand = new ActionCommand(async () =>
            {
                Busy();
                BusyMessage = "Saving Changes";

                await Context.SaveChangesAsync();
                Free();
            });
            //initiating command which clears the data of current selected patient and shows the add new patient option
            ClearView = new ActionCommand(() => { Patient = null; });
            // initializing command which marks a step as canceled
            MarkCanceledCommand = new ActionCommand(async () =>
            {
                await Task.Run(async () =>
                {
                    Busy();
                    BusyMessage = "Saving Changes";
                    SelectedStep.IsCancled = !SelectedStep.IsCancled;
                    Patient.CurrentStep.IsCancled = SelectedStep.IsCancled;
                    (await Context.Steps.FirstAsync(e => e.Id == SelectedStep.Id)).IsCancled =
                        SelectedStep.IsCancled;
                    (await Context.Patients.FirstAsync(e => e.Id == Patient.Id)).CurrentStep.IsCancled =
                        SelectedStep.IsCancled;
                    await Context.SaveChangesAsync();
                    Free();
                });

            });
            //Initializing command which marks a step as completed
            MarkCompleteCommand = new ActionCommand(async () =>
            {
                await Task.Run(async () =>
                {
                    Busy();
                    BusyMessage = "Saving Changes";
                    SelectedStep.IsCompleted = true;
                    (await Context.Steps.FirstAsync(e => e.Id == SelectedStep.Id)).IsCompleted = true;
                    Patient.CurrentStep.IsCompleted = true;
                    var patient = (await Context.Patients.FirstAsync(e => e.Id == Patient.Id));
                    patient.CurrentStep.IsCompleted = true;
                    if (SelectedStep.StepName != StepName.FollowUp)
                    {
                        var steps = new RangeObservableCollection<StepModel>(Patient.StepModels);
                        StepModel nextStep = GetNextStep();
                        Patient.CurrentStep = nextStep;
                        
                        patient.Steps.Add(Mapper.Map<StepModel, Step>(nextStep));
                        patient.CurrentStep = Mapper.Map<CurrentStep>(nextStep);
                        await Context.SaveChangesAsync();
                        steps.Add(
                            Mapper.Map<StepModel>(Context.Patients.First(e => e.Id == Patient.Id).Steps.Last()));
                        Patient.StepModels = steps;
                    }
                    await Context.SaveChangesAsync();

                    Free();
                });
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
                OnPropertyChanged();
                LoadSteps();
            }
        }

        private async void LoadSteps()
        {
            await Task.Run(() =>
            {
                Busy();
                BusyMessage = "Loading Steps";
                if (Patient == null) return;
                var steps = new RangeObservableCollection<StepModel>(Context.Steps.Where(e => e.PatientId == Patient.Id).Select(Mapper.Map<StepModel>));
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