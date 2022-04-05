#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem_FinalProject.Data;
using TaskManagementSystem_FinalProject.Models;

namespace TaskManagementSystem_FinalProject.Controllers
{
    [Authorize(Roles ="ProjectManager")]
    public class ProjectHelperController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectHelperController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProjectHelper
        public async Task<IActionResult> Index(bool ishideComplete,Priority priority)
        {
            
            var projects = await _context.Project.Include(p=>p.AppTasks).ThenInclude(a=>a.AppUser)
                                                 .Include(p=>p.Notifications)
                                                 .Include(p=>p.ProejectAndUsers).ThenInclude(pu=>pu.AppUser)
                                                 .ToListAsync();



            //When ishideComplete is true, hide tasks with completepercentage is 100
            foreach (var project in projects)
            {
                if (ishideComplete == true)
                {
                    ViewBag.IsHideComplete = ishideComplete;
                    project.AppTasks = project.AppTasks.OrderByDescending(a=>a.CompletePercentage)
                                                       .Where(a=>a.CompletePercentage < 100)
                                                       .ToList();    
                }else
                {
                    ViewBag.IsHideComplete = ishideComplete;
                    project.AppTasks = project.AppTasks.OrderByDescending(a => a.CompletePercentage)
                                                       .ToList();
                }
            }

            //implement priority
            if (priority == Priority.Newest)
            {
                ViewBag.Priority = priority;
                projects = projects.OrderByDescending(p => p.StartDate).ToList();
            }
            else if (priority == Priority.Budget)
            {
                ViewBag.Priority = priority;
                projects = projects.OrderByDescending(p => p.Budget).ToList();
            }
            else if (priority == Priority.DeadLine)
            {
                ViewBag.Priority = priority;
                projects = projects.OrderBy(p => p.DeadLine).ToList();
            }
            else if (priority == Priority.ExceededCost)
            {
                ViewBag.Priority = priority;
                var temp = new List<Project>();
                foreach (var p in projects)
                {
                    if (p.Budget > (p.ProejectAndUsers.Select(pu => pu.AppUser).Select(A => A.DailySalary).Sum() * ((DateTime.Now.Date - p.StartDate).Days)))
                    {
                        temp.Add(p);
                    }

                }
                foreach (var p in temp)
                {
                    projects.Remove(p);
                }
            }
            else if (priority == Priority.Finish)
            {
                ViewBag.Priority = priority;
                var temp = new List<Project>();
                foreach (var p in projects)
                {
                    if (!p.AppTasks.All(a=>a.CompletePercentage==100))
                    {
                        temp.Add(p);
                    }
                }
                foreach (var p in temp)
                {
                    projects.Remove(p);
                }
            }
            else if (priority == null)
            {
                ViewBag.Priority = priority;
                projects = projects.OrderBy(p => p.DeadLine).ToList();
            }

            var PriorityList = new Priority();
            int numOfNoticefromProject = 0;
            foreach (var project in projects)
            {
                numOfNoticefromProject += project.Notifications.Where(n=>n.Isopen==false).Count();
            }
            ViewBag.NumOfNotice = numOfNoticefromProject;
            
            



            var viewModel = new ViewModel(PriorityList,projects);
            return View(viewModel);
        }

        public IActionResult Notification(int nNumber)
        {
            var notices = _context.Notification.Include(n=>n.Project).Include(n=>n.AppUser)
                                               .Where(n => n.AppUserId != null || n.ProjectId!=null)
                                               .OrderBy(n=>n.Isopen)
                                               .ToList();
            ViewBag.NumOfNotice = nNumber;
            return View(notices);
        }
        [HttpPost]
        public IActionResult Notification(int id, bool isOpen )
        {
            var notification = _context.Notification.First(n=>n.Id == id);  
            notification.Isopen = isOpen;
            _context.Update(notification);
            _context.SaveChanges();
            return RedirectToAction("Notification");
        }
        
        public IActionResult CreateNotificationFromProject(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateNotificationFromProject()
        {
            //making notification with passed deadline with incomplited tasks of project
            var today = DateTime.Now.Date;
            var allprojectwitTasks = _context.Project.Include(p => p.AppTasks)
                                                     .Include(p=>p.Notifications);
            foreach (var project in allprojectwitTasks)
            {
                var completepercentages = project.AppTasks.Select(a => a.CompletePercentage).FirstOrDefault(c=> c==100);
                var newNotice = new Notification();
                var descriptions = project.Notifications.Select(n=>n.Description);
                if ((project.DeadLine - today).Days < 0 && completepercentages != 100)
                {
                    newNotice.ProjectId= project.Id;
                    newNotice.Description = $"{project.Name} deadline was passed with some incomplete tasks";
                    if(!descriptions.Contains(newNotice.Description))
                       _context.Notification.Add(newNotice);
                }
                
            }

            //making notification with complete task or project
            var alltasks = _context.AppTask;
            foreach (var task in alltasks)
            {
                
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
         
        // GET: ProjectHelper/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: ProjectHelper/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProjectHelper/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Project project)

        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: ProjectHelper/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: ProjectHelper/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Budget,StartDate,DeadLine")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: ProjectHelper/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: ProjectHelper/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Project.FindAsync(id);
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }

        public IActionResult NotFinishedAndPassedDeadlineTasks()
        {

            var notCompletedtasks = _context.AppTask.Include(a=>a.Project).Include(a=>a.AppUser)
                                                    .Where(a=>a.CompletePercentage<100).ToList();
                                        
            var passedDeadLineTasks = new List<AppTask>();
            foreach (var task in notCompletedtasks)
            {
                if ((task.DeadLine - DateTime.Now.Date).Days < 0)
                {
                    passedDeadLineTasks.Add(task);
                }
            }
            return View(passedDeadLineTasks);
        }

        public IActionResult AssignUser(int id)
        {
            var Project = _context.Project.First(P => P.Id == id);

            var users = _context.Users.Include(u => u.ProjectAndUsers)
                                      .Where(u => !u.ProjectAndUsers.Select(p=>p.ProjectId).Contains(id));

            ViewBag.UserList = new SelectList(users,"Id", "UserName");
            ViewBag.ProjectId = Project.Id; 
            return View();
        }
        [HttpPost]
        public IActionResult AssignUser(int projectId, string userId, 
                                        DateTime startDate, DateTime endDate)
        {
            var newPUser = new ProjectAndUser();
            newPUser.ProjectId = projectId; 
            newPUser.AppUserId = userId;
            newPUser.StartDate = startDate;
            newPUser.EndDate = endDate;
            _context.ProjectAndUser.Add(newPUser);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
