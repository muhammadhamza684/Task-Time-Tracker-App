using System.ComponentModel.DataAnnotations;

namespace Task___Time_Tracker_App.Models
{
    public class Role
    {
        [Key]
        public int roll { get; set; }
        public string DeveloperTeamName { get; set; }  

        public string ProductTeamName { get; set; } 

        public string QualityAssurance{ get; set; }

       // public IDictionary<string, object> DynamicProperties { get; set; }

    }
}
