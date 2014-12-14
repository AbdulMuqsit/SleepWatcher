using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace SleepWatcher.Entites
{
    public class CurrentStep
    {
        public int Id { get; set; }
        [Required]
        public StepName StepName { get; set; }

        [Required]
        public DateTime AlarmTime { get; set; }
     
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

    }
}
