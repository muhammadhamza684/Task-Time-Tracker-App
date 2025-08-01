
using System.ComponentModel.DataAnnotations.Schema;
using Task___Time_Tracker_App.Models;
namespace Task___Time_Tracker_App.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public int UserRuleId { get; set; }
        public int AssignedUserId { get; set; }

        public DateTime? CreatedDate { get; set; }
        public User? AssignedUser { get; set; }

        public int TaskTypeId { get; set; }

        public TaskType? TaskType { get; set; }

        public int UserRollId { get; set; }

        [ForeignKey("UserRollId")]
        public Role UserRule { get; set; }
    }


}





