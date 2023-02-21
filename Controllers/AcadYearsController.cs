using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SMS.Data;
using sms.Models;

namespace sms.Controllers
{
    public class AcadYearsController : Controller
    {
        private readonly SMSContext _context;

        public AcadYearsController(SMSContext context)
        {
            _context = context;
        }

        // GET: AcadYears
        public async Task<IActionResult> Index()
        {
              return _context.AcadYear != null ? 
                          View(await _context.AcadYear.ToListAsync()) :
                          Problem("Entity set 'SMSContext.AcadYear'  is null.");
        }

        // GET: AcadYears/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AcadYear == null)
            {
                return NotFound();
            }

            var acadYear = await _context.AcadYear
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acadYear == null)
            {
                return NotFound();
            }

            return View(acadYear);
        }

        // GET: AcadYears/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AcadYears/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] AcadYear acadYear)
        {
            if (ModelState.IsValid)
            {
                _context.Add(acadYear);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(acadYear);
        }

        // GET: AcadYears/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AcadYear == null)
            {
                return NotFound();
            }

            var acadYear = await _context.AcadYear.FindAsync(id);
            if (acadYear == null)
            {
                return NotFound();
            }
            return View(acadYear);
        }

        // POST: AcadYears/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] AcadYear acadYear)
        {
            if (id != acadYear.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(acadYear);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcadYearExists(acadYear.Id))
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
            return View(acadYear);
        }

        // GET: AcadYears/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AcadYear == null)
            {
                return NotFound();
            }

            var acadYear = await _context.AcadYear
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acadYear == null)
            {
                return NotFound();
            }

            return View(acadYear);
        }

        // POST: AcadYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AcadYear == null)
            {
                return Problem("Entity set 'SMSContext.AcadYear'  is null.");
            }
            var acadYear = await _context.AcadYear.FindAsync(id);
            if (acadYear != null)
            {
                _context.AcadYear.Remove(acadYear);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcadYearExists(int id)
        {
          return (_context.AcadYear?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
