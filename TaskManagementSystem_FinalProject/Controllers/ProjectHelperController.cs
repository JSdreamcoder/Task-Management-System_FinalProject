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
            
            var projects = await _context.Project.Include(p=>p.AppTasks).ToListAsync();



            //Whent ishideComplete is true, hide tasks with completepercentage is 100
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

            var PriorityList = new Priority();
            
            var viewModel = new ViewModel(PriorityList,projects);
            return View(viewModel);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Project project)
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
    }
}
