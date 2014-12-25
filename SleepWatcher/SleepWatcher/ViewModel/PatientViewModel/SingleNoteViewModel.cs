using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleepWatcher.Model;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    class SingleNoteViewModel
    {
        public NoteModel Note { get; set; }
        public int StepId { get; set; }
    }
}
