namespace TaskManagementSystem_FinalProject.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Budget { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadLine { get; set; }

        public ICollection<ProjectAndUser> ProejectAndUsers { get; set; }
        public ICollection<AppTask> AppTasks { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public Project()
        {
            ProejectAndUsers = new HashSet<ProjectAndUser>();
            AppTasks = new HashSet<AppTask>();
            Notifications = new HashSet<Notification>();
        }
    }

   
}
