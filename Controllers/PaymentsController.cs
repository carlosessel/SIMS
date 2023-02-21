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
    public class PaymentsController : Controller
    {
        private readonly SMSContext _context;

        public PaymentsController(SMSContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index(int? id)
        {
            if (id != null)
            {
                var specificStudent = _context.Student
                .Include(s => s.ProgStream)
                .FirstOrDefault(s => s.Id == id);

                ViewData["StudentNumber"] = specificStudent.StudentNumber;
                ViewData["StudentName"] = specificStudent.FullName;
                ViewData["Programme"] = specificStudent.ProgStream.ProgStreamName;
                ViewData["Id"] = id;

                var sMSContextForId = _context.Payment
                .Include(p => p.PaymentType)
                .Include(p => p.Student)
                .Where(p => p.StudentId == id);    
               
                return View(await sMSContextForId.ToListAsync());
            }

            var sMSContext = _context.Payment
            .Include(p => p.PaymentType)
            .Include(p => p.Student);

            return View(await sMSContext.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Payment == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.PaymentType)
                .Include(p => p.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            var specificStudent = _context.Student
                .Include(s => s.ProgStream)
                .Single(s => s.Id == _context.Payment.Single(p => p.Id == id).StudentId);

            ViewData["Id"] = specificStudent.Id;

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create(int? id, string? searchString)
        {
            if (searchString != null)
            {
                ViewData["SearchedStudent"] = searchString;
                var student = _context.Student.Single(s => s.FullName.Contains(searchString));
                ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FullName", student.Id);
                ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "Id", "Name");
                return View();
            }
            if (id != null)
            {
                var specificStudent
                = _context.Student
                .Include(s => s.ProgStream)
                .Single(s => s.Id == id);

                ViewData["Id"] = id;
                ViewData["SpecificStudentId"] = specificStudent.Id;
                ViewData["StudentNumber"] = specificStudent.StudentNumber;
                ViewData["StudentName"] = specificStudent.FullName;
                ViewData["Programme"] = specificStudent.ProgStream.ProgStreamName;
                
                ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "Id", "Name");
                return View();
            }
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "Id", "Name");
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FullName");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("StudentId,Amount,PaymentTypeId,Date")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                if (id != null)
                {
                    return RedirectToAction("Index", "Payments", new{ id = id});
                }
                return RedirectToAction(nameof(Index));
            }
            if (id != null)
            {
                var specificStudent
                = _context.Student
                .Include(s => s.ProgStream)
                .Single(s => s.Id == id);

                ViewData["Id"] = id;
                ViewData["SpecificStudentId"] = specificStudent.Id;
                ViewData["StudentNumber"] = specificStudent.StudentNumber;
                ViewData["StudentName"] = specificStudent.FullName;
                ViewData["Programme"] = specificStudent.ProgStream.ProgStreamName;
                
                ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "Id", "Name");
                return View(payment);
            }
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "Id", "Name", payment.PaymentTypeId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FullName", payment.StudentId);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? studentId, int id)
        {
            if (_context.Payment == null)
            {
                return NotFound();
            }

            if (studentId != null)
            {
                ViewData["Id"] = studentId;
                var paymentForStudentId = await _context.Payment.FindAsync(id);
                if (paymentForStudentId == null)
                {
                    return NotFound();
                }

                var specificStudent
                = _context.Student
                .Include(s => s.ProgStream)
                .Single(s => s.Id == studentId);

                ViewData["SpecificStudentId"] = specificStudent.Id;
                ViewData["StudentNumber"] = specificStudent.StudentNumber;
                ViewData["StudentName"] = specificStudent.FullName;
                ViewData["Programme"] = specificStudent.ProgStream.ProgStreamName;

                ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "Id", "Name", paymentForStudentId.PaymentTypeId);
                return View(paymentForStudentId);
            }

            var payment = await _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "Id", "Name", payment.PaymentTypeId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FullName", payment.StudentId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? studentId, int id, [Bind("Id,StudentId,Amount,PaymentTypeId,Date")] Payment payment)
        {
            if (id != payment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (studentId != null)
                {
                    return RedirectToAction("Index", "Payments", new{ id = studentId});
                }
                return RedirectToAction(nameof(Index));
            }
            if (studentId != null)
            {
                var specificStudent
                = _context.Student
                .Include(s => s.ProgStream)
                .Single(s => s.Id == id);

                ViewData["Id"] = id;
                ViewData["SpecificStudentId"] = specificStudent.Id;
                ViewData["StudentNumber"] = specificStudent.StudentNumber;
                ViewData["StudentName"] = specificStudent.FullName;
                ViewData["Programme"] = specificStudent.ProgStream.ProgStreamName;
                
                ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "Id", "Name");
                return View(payment);
            }
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "Id", "Name", payment.PaymentTypeId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FullName", payment.StudentId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? studentId, int id)
        {
            if (id == null || _context.Payment == null)
            {
                return NotFound();
            }

            if (studentId != null)
            {
                ViewData["Id"] = studentId;

                var paymentForStudentId = await _context.Payment
                .Include(p => p.PaymentType)
                .Include(p => p.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentForStudentId == null)
            {
                return NotFound();
            }

            var specificStudent
                = _context.Student
                .Include(s => s.ProgStream)
                .Single(s => s.Id == studentId);
            
            ViewData["SpecificStudentId"] = specificStudent.Id;

            return View(paymentForStudentId);
            }

            var payment = await _context.Payment
                .Include(p => p.PaymentType)
                .Include(p => p.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);

        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? studentId, int id)
        {
            if (_context.Payment == null)
            {
                return Problem("Entity set 'SMSContext.Payment'  is null.");
            }
            var payment = await _context.Payment.FindAsync(id);
            if (payment != null)
            {
                _context.Payment.Remove(payment);
            }
            
            await _context.SaveChangesAsync();
            if (studentId != null)
            {
                return RedirectToAction("Index", "Payments", new{ id = studentId});
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
          return (_context.Payment?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
