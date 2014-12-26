using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;
using SleepWatcher.Model;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    public class SingleNoteViewModel : ViewModelBase
    {
        private NoteModel _note;
        private int _stepId;

        public SingleNoteViewModel()
        {

            Notechanged += (() => SaveNoteCommand.Execute(null));
            SwitchToSingleNoteViewCommand = new ActionCommand(async (id) =>
            {
                await Task.Run(() => { Locator.PatientViewModel.CurrentViewModel = Locator.SingleNoteViewModel; });
                if (id is int)
                {
                    _stepId = (int)id;
                    if ((int)id == 0)
                        _note = new NoteModel() { StepId = _stepId, Date = DateTime.Now };
                }
            });
            SaveNoteCommand = new ActionCommand(async id =>
            {
                await Task.Run(async () =>
                {
                    if (IsContextBusy) return;
                    IsContextBusy = true;
                    if (Note != null && Note.Id == 0 && !String.IsNullOrWhiteSpace(Note.Text))
                    {
                        var step = (await Context.Steps.Include(e => e.Notes).FirstAsync(e => e.Id == StepId));
                        if (step.Notes == null) step.Notes = new Collection<Note>();
                        var newNote = Mapper.Map<Note>(Note);
                        step.Notes.Add(newNote);
                        await Context.SaveChangesAsync();
                        Note.Id = newNote.Id;

                    }
                    else if (Note != null && Note.Id != 0)
                    {
                        Note entry = await Context.Notes.FirstAsync(note => note.Id == Note.Id);
                        entry.Date = Note.Date;
                        entry.Text = Note.Text;
                        entry.Title = Note.Title;
                        Context.Entry(entry).State = EntityState.Modified;
                        await Context.SaveChangesAsync();
                    }

                    IsContextBusy = false;
                });
            });
        }

        public ActionCommand SwitchToSingleNoteViewCommand { get; set; }

        public NoteModel Note
        {
            get { return _note; }
            set
            {
                if (value != null) SwitchToSingleNoteViewCommand.Execute(value.StepId);
                if (Equals(value, _note)) return;
                _note = value;
                OnPropertyChanged();
            }
        }


        public int StepId
        {
            get { return _stepId; }
            set
            {
                if (Equals(value, _stepId)) return;
                _stepId = value;
                OnPropertyChanged();
            }
        }

        public ActionCommand SaveNoteCommand { get; set; }

        public bool IsBusy { get; set; }
        public event Action Notechanged;

        public void OnNoteChanged()
        {
            Action hacdler = Notechanged;
            if (hacdler != null) hacdler();
        }
    }
}