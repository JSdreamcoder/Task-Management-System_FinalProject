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
    [Authorize(Roles ="Developer")]
    public class DeveloperController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeveloperController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Developers
        public IActionResult Index()
        
        {
            
            var userName = User.Identity.Name;
            ViewBag.UserName = userName;
            var user = _context.AppUser.First(u=>u.UserName == userName);
            var userId = user.Id;
            var applicationDbContext = _context.AppTask.Include(a => a.AppUser)
                                                       .Include(a => a.Project)
                                                       .Include(a=>a.Notifications)
                                                       .Where(t=>t.AppUserId == userId);

            var today = DateTime.Now;
            int numberOfNotice = 0;
            
            foreach (var task in applicationDbContext)
            {
                numberOfNotice += task.Notifications.Where(n => n.Isopen == false).Count();
            }
            ViewBag.NumberOfNotice = numberOfNotice;
            ViewBag.UserId=userId;
            return View(applicationDbContext.ToList());
        }

        public  IActionResult NoticePage()
        {
            var userName = User.Identity.Name;
            ViewBag.UserName = userName;
            var user = _context.AppUser.First(u => u.UserName == userName);
            var userId = user.Id;
            
            var notices = _context.AppUser.Include(a => a.AppTasks).ThenInclude(t => t.Notifications)
                                          .First(a => a.UserName == userName);
            return View(notices);
        }
        [HttpPost]
        public IActionResult NoticePage(int id)
        {
            var notice = _context.Notification.First(u => u.Id == id);
            notice.Isopen = true;  
            _context.Update(notice);
            _context.SaveChanges();
            return RedirectToAction("NoticePage");
        }

        public IActionResult CreateTaskNotice()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateTaskNotice(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            var userName = User.Identity.Name;
            ViewBag.UserName = userName;
            var user = _context.AppUser.First(u => u.UserName == userName);
            var userId = user.Id;
            var applicationDbContext = _context.AppTask.Include(a => a.AppUser).Include(a => a.Project)
                                                       .Where(t => t.AppUserId == userId);
            var notifications = _context.Notification;
            var listOfTaskIdinNotification = notifications.Select(n=>n.AppTaskId).ToList();
            var today = DateTime.Now.Date;
            

            foreach (var task in applicationDbContext)
            {
                var diffOfDates = task.DeadLine - today;
                var number = diffOfDates.Days;

                if (listOfTaskIdinNotification.Contains(task.Id))
                {

                    var notice = notifications.First(n => n.AppTaskId == task.Id);
                    if (number == 1)
                    {
                        notice.Description = $"{task.Name} dead-Line remains 1 day";

                    }
                    else if (number == 0)
                    {
                        notice.Description = $"{task.Name} dead-Line is today";
                    }
                    else if (number < 0)
                    {
                        notice.Description = $"{task.Name} dead-Line is passed";
                    }
                    _context.Update(notice);
                }
                else
                {

                    var newNotice = new Notification();
                    newNotice.AppTaskId = task.Id;
                    if (number == 1)
                    {
                        newNotice.Description = $"{task.Name} dead-Line remains 1 day";
                        _context.Notification.Add(newNotice);
                    }
                    else if (number == 0)
                    {
                        newNotice.Description = $"{task.Name} dead-Line is today";
                        _context.Notification.Add(newNotice);

                    }
                    else if (number < 0)
                    {
                        newNotice.Description = $"{task.Name} dead-Line is passed";
                        _context.Notification.Add(newNotice);

                    }
                    
                }
                
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Developer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appTask = await _context.AppTask
                .Include(a => a.AppUser)
                .Include(a => a.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appTask == null)
            {
                return NotFound();
            }

            return View(appTask);
        }

        // GET: Developer/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.AppUser, "Id", "Id");
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Id");
            return View();
        }

        // POST: Developer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CompletePercentage,Comment,ProjectId,AppUserId")] AppTask appTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUser, "Id", "Id", appTask.AppUserId);
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Id", appTask.ProjectId);
            return View(appTask);
        }

        // GET: Developer/Edit/5
        public async Task<IActionResult> EditComment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appTask = await _context.AppTask.FindAsync(id);
            if (appTask == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUser, "Id", "Id", appTask.AppUserId);
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Id", appTask.ProjectId);
            return View(appTask);
        }

        // POST: Developer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditComment(int id, [Bind("Id,Name,CompletePercentage,Comment,ProjectId,AppUserId,DeadLine")] AppTask appTask)
        {
            if (id != appTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppTaskExists(appTask.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.AppUser, "Id", "Id", appTask.AppUserId);
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Id", appTask.ProjectId);
            return View(appTask);
        }

        public async Task<IActionResult> EditPercentage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appTask = await _context.AppTask.FindAsync(id);
            if (appTask == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUser, "Id", "Id", appTask.AppUserId);
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Id", appTask.ProjectId);
            return View(appTask);
        }

        // POST: Developer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPercentage(int id, [Bind("Id,Name,CompletePercentage,Comment,ProjectId,AppUserId")] AppTask appTask)
        {

           


            if (id != appTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   //notice when task complete
                    if (appTask.CompletePercentage == 100)
                    {
                        var newNotice = new Notification();
                        newNotice.Description = $"{appTask.Name} is completed";
                        newNotice.ProjectId = appTask.ProjectId;
                        newNotice.AppUserId = appTask.AppUserId;
                        _context.Notification.Add(newNotice);
                        
                    }
                    _context.Update(appTask);
                    await _context.SaveChangesAsync();

                    //notice when project complete
                    var projectId = appTask.ProjectId;
                    var project = _context.Project.Include(p => p.AppTasks).First(p => p.Id == projectId);
                    if (project.AppTasks.All(a => a.CompletePercentage == 100))
                    {
                        var newNotice = new Notification();
                        newNotice.Description = $"{project.Name} is completed";
                        newNotice.ProjectId = projectId;
                        _context.Notification.Add(newNotice);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppTaskExists(appTask.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.AppUser, "Id", "Id", appTask.AppUserId);
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Id", appTask.ProjectId);
            //notification with completepercentage is 100
           

            return View(appTask);
        }

        // GET: Developer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appTask = await _context.AppTask
                .Include(a => a.AppUser)
                .Include(a => a.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appTask == null)
            {
                return NotFound();
            }

            return View(appTask);
        }

        public IActionResult UrgentNote(string userId,int taskId)
        {
            
            ViewBag.AppUserId = userId;
            ViewBag.TaskId = taskId;
            return View();
        }
        [HttpPost]
        public IActionResult UrgentNote(string userId,string description,int taskId)
        {
            var task = _context.AppTask.First(a => a.Id == taskId);
            var notice = new Notification();
            notice.AppUserId = userId;
            notice.Description = description;
            notice.ProjectId = task.ProjectId;
            _context.Notification.Add(notice);
            _context.SaveChanges();
            return View();
        }

        // POST: Developer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appTask = await _context.AppTask.FindAsync(id);
            _context.AppTask.Remove(appTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppTaskExists(int id)
        {
            return _context.AppTask.Any(e => e.Id == id);
        }
    }
}
