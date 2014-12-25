using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SleepWatcher.Entites;

namespace SleepWatcher.Model
{
    public class StepModel : ObjectBase
    {
        private StepName _stepName;
        private DateTime _alarmTime;
        private DateTime _dateAdded;
        private byte[] _modifiedOn;
        private bool _isCompleted;
        private bool _isCancled;
        private int _count;

        public int Id { get; set; }

        [Required]
        public StepName StepName
        {
            get { return _stepName; }
            set
            {
                if (Equals(value, StepName)) return;
                _stepName = value;
                OnPropertyChanged();
            }
        }

        public int Days
        {
            get
            {
                if (StepName == StepName.FollowUp)
                {
                    return 30;
                }
                return 15;

            }

        }

        [Required]
        public DateTime DateAdded
        {
            get { return _dateAdded; }
            set
            {
                if (Equals(value, DateAdded)) return;
                _dateAdded = value;
                OnPropertyChanged();
            }
        }

        public DateTime AlarmTime
        {
            get { return _alarmTime; }
            set
            {
                if (Equals(value, AlarmTime)) return;
                _alarmTime = value;
                OnPropertyChanged();
            }
        }

        [Timestamp]
        public byte[] ModifiedOn
        {
            get { return _modifiedOn; }
            set
            {
                if (Equals(value, ModifiedOn)) return;
                _modifiedOn = value;
            }
        }

        public bool IsCompleted
        {
            get { return _isCompleted; }
            set
            {
                if (Equals(value, IsCompleted)) return;
                _isCompleted = value;
                OnPropertyChanged();
                OnPropertyChanged("Status");

            }
        }

        public bool IsCancled
        {
            get { return _isCancled; }
            set
            {
                if (Equals(value, IsCancled)) return;
                _isCancled = value;
                OnPropertyChanged();
                OnPropertyChanged("Status");

            }
        }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        [NotMapped]
        public Status Status
        {
            get
            {
                if (IsCompleted)
                {
                    return Status.Completed; ;
                }
                if (IsCancled)
                {
                    return Status.Canceled;
                }
                if (AlarmTime < DateTime.Now)
                {
                    return Status.Overdue;
                }
                return Status.Ongoing;
            }
        }
        [NotMapped]
        public string DueDate { get { return AlarmTime.ToShortDateString(); } }
        public string DateStarted { get { return DateAdded.ToShortDateString(); } }

        public int Count
        {
            get { return _count; }
            set
            {
                if (Equals(value, _count)) return;
                _count = value;
                OnPropertyChanged();
            }
        }
    }
}