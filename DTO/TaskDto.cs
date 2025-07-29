using Task___Time_Tracker_App.Models;

namespace Task___Time_Tracker_App.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
      //  public int AssignedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? AssignedUser { get; set; }

    }
}
