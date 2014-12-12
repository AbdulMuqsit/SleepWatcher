using System.ComponentModel.DataAnnotations;
using SleepWatcher.Entites;

namespace SleepWatcher.Model
{
    public class NoteModel :ObjectBase
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public int StepId { get; set; }
        public Step Step { get; set; }
        
    }
}
