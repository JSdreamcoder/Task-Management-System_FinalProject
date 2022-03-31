namespace TaskManagementSystem_FinalProject.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Budget { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadLine { get; set; }

        public ICollection<AppTask> AppTasks { get; set; }

        public Project()
        {
            AppTasks = new HashSet<AppTask>();
        }
    }

   
}
