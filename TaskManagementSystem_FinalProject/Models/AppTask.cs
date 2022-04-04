using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem_FinalProject.Models
{
    public class AppTask
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [Range(0, 100)]
        public int CompletePercentage { get; set; } = 0;
        public string? Comment { get; set; }

        public DateTime DeadLine { get; set; }
        public int? ProjectId { get; set; }
        public Project? Project { get; set; }

         
        
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public ICollection<Notification> Notifications { get; set; }

        public AppTask()
        {
            Notifications = new HashSet<Notification>(); 
        }


    }
}
