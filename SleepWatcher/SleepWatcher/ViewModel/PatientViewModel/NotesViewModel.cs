using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleepWatcher.Infrastructure;
using SleepWatcher.Model;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    class NotesViewModel : ViewModelBase
    {
        private RangeObservableCollection<NoteModel> _notes;
        private PatientModel _patient;
        private StepModel _step;

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
    }
}
