    using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vidyalaya.Data;
using Vidyalaya.Models;

namespace Vidyalaya.Controllers
{
    public class SchoolsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchoolsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Schools
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult GetData(JqueryDatatableParam param)
        {
            var schools = _context.school.ToList();

            //Searching
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                schools = schools.Where(x => x.SId.ToString().Contains(param.sSearch.ToLower())
                                              || x.SName.ToString().Contains(param.sSearch.ToLower())
                                              || x.SAddress.ToString().Contains(param.sSearch.ToLower())
                                              || x.SCity.ToString().Contains(param.sSearch.ToLower())
                                              || x.SState.ToString().Contains(param.sSearch.ToLower())).ToList();
            }
            //Sorting
            if (param.iSortCol_0 == 0)
            {
                schools = param.sSortDir_0 == "asc" ? schools.OrderBy(c => c.SId).ToList() : schools.OrderByDescending(c => c.SId).ToList();
            }
            else if (param.iSortCol_0 == 1)
            {
                schools = param.sSortDir_0 == "asc" ? schools.OrderBy(c => c.SName).ToList() : schools.OrderByDescending(c => c.SName).ToList();
            }
            else if (param.iSortCol_0 == 2)
            {
                schools = param.sSortDir_0 == "asc" ? schools.OrderBy(c => c.SAddress).ToList() : schools.OrderByDescending(c => c.SAddress).ToList();
            }
            else if (param.iSortCol_0 == 3)
            {
                schools = param.sSortDir_0 == "asc" ? schools.OrderBy(c => c.SCity).ToList() : schools.OrderByDescending(c => c.SCity).ToList();
            }
            else if (param.iSortCol_0 == 4)
            {
                schools = param.sSortDir_0 == "asc" ? schools.OrderBy(c => c.SState).ToList() : schools.OrderByDescending(c => c.SState).ToList();
            }

            //TotalRecords
            var displayResult = schools.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
            var totalRecords = schools.Count();
            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            });
        }

        // GET: Schools

        // GET: Schools/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var Schoooll = await _context.school.FindAsync(id);
            ViewBag.VBFriend = Schoooll.SId;

            if (id == null || _context.school == null)
            {
                return NotFound();
            }

            var school = await _context.school
                .FirstOrDefaultAsync(m => m.SId == id);
            if (school == null)
            {
                return NotFound();
            }

            return View(school);
        }

        // GET: Schools/Create
        public IActionResult Create(int id = 0)
        { 
            return View();
        }

        // POST: Schools/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SId,SName,SAddress,SCity,SState")] School school)
        {
            if (ModelState.IsValid)
            {
                _context.Add(school);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(school);
        }

        // GET: Schools/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.school == null)
            {
                return NotFound();
            }

            var school = await _context.school.FindAsync(id);
            if (school == null)
            {
                return NotFound();
            }
            return View(school);
        }

        // POST: Schools/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SId,SName,SAddress,SCity,SState")] School school)
        {
            if (id != school.SId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(school);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolExists(school.SId))
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
            return View(school);
        }

        // GET: Schools/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.school == null)
            {
                return NotFound();
            }

            var school = await _context.school
                .FirstOrDefaultAsync(m => m.SId == id);
            if (school == null)
            {
                return NotFound();
            }

            return View(school);
        }

        // POST: Schools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.school == null)
            {
                return Problem("Entity set 'ApplicationDbContext.school'  is null.");
            }
            var school = await _context.school.FindAsync(id);
            if (school != null)
            {
                _context.school.Remove(school);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolExists(int id)
        {
          return _context.school.Any(e => e.SId == id);
        }
    }
}
