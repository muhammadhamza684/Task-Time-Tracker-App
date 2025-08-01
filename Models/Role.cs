using System.ComponentModel.DataAnnotations;

namespace Task___Time_Tracker_App.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string TeamName { get; set; }  

    

    }
}
