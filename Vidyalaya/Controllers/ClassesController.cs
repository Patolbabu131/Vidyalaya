using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vidyalaya.Data;
using Vidyalaya.Models;

namespace Vidyalaya.Controllers
{
    public class ClassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult GetClasses(JqueryDatatableParam param, int id)
        {
            
            var classes= _context.cclass.Where(row => row.SId==id).ToList();

            //Searching
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                classes = classes.Where(x => x.CId.ToString().Contains(param.sSearch.ToLower())
                                              || x.CName.ToString().Contains(param.sSearch.ToLower())
                                              || x.CSubject.ToString().Contains(param.sSearch.ToLower())
                                              || x.CStandard.ToString().Contains(param.sSearch.ToLower())
                                              || x.CRoomNo.ToString().Contains(param.sSearch.ToLower())
                                              || x.SId.ToString().Contains(param.sSearch.ToLower())).ToList();
            }
            //Sorting
            if (param.iSortCol_0 == 0)
            {
                classes = param.sSortDir_0 == "asc" ? classes.OrderBy(c => c.CId).ToList() : classes.OrderByDescending(c => c.CId).ToList();
            }
            else if (param.iSortCol_0 == 1)
            {
                classes = param.sSortDir_0 == "asc" ? classes.OrderBy(c => c.CName).ToList() : classes.OrderByDescending(c => c.CName).ToList();
            }
            else if (param.iSortCol_0 == 2)
            {
                classes = param.sSortDir_0 == "asc" ? classes.OrderBy(c => c.CSubject).ToList() : classes.OrderByDescending(c => c.CSubject).ToList();
            }
            else if (param.iSortCol_0 == 3)
            {
                classes = param.sSortDir_0 == "asc" ? classes.OrderBy(c => c.CStandard).ToList() : classes.OrderByDescending(c => c.CStandard).ToList();
            }
            else if (param.iSortCol_0 == 4)
            {
                classes = param.sSortDir_0 == "asc" ? classes.OrderBy(c => c.CRoomNo).ToList() : classes.OrderByDescending(c => c.CRoomNo).ToList();
            }
            else if (param.iSortCol_0 == 4)
            {
                classes = param.sSortDir_0 == "asc" ? classes.OrderBy(c => c.SId).ToList() : classes.OrderByDescending(c => c.SId).ToList();
            }

            //TotalRecords
            var displayResult = classes.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
            var totalRecords = classes.Count();
            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            });
        }

        // GET: Classes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.cclass == null)
            {
                return NotFound();
            }

            var @class = await _context.cclass
                .FirstOrDefaultAsync(m => m.CId == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // GET: Classes/Create
        public IActionResult Create1()
        {
            return PartialView("Create");
        }

        // POST: Classes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CId,CName,CSubject,CStandard,CRoomNo,SId")] Class @class)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@class);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "schools");
            }
            return RedirectToAction("Details", "schools");
        }

        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.cclass == null)
            {
                return NotFound();
            }

            var @class = await _context.cclass.FindAsync(id);
            if (@class == null)
            {
                return NotFound();
            }
            return View(@class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CId,CName,CSubject,CStandard,CRoomNo,SId")] Class @class)
        {
            if (id != @class.CId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@class);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(@class.CId))
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
            return View(@class);
        }

        // GET: Classes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.cclass == null)
            {
                return NotFound();
            }

            var @class = await _context.cclass
                .FirstOrDefaultAsync(m => m.CId == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.cclass == null)
            {
                return Problem("Entity set 'ApplicationDbContext.cclass'  is null.");
            }
            var @class = await _context.cclass.FindAsync(id);
            if (@class != null)
            {
                _context.cclass.Remove(@class);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassExists(int id)
        {
          return _context.cclass.Any(e => e.CId == id);
        }
    }
}
