namespace TaskManagementSystem_FinalProject.Models
{
    public class ProjectAndUser
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}
