using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace SleepWatcher.Entites
{
    public class Step
    {

        public int Id { get; set; }
        [Required]
        public StepName StepName { get; set; }

        public int Days
        {
            get
            {
                if (this.StepName == StepName.FollowUp)
                {
                    return 30;
                }
                return 15;

            }

        }
        [Required]
        public DateTime DateAdded { get; set; }
        public DateTime AlarmTime { get; set; }
        [Timestamp]
        public byte[] ModifiedOn { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsCancled { get; set; }
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
        public string DueDate {get{return AlarmTime.ToShortDateString();}} 
        public string DateStarted{get{return DateAdded.ToShortDateString();}} 
        public virtual ICollection<Note> Notes { get; set; }

    }
}