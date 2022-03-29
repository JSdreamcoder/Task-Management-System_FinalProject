using Microsoft.AspNetCore.Authorization;

namespace TaskManagementSystem_FinalProject.Models
{
    [Authorize(Roles = "ProejectManager")]
    public class TaskHelper : IHelper
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
