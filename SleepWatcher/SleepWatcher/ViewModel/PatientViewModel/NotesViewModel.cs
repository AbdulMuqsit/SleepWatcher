using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;
using SleepWatcher.Infrastructure;
using SleepWatcher.Model;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    public class NotesViewModel : ViewModelBase
    {
        private RangeObservableCollection<NoteModel> _notes;
        private PatientModel _patient;
        private StepModel _step;
        public ActionCommand LoadNotes { get; set; }

        public RangeObservableCollection<NoteModel> Notes
        {
            get { return _notes; }
            set
            {
                if (Equals(value, _notes)) return;
                _notes = value;
                OnPropertyChanged();
            }
        }

        public PatientModel Patient
        {
            get { return _patient; }
            set
            {
                if (Equals(value, _patient)) return;
                _patient = value;
                OnPropertyChanged();
            }
        }

        public StepModel Step
        {
            get { return _step; }
            set
            {
                if (Equals(value, _step)) return;
                _step = value;
                OnPropertyChanged();
            }
        }

        public NotesViewModel()
        {

            //Initialize Command for loading notes
            LoadNotes = new ActionCommand(async (id) =>
            {
                if (IsContextBusy)
                {
                    return;
                }
                await Task.Run(async () =>
                {
                    IsContextBusy = true;
                    if (id is int)
                    { var s = await Context.Steps.FirstAsync(e => e.Id == (int)id);
                        Step = Mapper.Map<StepModel>(s);
                        await Context.Entry(s).Collection(e => e.Notes).LoadAsync();
                        Locator.NotesViewModel.Notes = new RangeObservableCollection<NoteModel>(s.Notes.Select(Mapper.Map<NoteModel>));
                        await Context.Entry(s).Reference(e => e.Patient).LoadAsync();
                        Patient = Mapper.Map<PatientModel>(s.Patient);
                    }
                    IsContextBusy = false;
                });
            });
            SwitchToNotesViewCommand = new ActionCommand(async (id) =>
            {
                await Task.Run(() =>
                {
                    Locator.PatientViewModel.CurrentViewModel = Locator.NotesViewModel;
                    if(id is int )LoadNotes.Execute((int)id);
                });
            });
        }
        public ActionCommand SwitchToNotesViewCommand { get; set; }

        public bool IsBusy { get; set; }
    }
}
