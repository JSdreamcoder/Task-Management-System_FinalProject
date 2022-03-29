using Microsoft.AspNetCore.Authorization;

namespace TaskManagementSystem_FinalProject.Models
{
    [Authorize(Roles = "ProejectManager")]
    public class ProjectHelper : IHelper
    {
        public void Add()
        {

        }
        public void Delete()
        {

        }
        public void Update()
        {

        }
    }
}
