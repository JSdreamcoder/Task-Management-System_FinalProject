using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace TaskManagementSystem_FinalProject.Models
{
    
    [Authorize(Roles ="ProjectManager")]
    public class AppUserManager
    {
         
        public RoleManager<IdentityRole> roleManager;
        public UserManager<AppUser> userManager;
        public AppUserManager(RoleManager<IdentityRole> rm,
                              UserManager<AppUser> um)
        {
            roleManager = rm;
            userManager = um;
        }
        public AppUserManager() { }
        public void CreateRole()
        {
            
            
        }
    }
}
