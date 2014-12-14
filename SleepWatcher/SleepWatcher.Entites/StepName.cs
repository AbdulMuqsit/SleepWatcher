using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SleepWatcher.Entites
{
    public enum StepName
    {

        [Display(Name = "Paper Work")]
        PaperWorkDone,

        [Display(Name = "Approval")]
        Approved,

        [Display(Name = "Examination")]
        Exam,
        Impression,
        Delivery,

        [Display(Name = "Follow Up")]
        FollowUp
    }
}
