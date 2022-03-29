using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementSystem_FinalProject.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Project> Projects { get; set; }
        
        public ICollection<AppTask> AppTasks { get; set; }


        public AppUser()
        {
           Projects = new HashSet<Project>();
           AppTasks = new HashSet<AppTask>();
        }

        
        
    }
}
