namespace TaskManagementSystem_FinalProject.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public int TasksId { get; set; }
        public Tasks Tasks { get; set; }
    }
}
