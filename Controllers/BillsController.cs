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
    public class BillsController : Controller
    {
        private readonly SMSContext _context;

        public BillsController(SMSContext context)
        {
            _context = context;
        }

        // GET: Bills
        public async Task<IActionResult> Index()
        {
            var sMSContext = _context.Bill.Include(b => b.AcadYear).Include(b => b.ProgStream).Include(b => b.YearLevel);
            return View(await sMSContext.ToListAsync());
        }

        // GET: Bills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bill == null)
            {
                return NotFound();
            }

            var bill = await _context.Bill
                .Include(b => b.AcadYear)
                .Include(b => b.ProgStream)
                .Include(b => b.YearLevel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // GET: Bills/Create
        public IActionResult Create()
        {
            ViewData["AcadYearId"] = new SelectList(_context.AcadYear, "Id", "Name");
            ViewData["ProgStreamId"] = new SelectList(_context.ProgStream, "Id", "ProgStreamName");
            ViewData["YearLevelId"] = new SelectList(_context.YearLevel, "Id", "YearLevelName");
            return View();
        }

        // POST: Bills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,ProgStreamId,AcadYearId,YearLevelId")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AcadYearId"] = new SelectList(_context.AcadYear, "Id", "Name", bill.AcadYearId);
            ViewData["ProgStreamId"] = new SelectList(_context.ProgStream, "Id", "ProgStreamName", bill.ProgStreamId);
            ViewData["YearLevelId"] = new SelectList(_context.YearLevel, "Id", "YearLevelName", bill.YearLevelId);
            return View(bill);
        }

        // GET: Bills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bill == null)
            {
                return NotFound();
            }

            var bill = await _context.Bill.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            ViewData["AcadYearId"] = new SelectList(_context.AcadYear, "Id", "Name", bill.AcadYearId);
            ViewData["ProgStreamId"] = new SelectList(_context.ProgStream, "Id", "ProgStreamName", bill.ProgStreamId);
            ViewData["YearLevelId"] = new SelectList(_context.YearLevel, "Id", "YearLevelName", bill.YearLevelId);
            return View(bill);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,ProgStreamId,AcadYearId,YearLevelId")] Bill bill)
        {
            if (id != bill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bill.Id))
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
            ViewData["AcadYearId"] = new SelectList(_context.AcadYear, "Id", "Name", bill.AcadYearId);
            ViewData["ProgStreamId"] = new SelectList(_context.ProgStream, "Id", "ProgStreamName", bill.ProgStreamId);
            ViewData["YearLevelId"] = new SelectList(_context.YearLevel, "Id", "YearLevelName", bill.YearLevelId);
            return View(bill);
        }

        // GET: Bills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bill == null)
            {
                return NotFound();
            }

            var bill = await _context.Bill
                .Include(b => b.AcadYear)
                .Include(b => b.ProgStream)
                .Include(b => b.YearLevel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bill == null)
            {
                return Problem("Entity set 'SMSContext.Bill'  is null.");
            }
            var bill = await _context.Bill.FindAsync(id);
            if (bill != null)
            {
                _context.Bill.Remove(bill);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillExists(int id)
        {
          return (_context.Bill?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
