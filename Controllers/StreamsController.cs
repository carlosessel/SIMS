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
    public class StreamsController : Controller
    {
        private readonly SMSContext _context;

        public StreamsController(SMSContext context)
        {
            _context = context;
        }

        // GET: Streams
        public async Task<IActionResult> Index()
        {
              return _context.Streams != null ? 
                          View(await _context.Streams.ToListAsync()) :
                          Problem("Entity set 'SMSContext.Streams'  is null.");
        }

        // GET: Streams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Streams == null)
            {
                return NotFound();
            }

            var streams = await _context.Streams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (streams == null)
            {
                return NotFound();
            }

            return View(streams);
        }

        // GET: Streams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Streams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Streams streams)
        {
            if (ModelState.IsValid)
            {
                _context.Add(streams);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(streams);
        }

        // GET: Streams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Streams == null)
            {
                return NotFound();
            }

            var streams = await _context.Streams.FindAsync(id);
            if (streams == null)
            {
                return NotFound();
            }
            return View(streams);
        }

        // POST: Streams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Streams streams)
        {
            if (id != streams.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(streams);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StreamsExists(streams.Id))
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
            return View(streams);
        }

        // GET: Streams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Streams == null)
            {
                return NotFound();
            }

            var streams = await _context.Streams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (streams == null)
            {
                return NotFound();
            }

            return View(streams);
        }

        // POST: Streams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Streams == null)
            {
                return Problem("Entity set 'SMSContext.Streams'  is null.");
            }
            var streams = await _context.Streams.FindAsync(id);
            if (streams != null)
            {
                _context.Streams.Remove(streams);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StreamsExists(int id)
        {
          return (_context.Streams?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
