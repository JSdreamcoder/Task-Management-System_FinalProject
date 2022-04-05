namespace TaskManagementSystem_FinalProject.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int? ProjectId { get; set; }

        public bool Isopen { get; set; } = false;
        
        public Project? Project { get; set; }
        public int? AppTaskId { get; set; }
        public AppTask? AppTask { get; set; }

        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

    }
}
