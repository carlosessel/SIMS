using System.Transactions;
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
    public class StudentController : Controller
    {
        private readonly SMSContext _context;

        public StudentController(SMSContext context)
        {
            _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
            var sMSContext = _context.Student.Include(s => s.Country).Include(s => s.Gender).Include(s => s.MaritalStatus).Include(s => s.ProgStream);
            return View(await sMSContext.ToListAsync());
        }

                // GET: Bill Available
        public async Task<IActionResult> Bill(int id)
        {
            var specificStudent = _context.Student
            .Include(s => s.ProgStream)
            .Include(s => s.CourseRegistration)
            .FirstOrDefault(s => s.Id == id);
            
            var specificStudentYearLevelMax = specificStudent.CourseRegistration.Select(s => s.YearLevelId).ToList().DefaultIfEmpty().Max();
            int specificStudentYearLevel;

            switch (specificStudentYearLevelMax)
            {
                case 1:
                  specificStudentYearLevel = 100;
                  break;

                case 2:
                  specificStudentYearLevel = 200;
                  break;

                case 3:
                  specificStudentYearLevel = 300;
                  break;

                case 4:
                  specificStudentYearLevel = 400;
                  break;

                case 5:
                  specificStudentYearLevel = 500;
                  break;

                case 6:
                  specificStudentYearLevel = 600;
                  break;

                case 7:
                  specificStudentYearLevel = 700;
                  break;
                
                default:
                  return View("ErrorPageForNoCourseRegistration");
            }

            if(
                _context.Bill.Select(b => b.ProgStreamId).ToList().Contains((int)specificStudent.ProgStreamId)
              )
            {
                if (specificStudentYearLevel == 100)
                {
                if (_context.Bill.FirstOrDefault(b => b.ProgStreamId == specificStudent.ProgStreamId && b.YearLevelId == 1) == null)
                {
                    ViewData["Id"] = id;
                    return View("BillNotAvailable");
                }
                ViewData["Id"] = id;
                ViewData["StudentNumber"] = specificStudent.StudentNumber;
                ViewData["StudentName"] = specificStudent.FullName;
                ViewData["Programme"] = specificStudent.ProgStream.ProgStreamName;
                ViewData["AcademicYear"] = _context.Bill.Include(b => b.AcadYear).FirstOrDefault(b => b.ProgStreamId == specificStudent.ProgStreamId && b.YearLevelId == 1).AcadYear.Name;
                double billAmount = _context.Bill.FirstOrDefault(b => b.ProgStreamId == specificStudent.ProgStreamId && b.YearLevelId == 1).Amount;
                ViewData["BillAmount"] = billAmount;
                double amountPaid =(double)_context.Payment.Where(p => p.StudentId == specificStudent.Id).Select(p => p.Amount).ToList().Sum();
                ViewData["AmountPaid"] = amountPaid;
                double amountDifference = billAmount - amountPaid;
                double balance;
                double amountOwed;
                if (amountDifference > 1)
                {
                    balance = amountDifference;
                    amountOwed = 0.00;
                }
                else if(amountDifference == 0.00)
                {
                    balance = 0.00;
                    amountOwed = 0.00;
                }
                else
                {
                    amountOwed = amountDifference * -1;
                    balance = 0.00;
                }

                ViewData["Balance"] = balance;
                ViewData["AmountOwed"] = amountOwed;

                return View("BillAvailable");
                }
                else if(specificStudentYearLevel == 200)
                {
                if (_context.Bill.FirstOrDefault(b => b.ProgStreamId == specificStudent.ProgStreamId && b.YearLevelId == 2) == null)
                {
                    ViewData["Id"] = id;
                    return View("BillNotAvailable");
                }
                ViewData["Id"] = id;
                ViewData["StudentNumber"] = specificStudent.StudentNumber;
                ViewData["StudentName"] = specificStudent.FullName;
                ViewData["Programme"] = specificStudent.ProgStream.ProgStreamName;
                ViewData["AcademicYear"] = _context.Bill.Include(b => b.AcadYear).FirstOrDefault(b => b.ProgStreamId == specificStudent.ProgStreamId && b.YearLevelId == 2).AcadYear.Name;
                double billAmount = _context.Bill.FirstOrDefault(b => b.ProgStreamId == specificStudent.ProgStreamId && b.YearLevelId == 2).Amount;
                ViewData["BillAmount"] = billAmount;
                double amountPaid =(double)_context.Payment.Where(p => p.StudentId == specificStudent.Id).Select(p => p.Amount).ToList().Sum();
                ViewData["AmountPaid"] = amountPaid;
                double amountDifference = billAmount - amountPaid;
                double balance;
                double amountOwed;
                if (amountDifference > 1)
                {
                    balance = amountDifference;
                    amountOwed = 0.00;
                }
                else if(amountDifference == 0.00)
                {
                    balance = 0.00;
                    amountOwed = 0.00;
                }
                else
                {
                    amountOwed = amountDifference * -1;
                    balance = 0.00;
                }

                ViewData["Balance"] = balance;
                ViewData["AmountOwed"] = amountOwed;

                return View("BillAvailable"); 
                }
                else if(specificStudentYearLevel == 300)
                {
                if (_context.Bill.FirstOrDefault(b => b.ProgStreamId == specificStudent.ProgStreamId && b.YearLevelId == 3) == null)
                {
                    ViewData["Id"] = id;
                    return View("BillNotAvailable");
                }
                ViewData["Id"] = id;
                ViewData["StudentNumber"] = specificStudent.StudentNumber;
                ViewData["StudentName"] = specificStudent.FullName;
                ViewData["Programme"] = specificStudent.ProgStream.ProgStreamName;
                ViewData["AcademicYear"] = _context.Bill.Include(b => b.AcadYear).FirstOrDefault(b => b.ProgStreamId == specificStudent.ProgStreamId && b.YearLevelId == 3).AcadYear.Name;
                double billAmount = _context.Bill.FirstOrDefault(b => b.ProgStreamId == specificStudent.ProgStreamId && b.YearLevelId == 3).Amount;
                ViewData["BillAmount"] = billAmount;
                double amountPaid =(double)_context.Payment.Where(p => p.StudentId == specificStudent.Id).Select(p => p.Amount).ToList().Sum();
                ViewData["AmountPaid"] = amountPaid;
                double amountDifference = billAmount - amountPaid;
                double balance;
                double amountOwed;
                if (amountDifference > 1)
                {
                    balance = amountDifference;
                    amountOwed = 0.00;
                }
                else if(amountDifference == 0.00)
                {
                    balance = 0.00;
                    amountOwed = 0.00;
                }
                else
                {
                    amountOwed = amountDifference * -1;
                    balance = 0.00;
                }

                ViewData["Balance"] = balance;
                ViewData["AmountOwed"] = amountOwed;

                return View("BillAvailable"); 
                }
                else if(specificStudentYearLevel == 400)
                {
                if (_context.Bill.FirstOrDefault(b => b.ProgStreamId == specificStudent.ProgStreamId && b.YearLevelId == 4) == null)
                {
                    ViewData["Id"] = id;
                    return View("BillNotAvailable");
                }
                ViewData["Id"] = id;
                ViewData["StudentNumber"] = specificStudent.StudentNumber;
                ViewData["StudentName"] = specificStudent.FullName;
                ViewData["Programme"] = specificStudent.ProgStream.ProgStreamName;
                ViewData["AcademicYear"] = _context.Bill.Include(b => b.AcadYear).FirstOrDefault(b => b.ProgStreamId == specificStudent.ProgStreamId && b.YearLevelId == 4).AcadYear.Name;
                double billAmount = _context.Bill.FirstOrDefault(b => b.ProgStreamId == specificStudent.ProgStreamId && b.YearLevelId == 4).Amount;
                ViewData["BillAmount"] = billAmount;
                double amountPaid =(double)_context.Payment.Where(p => p.StudentId == specificStudent.Id).Select(p => p.Amount).ToList().Sum();
                ViewData["AmountPaid"] = amountPaid;
                double amountDifference = billAmount - amountPaid;
                double balance;
                double amountOwed;
                if (amountDifference > 1)
                {
                    balance = amountDifference;
                    amountOwed = 0.00;
                }
                else if(amountDifference == 0.00)
                {
                    balance = 0.00;
                    amountOwed = 0.00;
                }
                else
                {
                    amountOwed = amountDifference * -1;
                    balance = 0.00;
                }

                ViewData["Balance"] = balance;
                ViewData["AmountOwed"] = amountOwed;

                return View("BillAvailable"); 
                }
                else if(specificStudentYearLevel == 500)
                {
                if (_context.Bill.FirstOrDefault(b => b.ProgStreamId == specificStudent.ProgStreamId && b.YearLevelId == 5) == null)
                {
                    ViewData["Id"] = id;
                    return View("BillNotAvailable");
                }
                ViewData["Id"] = id;
                ViewData["StudentNumber"] = specificStudent.StudentNumber;
                ViewData["StudentName"] = specificStudent.FullName;
                ViewData["Programme"] = specificStudent.ProgStream.ProgStreamName;
                ViewData["AcademicYear"] = _context.Bill.Include(b => b.AcadYear).FirstOrDefault(b => b.ProgStreamId == specificStudent.ProgStreamId && b.YearLevelId == 5).AcadYear.Name;
                double billAmount = _context.Bill.FirstOrDefault(b => b.ProgStreamId == specificStudent.ProgStreamId && b.YearLevelId == 5).Amount;
                ViewData["BillAmount"] = billAmount;
                double amountPaid =(double)_context.Payment.Where(p => p.StudentId == specificStudent.Id).Select(p => p.Amount).ToList().Sum();
                ViewData["AmountPaid"] = amountPaid;
                double amountDifference = billAmount - amountPaid;
                double balance;
                double amountOwed;
                if (amountDifference > 1)
                {
                    balance = amountDifference;
                    amountOwed = 0.00;
                }
                else if(amountDifference == 0.00)
                {
                    balance = 0.00;
                    amountOwed = 0.00;
                }
                else
                {
                    amountOwed = amountDifference * -1;
                    balance = 0.00;
                }

                ViewData["Balance"] = balance;
                ViewData["AmountOwed"] = amountOwed;

                return View("BillAvailable"); 
                }
                else if(specificStudentYearLevel == 600)
                {
                if (_context.Bill.FirstOrDefault(b => b.ProgStreamId == specificStudent.ProgStreamId && b.YearLevelId == 6) == null)
                {
                    ViewData["Id"] = id;
                    return View("BillNotAvailable");
                }
                ViewData["Id"] = id;
                ViewData["StudentNumber"] = specificStudent.StudentNumber;
                ViewData["StudentName"] = specificStudent.FullName;
                ViewData["Programme"] = specificStudent.ProgStream.ProgStreamName;
                ViewData["AcademicYear"] = _context.Bill.Include(b => b.AcadYear).FirstOrDefault(b => b.ProgStreamId == specificStudent.ProgStreamId && b.YearLevelId == 6).AcadYear.Name;
                double billAmount = _context.Bill.FirstOrDefault(b => b.ProgStreamId == specificStudent.ProgStreamId && b.YearLevelId == 6).Amount;
                ViewData["BillAmount"] = billAmount;
                double amountPaid =(double)_context.Payment.Where(p => p.StudentId == specificStudent.Id).Select(p => p.Amount).ToList().Sum();
                ViewData["AmountPaid"] = amountPaid;
                double amountDifference = billAmount - amountPaid;
                double balance;
                double amountOwed;
                if (amountDifference > 1)
                {
                    balance = amountDifference;
                    amountOwed = 0.00;
                }
                else if(amountDifference == 0.00)
                {
                    balance = 0.00;
                    amountOwed = 0.00;
                }
                else
                {
                    amountOwed = amountDifference * -1;
                    balance = 0.00;
                }

                ViewData["Balance"] = balance;
                ViewData["AmountOwed"] = amountOwed;

                return View("BillAvailable"); 
                }
                else if(specificStudentYearLevel == 700)
                {
                if (_context.Bill.FirstOrDefault(b => b.ProgStreamId == specificStudent.ProgStreamId && b.YearLevelId == 7) == null)
                {
                    ViewData["Id"] = id;
                    return View("BillNotAvailable");
                }
                ViewData["Id"] = id;
                ViewData["StudentNumber"] = specificStudent.StudentNumber;
                ViewData["StudentName"] = specificStudent.FullName;
                ViewData["Programme"] = specificStudent.ProgStream.ProgStreamName;
                ViewData["AcademicYear"] = _context.Bill.Include(b => b.AcadYear).FirstOrDefault(b => b.ProgStreamId == specificStudent.ProgStreamId && b.YearLevelId == 7).AcadYear.Name;
                double billAmount = _context.Bill.FirstOrDefault(b => b.ProgStreamId == specificStudent.ProgStreamId && b.YearLevelId == 7).Amount;
                ViewData["BillAmount"] = billAmount;
                double amountPaid =(double)_context.Payment.Where(p => p.StudentId == specificStudent.Id).Select(p => p.Amount).ToList().Sum();
                ViewData["AmountPaid"] = amountPaid;
                double amountDifference = billAmount - amountPaid;
                double balance;
                double amountOwed;
                if (amountDifference > 1)
                {
                    balance = amountDifference;
                    amountOwed = 0.00;
                }
                else if(amountDifference == 0.00)
                {
                    balance = 0.00;
                    amountOwed = 0.00;
                }
                else
                {
                    amountOwed = amountDifference * -1;
                    balance = 0.00;
                }

                ViewData["Balance"] = balance;
                ViewData["AmountOwed"] = amountOwed;

                return View("BillAvailable"); 
                }
                else
                {
                    return View("ErrorPageForNoCourseRegistration");
                }
            }
            else
            {
                ViewData["Id"] = id;
                return View("BillNotAvailable");
            }
        }


        // GET: Student/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                
                .Include(s => s.Country)
                .Include(s => s.Gender)
                .Include(s => s.MaritalStatus)
                .Include(s => s.ProgStream)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            ViewData["Id"] = id;

            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
    
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name");
            ViewData["GenderId"] = new SelectList(_context.Gender, "Id", "GenderType");
            ViewData["MaritalStatusId"] = new SelectList(_context.MaritalStatus, "Id", "Name");
            ViewData["ProgStreamId"] = new SelectList(_context.ProgStream, "Id", "ProgStreamName");
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,MiddleName,LastName,BirthDate,Email,PhoneNumber,ProgStreamId, CountryId,GenderId,MaritalStatusId")] Student student)
        {
            if (ModelState.IsValid)
            {

                var maxId =
                _context.Student.Select(s => s.Id).ToList().DefaultIfEmpty().Max();
                
                int startingNumber = maxId + 1;
                var studentNumber = GenerateStudentNumber(startingNumber);
                var studentToAdd
                = new Student
                {
                    StudentNumber = studentNumber,
                    FirstName = student.FirstName,
                    MiddleName = student.MiddleName,
                    LastName = student.LastName,
                    BirthDate = student.BirthDate,
                    Email = student.Email,
                    PhoneNumber = student.PhoneNumber,
                    ProgStreamId = student.ProgStreamId,
                    CountryId = student.CountryId,
                    GenderId = student.GenderId,
                    MaritalStatusId = student.MaritalStatusId
                };
                _context.Add(studentToAdd);
                await _context.SaveChangesAsync();
                startingNumber += 1;
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name", student.CountryId);
            ViewData["GenderId"] = new SelectList(_context.Gender, "Id", "GenderType", student.GenderId);
            ViewData["MaritalStatusId"] = new SelectList(_context.MaritalStatus, "Id", "Name", student.MaritalStatusId);
            ViewData["ProgStreamId"] = new SelectList(_context.ProgStream, "Id", "ProgStreamName", student.ProgStreamId);
            return View(student);
        }

        private int GenerateStudentNumber(int number)
        {

            string studentNumber;

            string checkValueToString = number.ToString();

            int checkValueLength = checkValueToString.Length;

            string prefix = "10";
            
            string suffix = "20";

            switch (checkValueLength)
            {
                case 1:
                  studentNumber = $"{prefix}00{checkValueToString}{suffix}";
                  break;

                case 2:
                  studentNumber = $"{prefix}0{checkValueToString}{suffix}";
                  break;
                
                default:
                  studentNumber = $"{prefix}{checkValueToString}{suffix}";
                  break;
            }
            return int.Parse(studentNumber);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student
            .Include(i => i.ProgStream)
            .ThenInclude(i => i.Department)
            .ThenInclude(i => i.Course)
            .Include(i => i.CourseRegistration)
            .FirstOrDefaultAsync(m => m.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name", student.CountryId);
            ViewData["GenderId"] = new SelectList(_context.Gender, "Id", "GenderType", student.GenderId);
            ViewData["MaritalStatusId"] = new SelectList(_context.MaritalStatus, "Id", "Name", student.MaritalStatusId);
            ViewData["ProgStreamId"] = new SelectList(_context.ProgStream, "Id", "ProgStreamName", student.ProgStreamId);
            
    
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {

            var studentToUpdate = await _context.Student
            .Include(i => i.ProgStream)
            .ThenInclude(i => i.Department)
            .ThenInclude(i => i.Course)
            .Include(i => i.CourseRegistration)
            .FirstOrDefaultAsync(m => m.Id == id);


        if (await TryUpdateModelAsync<Student>(
            studentToUpdate,
            "",
            i => i.FirstName, i => i.MiddleName, i => i.LastName, i => i.BirthDate,i => i.Email, i => i.PhoneNumber, i => i.ProgStreamId, i => i.CountryId,i => i.GenderId,i => i.MaritalStatusId))
       {

        // UpdateStudentCourses(selectedCourses, studentToUpdate);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
            }
            return RedirectToAction(nameof(Index));
        }
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name", studentToUpdate.CountryId);
            ViewData["GenderId"] = new SelectList(_context.Gender, "Id", "GenderType", studentToUpdate.GenderId);
            ViewData["MaritalStatusId"] = new SelectList(_context.MaritalStatus, "Id", "Name", studentToUpdate.MaritalStatusId);
            ViewData["ProgStreamId"] = new SelectList(_context.ProgStream, "Id", "ProgStreamName", studentToUpdate.ProgStreamId);
            

            return View(studentToUpdate);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.Country)
                .Include(s => s.Gender)
                .Include(s => s.MaritalStatus)
                .Include(s => s.ProgStream)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Student == null)
            {
                return Problem("Entity set 'SMSContext.Student'  is null.");
            }
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (_context.Student?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
