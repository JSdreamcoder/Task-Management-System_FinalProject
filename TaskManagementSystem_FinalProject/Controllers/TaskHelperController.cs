﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem_FinalProject.Data;
using TaskManagementSystem_FinalProject.Models;

namespace TaskManagementSystem_FinalProject.Controllers
{
    public class TaskHelperController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaskHelperController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TaskHelper123

        //test123
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AppTask.Include(a => a.AppUser).Include(a => a.Project);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TaskHelper/Details/5
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

        // GET: TaskHelper/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.AppUser, "Id", "Id");
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Id");
            return View();
        }

        // POST: TaskHelper/Create
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

        // GET: TaskHelper/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: TaskHelper/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CompletePercentage,Comment,ProjectId,AppUserId")] AppTask appTask)
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

        // GET: TaskHelper/Delete/5
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

        // POST: TaskHelper/Delete/5
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
