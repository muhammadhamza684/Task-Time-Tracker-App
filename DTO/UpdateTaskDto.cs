namespace Task___Time_Tracker_App.DTO
{
    public class UpdateTaskDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
       // public int UserRuleId { get; set; }

        public int AssignedUserId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int TaskTypeId { get; set; }

       // public int UserRollId { get; set; }
    }
}
