namespace Task___Time_Tracker_App.Models
{
    public class Tassk
    {
        public int Id { get; set; }
        public string?   Title { get; set; }
        public string?   Description { get; set; }
        public string? Status { get; set; }
        public int AssignedUserId { get; set; }
        public User AssignedUser { get; set; } 
        public DateTime? CreatedDate { get; set; }
    }
}
