using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vidyalaya.Data;
using Vidyalaya.Models;

namespace Vidyalaya.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Activities
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult GetActivities(JqueryDatatableParam param, int id)
        {
            var activities = _context.activities.Where(row => row.SId == id).ToList();

            //Searching
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                activities = activities.Where(x => x.SId.ToString().Contains(param.sSearch.ToLower())
                                              || x.AId.ToString().Contains(param.sSearch.ToLower())
                                              || x.ATitle.ToString().Contains(param.sSearch.ToLower())
                                              || x.ADescription.ToString().Contains(param.sSearch.ToLower())).ToList();
            }
            //Sorting
            if (param.iSortCol_0 == 0)
            {
                activities = param.sSortDir_0 == "asc" ? activities.OrderBy(c => c.SId).ToList() : activities.OrderByDescending(c => c.SId).ToList();
            }
            else if (param.iSortCol_0 == 1)
            {
                activities = param.sSortDir_0 == "asc" ? activities.OrderBy(c => c.AId).ToList() : activities.OrderByDescending(c => c.AId).ToList();
            }
            else if (param.iSortCol_0 == 2)
            {
                activities = param.sSortDir_0 == "asc" ? activities.OrderBy(c => c.ATitle).ToList() : activities.OrderByDescending(c => c.ATitle).ToList();
            }
            else if (param.iSortCol_0 == 3)
            {
                activities = param.sSortDir_0 == "asc" ? activities.OrderBy(c => c.ADescription).ToList() : activities.OrderByDescending(c => c.ADescription).ToList();
            }
          

            //TotalRecords
            var displayResult = activities.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
            var totalRecords = activities.Count();
            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            });
        }

        // GET: Activities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.activities == null)
            {
                return NotFound();
            }

            var activities = await _context.activities
                .FirstOrDefaultAsync(m => m.AId == id);
            if (activities == null)
            {
                return NotFound();
            }

            return View(activities);
        }

        // GET: Activities/Create
        public IActionResult Create1()
        {
            return PartialView("Create");
        }

        // POST: Activities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AId,ATitle,ADescription,SId")] Activities activities)
        {
            if (ModelState.IsValid)
            {
                _context.Add(activities);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "schools");
            }
            return RedirectToAction("Index", "schools");
        }

        // GET: Activities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.activities == null)
            {
                return NotFound();
            }

            var activities = await _context.activities.FindAsync(id);
            if (activities == null)
            {
                return NotFound();
            }
            return View(activities);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AId,ATitle,ADescription,SId")] Activities activities)
        {
            if (id != activities.AId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivitiesExists(activities.AId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "schools");
            }
            return View(activities);
        }

        // GET: Activities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.activities == null)
            {
                return NotFound();
            }

            var activities = await _context.activities
                .FirstOrDefaultAsync(m => m.AId == id);
            if (activities == null)
            {
                return NotFound();
            }

            return View(activities);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.activities == null)
            {
                return Problem("Entity set 'ApplicationDbContext.activities'  is null.");
            }
            var activities = await _context.activities.FindAsync(id);
            if (activities != null)
            {
                _context.activities.Remove(activities);
            }  
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "schools");
        }
        private bool ActivitiesExists(int id)
        {
          return _context.activities.Any(e => e.AId == id);
        }
    }
}
