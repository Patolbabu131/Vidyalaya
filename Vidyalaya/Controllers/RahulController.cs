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
    public class RahulController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RahulController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rahul
        public async Task<IActionResult> Index()
        {
              return View(await _context.school.ToListAsync());
        }

        // GET: Rahul/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Rahul/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rahul/Create
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

        // GET: Rahul/Edit/5
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

        // POST: Rahul/Edit/5
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

        // GET: Rahul/Delete/5
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

        // POST: Rahul/Delete/5
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
