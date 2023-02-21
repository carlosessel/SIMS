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
    public class GradingSystemsController : Controller
    {
        private readonly SMSContext _context;

        public GradingSystemsController(SMSContext context)
        {
            _context = context;
        }

        // GET: GradingSystems
        public async Task<IActionResult> Index()
        {
              return _context.GradingSystem != null ? 
                          View(await _context.GradingSystem.ToListAsync()) :
                          Problem("Entity set 'SMSContext.GradingSystem'  is null.");
        }

        // GET: GradingSystems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GradingSystem == null)
            {
                return NotFound();
            }

            var gradingSystem = await _context.GradingSystem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gradingSystem == null)
            {
                return NotFound();
            }

            return View(gradingSystem);
        }

        // GET: GradingSystems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GradingSystems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] GradingSystem gradingSystem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gradingSystem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gradingSystem);
        }

        // GET: GradingSystems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GradingSystem == null)
            {
                return NotFound();
            }

            var gradingSystem = await _context.GradingSystem.FindAsync(id);
            if (gradingSystem == null)
            {
                return NotFound();
            }
            return View(gradingSystem);
        }

        // POST: GradingSystems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] GradingSystem gradingSystem)
        {
            if (id != gradingSystem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gradingSystem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradingSystemExists(gradingSystem.Id))
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
            return View(gradingSystem);
        }

        // GET: GradingSystems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GradingSystem == null)
            {
                return NotFound();
            }

            var gradingSystem = await _context.GradingSystem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gradingSystem == null)
            {
                return NotFound();
            }

            return View(gradingSystem);
        }

        // POST: GradingSystems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GradingSystem == null)
            {
                return Problem("Entity set 'SMSContext.GradingSystem'  is null.");
            }
            var gradingSystem = await _context.GradingSystem.FindAsync(id);
            if (gradingSystem != null)
            {
                _context.GradingSystem.Remove(gradingSystem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradingSystemExists(int id)
        {
          return (_context.GradingSystem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
