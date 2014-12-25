using System;
using System.ComponentModel.DataAnnotations;
using SleepWatcher.Entites;

namespace SleepWatcher.Model
{
    public class NoteModel :ObjectBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
        
    }
}
