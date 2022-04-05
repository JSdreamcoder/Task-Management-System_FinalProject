using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementSystem_FinalProject.Models
{
    public class AppUser : IdentityUser
    {
        public int? DailySalary { get; set; }
        public ICollection<Project> Projects { get; set; }
        
        public ICollection<AppTask> AppTasks { get; set; }

        public ICollection<Notification> Notifications { get; set; }
        public AppUser()
        {
           Projects = new HashSet<Project>();
           AppTasks = new HashSet<AppTask>();
           Notifications = new HashSet<Notification>();
        }

        
        
    }
}
