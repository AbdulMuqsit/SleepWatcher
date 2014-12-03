using System.ComponentModel.DataAnnotations;

namespace SleepWatcher.Model
{
    public class Note :ObjectBase
    {
        public int Id { get; set; }
        [Required]
       
        public string Text { get; set; }

        public int StepId { get; set; }
        public Step Step { get; set; }
        
    }
}
