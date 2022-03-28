using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem_FinalProject.Models;

namespace TaskManagementSystem_FinalProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserManager>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BaseUser> BaseUser { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Developer> Developer { get; set; }
        public DbSet<Project> Project { get; set; }

        public DbSet<ProjectHelper> ProjectHelper { get; set; }

        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<TaskHelper> TaskHelper { get; set; }
        public DbSet<UserManager> UserManager { get; set; }
    }
}