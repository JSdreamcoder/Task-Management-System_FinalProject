using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem_FinalProject.Models;

namespace TaskManagementSystem_FinalProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AppUser> AppUser { get; set; }
       
       
        public DbSet<Project> Project { get; set; }

        public DbSet<AppTask> AppTask { get; set; }
        
        public DbSet<Notification> Notification { get; set; }

        public DbSet<ProjectAndUser> ProjectAndUser { get; set; }
    }
}