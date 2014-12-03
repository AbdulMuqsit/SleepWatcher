using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace SleepWatcher.Entites
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        [NotMapped]
        public Step CurrentStep { get { return Steps.Last(); } }

        public virtual ICollection<Step> Steps { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
    }
}
