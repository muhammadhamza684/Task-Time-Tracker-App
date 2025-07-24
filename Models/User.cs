using System.ComponentModel.DataAnnotations;

namespace Task___Time_Tracker_App.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
            public string? Name { get; set; }
            public string? Email { get; set; }
            public string? Password { get; set; }
    }
}
