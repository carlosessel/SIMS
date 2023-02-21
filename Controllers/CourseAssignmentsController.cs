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
    public class CourseAssignmentsController : Controller
    {
        private readonly SMSContext _context;

        public CourseAssignmentsController(SMSContext context)
        {
            _context = context;
        }

        // GET: CourseAssignments
        public async Task<IActionResult> Index(int id, string? filter)
        {
            if (filter != null)
            {
            var specificInstructorAssignmentDetailsForFilter = 
            _context.CourseAssignment
            .Include(c => c.AcadYear)
            .Include(c => c.Course)
            .Include(c => c.Instructor)
            .Include(c => c.Semester)
            .Where(i => i.InstructorId == id && i.AcadYear.Name == filter);


            ViewData["Id"] = id;
                
            var specificInstructorForFilter
                = await _context.Instructor
                .Include(i => i.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            
             ViewData["InstructorName"] =  specificInstructorForFilter.FullName;
             ViewData["InstructorDepartment"] = specificInstructorForFilter.Department.Name;

             ViewData["Filter"]
             = new SelectList(_context.AcadYear, "Name", "Name", filter);

            return View(await specificInstructorAssignmentDetailsForFilter.ToListAsync());
            }
            
            var specificInstructorAssignmentDetails = _context.CourseAssignment
            .Include(c => c.AcadYear)
            .Include(c => c.Course)
            .Include(c => c.Instructor)
            .Include(c => c.Semester)
            .Where(i => i.InstructorId == id);


            ViewData["Id"] = id;
                
            var specificInstructor
                = await _context.Instructor
                .Include(i => i.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            
             ViewData["InstructorName"] =  specificInstructor.FullName;
             ViewData["InstructorDepartment"] = specificInstructor.Department.Name;

            return View(await specificInstructorAssignmentDetails.ToListAsync());

        }

        // GET: CourseAssignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CourseAssignment == null)
            {
                return NotFound();
            }

            var courseAssignment = await _context.CourseAssignment
                .Include(c => c.AcadYear)
                .Include(c => c.Course)
                .Include(c => c.Instructor)
                .Include(c => c.Semester)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseAssignment == null)
            {
                return NotFound();
            }
            
            var specificInstructor
            = _context.Instructor
            .Include(i => i.Department)
            .Include(i => i.CourseAssignment)
            .Single(i => i.Id == _context.CourseAssignment.Single(c => c.Id == id).InstructorId);

            ViewData["Id"] = specificInstructor.Id;
             ViewData["InstructorName"] =  specificInstructor.FullName;
             ViewData["InstructorDepartment"] = specificInstructor.Department.Name;

            return View(courseAssignment);
        }

        // GET: CourseAssignments/Create
        public async Task<IActionResult> Create(int id)
        {
            var specificInstructor
            = _context.Instructor
            .Include(i => i.Department)
            .ThenInclude(i => i.Course)
            .Include(i => i.CourseAssignment)
            .Single(i => i.Id == id);

            var instructorCourses 
            = specificInstructor
            .Department
            .Course;

            ViewData["Id"] = id;
            ViewData["SpecificInstructorId"] = specificInstructor.Id;
             ViewData["InstructorName"] =  specificInstructor.FullName;
             ViewData["InstructorDepartment"] = specificInstructor.Department.Name;

            ViewData["AcadYearId"] = new SelectList(_context.AcadYear, "Id", "Name");
            ViewData["CourseId"] = new SelectList(instructorCourses, "Id", "CourseName");
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "Id", "FullName", specificInstructor.Id);
            ViewData["SemesterId"] = new SelectList(_context.Semester, "Id", "Name");
            return View();
        }

        // POST: CourseAssignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InstructorId,CourseId,AcadYearId,SemesterId")] CourseAssignment courseAssignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseAssignment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "CourseAssignments", new{ id = courseAssignment.InstructorId });
            }
            ViewData["AcadYearId"] = new SelectList(_context.AcadYear, "Id", "Name", courseAssignment.AcadYearId);
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "CourseName", courseAssignment.CourseId);
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "Id", "FullName", courseAssignment.InstructorId);
            ViewData["SemesterId"] = new SelectList(_context.Semester, "Id", "Name", courseAssignment.SemesterId);
            return View(courseAssignment);
        }

        // GET: CourseAssignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CourseAssignment == null)
            {
                return NotFound();
            }

            var courseAssignment = await _context.CourseAssignment.FindAsync(id);
            if (courseAssignment == null)
            {
                return NotFound();
            }

            var specificInstructor
            = _context.Instructor
            .Include(i => i.Department)
            .ThenInclude(i => i.Course)
            .Include(i => i.CourseAssignment)
            .Single(i => i.Id == _context.CourseAssignment.Single(c => c.Id == id).InstructorId);

            var instructorCourses
            = specificInstructor
            .Department
            .Course;

            ViewData["Id"] = specificInstructor.Id;
             ViewData["InstructorName"] =  specificInstructor.FullName;
             ViewData["InstructorDepartment"] = specificInstructor.Department.Name;

            ViewData["AcadYearId"] = new SelectList(_context.AcadYear, "Id", "Name", courseAssignment.AcadYearId);
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "CourseName", courseAssignment.CourseId);
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "Id", "FullName", courseAssignment.InstructorId);
            ViewData["SemesterId"] = new SelectList(_context.Semester, "Id", "Name", courseAssignment.SemesterId);
            return View(courseAssignment);
        }

        // POST: CourseAssignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InstructorId,CourseId,AcadYearId,SemesterId")] CourseAssignment courseAssignment)
        {
            if (id != courseAssignment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseAssignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseAssignmentExists(courseAssignment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "CourseAssignments", new{ id = courseAssignment.InstructorId });
            }
            ViewData["AcadYearId"] = new SelectList(_context.AcadYear, "Id", "Name", courseAssignment.AcadYearId);
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "CourseName", courseAssignment.CourseId);
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "Id", "FullName", courseAssignment.InstructorId);
            ViewData["SemesterId"] = new SelectList(_context.Semester, "Id", "Name", courseAssignment.SemesterId);
            return View(courseAssignment);
        }

        // GET: CourseAssignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CourseAssignment == null)
            {
                return NotFound();
            }

            var courseAssignment = await _context.CourseAssignment
                .Include(c => c.AcadYear)
                .Include(c => c.Course)
                .Include(c => c.Instructor)
                .Include(c => c.Semester)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseAssignment == null)
            {
                return NotFound();
            }

            var specificInstructor
            = _context.Instructor
            .Include(i => i.Department)
            .Include(i => i.CourseAssignment)
            .Single(i => i.Id == _context.CourseAssignment.Single(c => c.Id == id).InstructorId);

            ViewData["Id"] = specificInstructor.Id;
             ViewData["InstructorDepartment"] = specificInstructor.Department.Name;

            return View(courseAssignment);
        }

        // POST: CourseAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CourseAssignment == null)
            {
                return Problem("Entity set 'SMSContext.CourseAssignment'  is null.");
            }
            var courseAssignment = await _context.CourseAssignment.FindAsync(id);
            if (courseAssignment != null)
            {
                _context.CourseAssignment.Remove(courseAssignment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "CourseAssignments", new{id =  _context.CourseAssignment.Single(c => c.Id == id).InstructorId});
        }

        private bool CourseAssignmentExists(int id)
        {
          return (_context.CourseAssignment?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
