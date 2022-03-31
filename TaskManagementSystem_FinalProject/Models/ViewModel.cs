namespace TaskManagementSystem_FinalProject.Models
{
    public class ViewModel
    {
        public Priority Priority { get; set; }
        public List<Project> projects { get; set; }

        public ViewModel(Priority prio, List<Project> p)
        {
            Priority = prio;
            projects = p;
        }
    }
}
