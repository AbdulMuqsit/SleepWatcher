using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleepWatcher.Infrastructure;
using SleepWatcher.Model;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    class NotesViewModel
    {
        public RangeObservableCollection<NoteModel> Notes { get; set; }
        public PatientModel Patient { get; set; }
        public StepModel Step { get; set; }
    }
}
