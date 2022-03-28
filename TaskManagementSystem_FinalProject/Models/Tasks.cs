namespace TaskManagementSystem_FinalProject.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public Comment Comment { get; set; }    


    }
}
