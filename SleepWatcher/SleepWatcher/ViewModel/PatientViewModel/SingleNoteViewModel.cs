using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleepWatcher.Model;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    class SingleNoteViewModel:ViewModelBase
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
    }
}
