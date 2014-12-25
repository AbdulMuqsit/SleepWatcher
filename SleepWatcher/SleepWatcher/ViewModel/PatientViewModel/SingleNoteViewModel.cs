using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
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

        public NoteModel Note
        {
            get { return _note; }
            set
            {
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

        public SingleNoteViewModel()
        {
            SaveNoteCommand = new ActionCommand(async() =>
            {
                await Task.Run(async () =>
                {
                    DbEntityEntry entity = Context.Entry(Note);
                    if (entity.State == EntityState.Detached)
                    {
                        (await Context.Steps.FirstAsync(e => e.Id == StepId)).Notes.Add(Mapper.Map<Note>(Note));
                    }
                    else
                    {
                        entity.State = EntityState.Modified;
                    }
                    await Context.SaveChangesAsync();
                });
            });
        }
    }
}
