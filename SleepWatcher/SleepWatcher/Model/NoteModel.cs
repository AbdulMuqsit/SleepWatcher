using System;
using System.ComponentModel.DataAnnotations;
using SleepWatcher.Entites;

namespace SleepWatcher.Model
{
    public class NoteModel : ObjectBase
    {
        private string _title;
        private string _text;
        private DateTime _date;
        private int _id;
        private int _stepId;

        public int Id
        {
            get { return _id; }
            set
            {
                if (Equals(value, _id)) return;
                _id = value;
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

        public string Title
        {
            get { return _title; }
            set
            {
                if (Equals(value, _title)) return;
                _title = value;
                OnPropertyChanged();
                Locator.SingleNoteViewModel.OnNoteChanged();
            }
        }

        [Required]
        public string Text
        {
            get { return _text; }
            set
            {
                if (Equals(value, _text))
                    return;
                _text = value;
                OnPropertyChanged();
                Locator.SingleNoteViewModel.OnNoteChanged();
                
            }
        }

        [Required]
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (Equals(value, _date)) return;
                _date = value;
                OnPropertyChanged();
                Locator.SingleNoteViewModel.OnNoteChanged();

            }
        }
    }
}
