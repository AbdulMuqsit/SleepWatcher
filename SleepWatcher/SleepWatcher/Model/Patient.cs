using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SleepWatcher.Model
{
    public class Patient: ObjectBase
    {
        private string _firstName;
        private string _lastName;

        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (Equals(value, FirstName)) return;
                _firstName = value;
                OnPropertyChanged();
            }
        }

        [Required]
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (Equals(value, LastName)) return;
                _lastName = value;
                OnPropertyChanged();
            }
        }

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
