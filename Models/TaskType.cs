using System.ComponentModel.DataAnnotations;

namespace Task___Time_Tracker_App.Models
{
    public class TaskType
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
    }
}
