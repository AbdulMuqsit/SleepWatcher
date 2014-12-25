using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepWatcher.Entites
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string Text { get; set; }
        public int StepId { get; set; }
        public Step Step { get; set; }
        
    }
}
