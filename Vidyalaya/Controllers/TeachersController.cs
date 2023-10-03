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
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeachersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Teachers
        public async Task<IActionResult> Index()
        {
              return View(await _context.teachers.ToListAsync());
        }
        public ActionResult GetTeachers(JqueryDatatableParam param, int id)
        {

            var teachers = _context.teachers.Where(row => row.SId == id).ToList();

            //Searching
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                teachers = teachers.Where(x => x.TId.ToString().Contains(param.sSearch.ToLower())
                                              || x.TName.ToString().Contains(param.sSearch.ToLower())
                                              || x.TSubject.ToString().Contains(param.sSearch.ToLower())
                                              || x.TStandard.ToString().Contains(param.sSearch.ToLower())).ToList();
            }
            //Sorting
            if (param.iSortCol_0 == 0)
            {
                teachers = param.sSortDir_0 == "asc" ? teachers.OrderBy(c => c.TId).ToList() : teachers.OrderByDescending(c => c.TId).ToList();
            }
            else if (param.iSortCol_0 == 1)
            {
                teachers = param.sSortDir_0 == "asc" ? teachers.OrderBy(c => c.TName).ToList() : teachers.OrderByDescending(c => c.TName).ToList();
            }
            else if (param.iSortCol_0 == 2)
            {
                teachers = param.sSortDir_0 == "asc" ? teachers.OrderBy(c => c.TSubject).ToList() : teachers.OrderByDescending(c => c.TSubject).ToList();
            }
            else if (param.iSortCol_0 == 3)
            {
                teachers = param.sSortDir_0 == "asc" ? teachers.OrderBy(c => c.TStandard).ToList() : teachers.OrderByDescending(c => c.TStandard).ToList();
            }
     
            //TotalRecords
            var displayResult = teachers.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
            var totalRecords = teachers.Count();
            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            });
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.teachers == null)
            {
                return NotFound();
            }

            var teachers = await _context.teachers
                .FirstOrDefaultAsync(m => m.TId == id);
            if (teachers == null)
            {
                return NotFound();
            }

            return View(teachers);
        }

        // GET: Teachers/Create
        public IActionResult Create1()
        {
            return PartialView("Create");
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TId,TName,TSubject,TStandard,SId")] Teachers teachers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teachers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teachers);
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.teachers == null)
            {
                return NotFound();
            }

            var teachers = await _context.teachers.FindAsync(id);
            if (teachers == null)
            {
                return NotFound();
            }
            return View(teachers);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TId,TName,TSubject,TStandard,SId")] Teachers teachers)
        {
            if (id != teachers.TId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teachers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeachersExists(teachers.TId))
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
            return View(teachers);
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.teachers == null)
            {
                return NotFound();
            }

            var teachers = await _context.teachers
                .FirstOrDefaultAsync(m => m.TId == id);
            if (teachers == null)
            {
                return NotFound();
            }

            return View(teachers);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.teachers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.teachers'  is null.");
            }
            var teachers = await _context.teachers.FindAsync(id);
            if (teachers != null)
            {
                _context.teachers.Remove(teachers);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeachersExists(int id)
        {
          return _context.teachers.Any(e => e.TId == id);
        }
    }
}
