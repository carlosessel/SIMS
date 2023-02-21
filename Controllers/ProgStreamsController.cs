using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SMS.Data;
using sms.Models;
using sms.Models.ViewModels;

namespace sms.Controllers
{
    public class ProgStreamsController : Controller
    {
        private readonly SMSContext _context;

        public ProgStreamsController(SMSContext context)
        {
            _context = context;
        }

        // GET: ProgStreams
        public async Task<IActionResult> Index()
        {
            var sMSContext = _context.ProgStream.Include(p => p.GradingSystem).Include(p => p.Programme).Include(p => p.Streams).Include(p => p.Department);
            return View(await sMSContext.ToListAsync());
        }

        // GET: ProgStreams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProgStream == null)
            {
                return NotFound();
            }

            var progStream = await _context.ProgStream
                .Include(p => p.GradingSystem)
                .Include(p => p.Programme)
                .Include(p => p.Streams)
                .Include(p => p.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (progStream == null)
            {
                return NotFound();
            }

            return View(progStream);
        }

        // GET: ProgStreams/Create
        public IActionResult Create()
        {
            ViewData["GradingSystemId"] = new SelectList(_context.GradingSystem, "Id", "Name");
            ViewData["ProgrammeId"] = new SelectList(_context.Programme, "Id", "Name");
            ViewData["StreamsId"] = new SelectList(_context.Streams, "Id", "Name");
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Name");
            return View();
        }

        // POST: ProgStreams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProgStreamViewModel progStream)
        {
            var _streamName =  _context.Streams.Single(s => s.Id == progStream.StreamsId).Name;
            var _programmeName =  _context.Programme.Single(p => p.Id == progStream.ProgrammeId).Name;
            String _progStreamName = $"{_programmeName} {_streamName}";
            
            if (ModelState.IsValid)
            {
                var newProgStream = new ProgStream
                {
                    Id = progStream.Id,
                    ProgrammeId = progStream.ProgrammeId,
                    StreamsId = progStream.StreamsId,
                    GradingSystemId = progStream.GradingSystemId,
                    ProgStreamName =  progStream.StreamsId != 1 ? _progStreamName : _programmeName,
                    DepartmentId = progStream.DepartmentId
                };
                _context.Add(newProgStream);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GradingSystemId"] = new SelectList(_context.GradingSystem, "Id", "Name", progStream.GradingSystemId);
            ViewData["ProgrammeId"] = new SelectList(_context.Programme, "Id", "Name", progStream.ProgrammeId);
            ViewData["StreamsId"] = new SelectList(_context.Streams, "Id", "Name", progStream.StreamsId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Name", progStream.DepartmentId);
            return View(progStream);
        }

        // GET: ProgStreams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProgStream == null)
            {
                return NotFound();
            }

            var progStream = await _context.ProgStream.FindAsync(id);
            if (progStream == null)
            {
                return NotFound();
            }
            ViewData["GradingSystemId"] = new SelectList(_context.GradingSystem, "Id", "Name", progStream.GradingSystemId);
            ViewData["ProgrammeId"] = new SelectList(_context.Programme, "Id", "Name", progStream.ProgrammeId);
            ViewData["StreamsId"] = new SelectList(_context.Streams, "Id", "Name", progStream.StreamsId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Name", progStream.DepartmentId);
            return View(progStream);
        }

        // POST: ProgStreams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProgStreamViewModel progStream)
        {
            if (id != progStream.Id)
            {
                return NotFound();
            }
            var _streamName =  _context.Streams.Single(s => s.Id == progStream.StreamsId).Name;
            var _programmeName =  _context.Programme.Single(p => p.Id == progStream.ProgrammeId).Name;
            String _progStreamName = $"{_programmeName} {_streamName}";

            if (ModelState.IsValid)
            {
                try
                {
                var newProgStream = new ProgStream
                {
                    Id = progStream.Id,
                    ProgrammeId = progStream.ProgrammeId,
                    StreamsId = progStream.StreamsId,
                    GradingSystemId = progStream.GradingSystemId,
                    ProgStreamName = progStream.StreamsId != 1 ? _progStreamName : _programmeName,
                    DepartmentId = progStream.DepartmentId
                };
                    _context.Update(newProgStream);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgStreamExists(progStream.Id))
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
            ViewData["GradingSystemId"] = new SelectList(_context.GradingSystem, "Id", "Name", progStream.GradingSystemId);
            ViewData["ProgrammeId"] = new SelectList(_context.Programme, "Id", "Name", progStream.ProgrammeId);
            ViewData["StreamsId"] = new SelectList(_context.Streams, "Id", "Name", progStream.StreamsId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Name", progStream.DepartmentId);
            return View(progStream);
        }

        // GET: ProgStreams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProgStream == null)
            {
                return NotFound();
            }

            var progStream = await _context.ProgStream
                .Include(p => p.GradingSystem)
                .Include(p => p.Programme)
                .Include(p => p.Streams)
                .Include(p => p.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (progStream == null)
            {
                return NotFound();
            }

            return View(progStream);
        }

        // POST: ProgStreams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProgStream == null)
            {
                return Problem("Entity set 'SMSContext.ProgStream'  is null.");
            }
            var progStream = await _context.ProgStream.FindAsync(id);
            if (progStream != null)
            {
                _context.ProgStream.Remove(progStream);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgStreamExists(int id)
        {
          return (_context.ProgStream?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
