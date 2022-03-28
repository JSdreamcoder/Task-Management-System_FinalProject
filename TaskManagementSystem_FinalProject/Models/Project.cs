namespace TaskManagementSystem_FinalProject.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Tasks> Tasks { get; set; }

        public Project()
        {
            Tasks = new HashSet<Tasks>();
        }
    }
}
