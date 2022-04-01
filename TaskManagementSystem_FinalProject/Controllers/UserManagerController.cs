#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem_FinalProject.Data;
using TaskManagementSystem_FinalProject.Models;

namespace TaskManagementSystem_FinalProject.Controllers
{
    public class UserManagerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserManagerController(ApplicationDbContext context,
                                     UserManager<AppUser> um,
                                     RoleManager<IdentityRole> rm)
        {
            _context = context;
            userManager = um;
            roleManager = rm;
        }


        // GET: UserManager
        public async Task<IActionResult> Index()
        {
            var userAndRoles = new Dictionary<AppUser, IList<string>>();
            var users = _context.Users;

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                userAndRoles.Add(user, roles);
            }
            return View(userAndRoles);
        }

        public async Task<IActionResult> AssignRoleToUser(string userid)
        {
            var user = _context.AppUser.First(u => u.Id == userid);
            var userRoles = await userManager.GetRolesAsync(user);
            var allRoles = _context.Roles.ToList();
            // prevent selectList of roles have the roles that user aleady have
            var otherRoles = allRoles.Where(r => !userRoles.Contains(r.ToString())).ToList();

            ViewBag.User = _context.AppUser.First(u => u.Id == userid);
            ViewBag.UserRoles = await userManager.GetRolesAsync(user);
            var selectlistOfRoles = new SelectList(otherRoles, "Name", "Name");

            return View(selectlistOfRoles);
        }
        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser(string role, string userid)
        {
            var user = _context.Users.First(u => u.Id == userid);
            await userManager.AddToRoleAsync(user, role);
            await userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CreateRole()
        {
            var allroles = _context.Roles.ToList();
            return View(allroles);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(string rolename)
        {
            try
            {
                await roleManager.RoleExistsAsync(rolename);
                await roleManager.CreateAsync(new IdentityRole(rolename));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("CreateRole");
        }

        public async Task<IActionResult> DeleteRole()
        {
            ViewBag.AllRoles = _context.Roles.ToList();
            var allroles = _context.Roles;
            var roleList = new SelectList(allroles, "Name", "Name");
            return View(roleList);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string role)
        {
            var roleToDelete = _context.Roles.First(r => r.Name == role);
            _context.Roles.Remove(roleToDelete);
            _context.SaveChanges();
            return RedirectToAction("DeleteRole");
        }
        public async Task<IActionResult> DeleteUserRole(string userid)
        {
            var user = _context.AppUser.First(u => u.Id == userid);
            var userRoles = await userManager.GetRolesAsync(user);
            var allRoles = _context.Roles.ToList();


            ViewBag.User = _context.AppUser.First(u => u.Id == userid);
            ViewBag.UserRoles = await userManager.GetRolesAsync(user);
            var selectlistOfRoles = new SelectList(userRoles);
            return View(selectlistOfRoles);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUserRole(string role, string userid)
        {
            //var user = _context.Users.First(u => u.Id == userid);
            var roleId = _context.Roles.First(r => r.Name == role).Id;
            var userRole = _context.UserRoles.First(ur => ur.UserId == userid && ur.RoleId == roleId);
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteUser()
        {
            ViewBag.AllUsers = _context.AppUser;
            var Alluser = _context.AppUser;
            var userList = new SelectList(Alluser, "Email", "Email");
            return View(userList);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string username)
        {
            var user = _context.AppUser.First(u => u.Email == username);
            var tasksOfUser = _context.AppTask.Where(t=>t.AppUserId==user.Id).ToList();
            foreach (var task in tasksOfUser)
            {
                task.AppUserId = null;
            }
            _context.AppUser.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("DeleteUser");
        }
    }
}
