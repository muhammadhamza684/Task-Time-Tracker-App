using System.ComponentModel.DataAnnotations.Schema;

namespace Task___Time_Tracker_App.Models
{
    public class TimeLog
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
       // [ForeignKey("TaskId")]
        public Tassk? Task { get; set; }
        public int UserId { get; set; }

      //  [ForeignKey("UserId")]
        public User? User { get; set; }
        public decimal HoursSpent { get; set; }
        public DateTime? LogDate { get; set; }
    }
}
