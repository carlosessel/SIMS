using System.ComponentModel;
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
    public class CourseRegistrationsController : Controller
    {
        private readonly SMSContext _context;

        public CourseRegistrationsController(SMSContext context)
        {
            _context = context;
        }

        // GET: CourseScores
       public async Task<IActionResult> CourseScores(int? instructorId, int? courseId)
       {

        var viewModel = new CourseScores();
        viewModel.Instructors = await _context.Instructor
        .Include(i => i.CourseAssignment)
        .ThenInclude(i => i.Course)
        .ThenInclude(i => i.CourseRegistration)
        .ThenInclude(i => i.Student)
        .ThenInclude(i => i.CourseRegistration)
        .ToListAsync();

        ViewData["InstructorList"] = new SelectList(viewModel.Instructors, "Id", "FullName", instructorId);

        if (instructorId != null)
        {
            ViewData["InstructorId"] = instructorId;

            viewModel.Courses = viewModel.Instructors
            .Single(i => i.Id == instructorId)
            .CourseAssignment
            .Select(i => i.Course)
            .ToList();

            ViewData["CourseList"] = new SelectList(viewModel.Courses, "Id", "CourseName", courseId);
        }
        if (courseId != null)
        {
            ViewData["CourseId"] = courseId;
            ViewData["Course"] = _context.Course.Single(c => c.Id == courseId).CourseName;

            viewModel.Students = _context.Course
            .Single(c => c.Id == courseId)
            .CourseRegistration
            .Select(i => i.Student);
            
        }
        
        return View(viewModel);
       }

        // GET: CourseRegistrations
        public async Task<IActionResult> Index(int id, string? filter)
        {
            if(id == null)
            {
                return NotFound();
            }

            if(filter != null)
            {
                 var specificStudentRegistrationDetailsForFilter
                = _context.CourseRegistration
                .Include(c => c.AcadYear).Include(c => c.Course).Include(c => c.Semester).Include(c => c.Student).Include(c => c.YearLevel)
                .Where(s => s.StudentId == id && s.AcadYear.Name == filter);

                ViewData["Id"] = id;
                
                var specificStudentForIdAndFilter
                = await _context.Student
                .Include(i => i.ProgStream)
                .ThenInclude(i => i.Department)
                .ThenInclude(i => i.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

                var studentProgrammeForIdAndFilter
                = specificStudentForIdAndFilter
                .ProgStream
                .ProgStreamName;

                ViewData["Programme"] = studentProgrammeForIdAndFilter;
                ViewData["StudentNumber"] = specificStudentForIdAndFilter.StudentNumber;
                ViewData["StudentName"] = specificStudentForIdAndFilter.FullName;

                ViewData["Filter"]
                = new SelectList(_context.AcadYear, "Name", "Name", filter);


                return View(await specificStudentRegistrationDetailsForFilter.ToListAsync());

            }

            var specificStudentRegistrationDetails
                = _context.CourseRegistration
                .Include(c => c.AcadYear).Include(c => c.Course).Include(c => c.Semester).Include(c => c.Student).Include(c => c.YearLevel)
                .Where(s => s.StudentId == id);

            ViewData["Id"] = id;
                
            var specificStudentForId
                = await _context.Student
                .Include(i => i.ProgStream)
                .ThenInclude(i => i.Department)
                .ThenInclude(i => i.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

            var studentProgrammeForId
                = specificStudentForId
                .ProgStream
                .ProgStreamName;

            ViewData["Programme"] = studentProgrammeForId;
            ViewData["StudentNumber"] = specificStudentForId.StudentNumber;
            ViewData["StudentName"] =  specificStudentForId.FullName;

                
            ViewData["Filter"]
                = new SelectList(_context.AcadYear, "Name", "Name", filter);

            return View(await specificStudentRegistrationDetails.ToListAsync());
        }

        // GET: CourseRegistrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CourseRegistration == null)
            {
                return NotFound();
            }

            var courseRegistration = await _context.CourseRegistration
                .Include(c => c.AcadYear)
                .Include(c => c.Course)
                .Include(c => c.Semester)
                .Include(c => c.Student)
                .Include(c => c.YearLevel)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (courseRegistration == null)
            {
                return NotFound();
            }

            var specificStudent
                = await _context.Student
                .Include(i => i.ProgStream)
                .ThenInclude(i => i.Department)
                .ThenInclude(i => i.Course)
                .FirstOrDefaultAsync(m => m.Id == _context.CourseRegistration.FirstOrDefault(i => i.Id == id).StudentId);

            var studentProgramme
                = specificStudent
                .ProgStream
                .ProgStreamName;

            ViewData["Id"] = specificStudent.Id;
            ViewData["Programme"] = studentProgramme;
            ViewData["StudentNuumber"] = specificStudent.StudentNumber;

            return View(courseRegistration);
        }

        // GET: CourseRegistrations/Create
        public async Task<IActionResult>  Create(int id)
        {
                var specificStudent
                = await _context.Student
                .Include(i => i.ProgStream)
                .ThenInclude(i => i.Department)
                .ThenInclude(i => i.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
               
               ViewData["SpecificStudentId"] = specificStudent.Id;

               var studentCourses
               = specificStudent
               .ProgStream
               .Department
               .Course;

               var studentProgramme
                = specificStudent
                .ProgStream
                .ProgStreamName;

               ViewData["Id"] = id;
               ViewData["StudentName"] = specificStudent.FullName;
               ViewData["StudentNumber"] = specificStudent.StudentNumber;
               ViewData["Programme"] = studentProgramme;

                ViewData["AcadYearId"] = new SelectList(_context.AcadYear, "Id", "Name");
                ViewData["CourseId"] = new SelectList(studentCourses, "Id", "Title");
                // ViewData["ProgStreamId"] = new SelectList(_context.ProgStream, "Id", "ProgStreamName", specificStudent.ProgStreamId);
                ViewData["SemesterId"] = new SelectList(_context.Semester, "Id", "Name");
                //ViewData["StudentId"] = new SelectList(specificStudentList, "Id", "FullName", specificStudent.Id);
                ViewData["YearLevelId"] = new SelectList(_context.YearLevel, "Id", "YearLevelName");
                return View();
        }

        // POST: CourseRegistrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,CourseId,AcadYearId,SemesterId,YearLevelId")] CourseRegistration courseRegistration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseRegistration);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "CourseRegistrations", new{ id = courseRegistration.StudentId });
            }

            ViewData["AcadYearId"] = new SelectList(_context.AcadYear, "Id", "Name", courseRegistration.AcadYearId);
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "CourseName", courseRegistration.CourseId);
            ViewData["SemesterId"] = new SelectList(_context.Semester, "Id", "Name", courseRegistration.SemesterId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FullName", courseRegistration.StudentId);
            ViewData["YearLevelId"] = new SelectList(_context.YearLevel, "Id", "YearLevelName", courseRegistration.YearLevelId);
            return View(courseRegistration);
        }

        // GET: CourseRegistrations/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var courseRegistration = await _context.CourseRegistration.FindAsync(id);
            if (courseRegistration == null)
            {
                return NotFound();
            }

            var specificStudent
                = await _context.Student
                .Include(i => i.ProgStream)
                .ThenInclude(i => i.Department)
                .ThenInclude(i => i.Course)
                .Include(i => i.CourseRegistration)
                .FirstOrDefaultAsync(m => m.Id == _context.CourseRegistration.FirstOrDefault(i => i.Id == id).StudentId);
        
            
            var studentCourses
               = specificStudent
               .ProgStream
               .Department
               .Course;

    
            var studentProgramme
                = specificStudent
                .ProgStream
                .ProgStreamName;

               ViewData["Id"] = specificStudent.Id;
               ViewData["SpecificStudentId"] = specificStudent.Id;
               ViewData["StudentName"] = specificStudent.FullName;
               ViewData["StudentNumber"] = specificStudent.StudentNumber;
               ViewData["Programme"] = studentProgramme;


            ViewData["AcadYearId"] = new SelectList(_context.AcadYear, "Id", "Name", courseRegistration.AcadYearId);
            ViewData["CourseId"] = new SelectList(studentCourses, "Id", "CourseName", courseRegistration.CourseId);
            // ViewData["ProgStreamId"] = new SelectList(_context.ProgStream, "Id", "ProgStreamName", courseRegistration.ProgStreamId);
            ViewData["SemesterId"] = new SelectList(_context.Semester, "Id", "Name", courseRegistration.SemesterId);
            // ViewData["StudentId"] = new SelectList(specificStudentList, "Id", "FullName", courseRegistration.StudentId);
            ViewData["YearLevelId"] = new SelectList(_context.YearLevel, "Id", "YearLevelName", courseRegistration.YearLevelId);
            return View(courseRegistration);
        }


        // Get: EditScore
        public async Task<IActionResult> EditScore(int id, int? instructorId, int? courseId)
        {

            var courseRegistration = await _context.CourseRegistration.FindAsync(id);
            if (courseRegistration == null)
            {
                return NotFound();
            }

            var specificStudent
                = await _context.Student
                .Include(i => i.ProgStream)
                .ThenInclude(i => i.Department)
                .ThenInclude(i => i.Course)
                .Include(i => i.CourseRegistration)
                .FirstOrDefaultAsync(m => m.Id == _context.CourseRegistration.FirstOrDefault(i => i.Id == id).StudentId);
        
            
            var studentCourses
               = specificStudent
               .ProgStream
               .Department
               .Course;

    
            var studentProgramme
                = specificStudent
                .ProgStream
                .ProgStreamName;

               ViewData["Id"] = specificStudent.Id;
               ViewData["SpecificStudentId"] = specificStudent.Id;
               ViewData["StudentName"] = specificStudent.FullName;
               ViewData["StudentNumber"] = specificStudent.StudentNumber;
               ViewData["Programme"] = studentProgramme;

            ViewData["CourseId"] = courseId;
            ViewData["InstructorId"] = instructorId;

            return View(courseRegistration);
        }

        

        // POST: CourseRegistrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,CourseId,AcadYearId,SemesterId,YearLevelId, Score1, Score2")] CourseRegistration courseRegistration)
        {
            if (id != courseRegistration.Id)
            {
                return NotFound();
            }

                if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseRegistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseRegistrationExists(courseRegistration.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Student", new{ id = courseRegistration.StudentId });
            }
            
            ViewData["AcadYearId"] = new SelectList(_context.AcadYear, "Id", "Name", courseRegistration.AcadYearId);
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "CourseName", courseRegistration.CourseId);
            // ViewData["ProgStreamId"] = new SelectList(_context.ProgStream, "Id", "ProgStreamName", courseRegistration.ProgStreamId);
            ViewData["SemesterId"] = new SelectList(_context.Semester, "Id", "Name", courseRegistration.SemesterId);
            //ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FullName", courseRegistration.StudentId);
            ViewData["YearLevelId"] = new SelectList(_context.YearLevel, "Id", "YearLevelName", courseRegistration.YearLevelId);
            
            return View(courseRegistration);
        }

        // Post: EditScore
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditScore(int id, int? courseId, int? instructorId, [Bind("Id,StudentId,CourseId,AcadYearId,SemesterId,YearLevelId, Score1, Score2")] CourseRegistration courseRegistration)
        {
            if (id != courseRegistration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseRegistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseRegistrationExists(courseRegistration.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("CourseScores", "CourseRegistrations", new{instructorId = instructorId, courseId = courseId});
            }
            
            ViewData["AcadYearId"] = new SelectList(_context.AcadYear, "Id", "Name", courseRegistration.AcadYearId);
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "CourseName", courseRegistration.CourseId);
            // ViewData["ProgStreamId"] = new SelectList(_context.ProgStream, "Id", "ProgStreamName", courseRegistration.ProgStreamId);
            ViewData["SemesterId"] = new SelectList(_context.Semester, "Id", "Name", courseRegistration.SemesterId);
            //ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FullName", courseRegistration.StudentId);
            ViewData["YearLevelId"] = new SelectList(_context.YearLevel, "Id", "YearLevelName", courseRegistration.YearLevelId);
            
            return View(courseRegistration);
            
        }

        // GET: CourseRegistrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CourseRegistration == null)
            {
                return NotFound();
            }

            var courseRegistration = await _context.CourseRegistration
                .Include(c => c.AcadYear)
                .Include(c => c.Course)
                .Include(c => c.Semester)
                .Include(c => c.Student)
                .Include(c => c.YearLevel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseRegistration == null)
            {
                return NotFound();
            }
            var specificStudent
                = await _context.Student
                .Include(i => i.ProgStream)
                .ThenInclude(i => i.Department)
                .ThenInclude(i => i.Course)
                .FirstOrDefaultAsync(m => m.Id == _context.CourseRegistration.FirstOrDefault(i => i.Id == id).StudentId);

            var studentProgramme
                = specificStudent
                .ProgStream
                .ProgStreamName;

            ViewData["Id"] = specificStudent.Id;
            ViewData["Programme"] = studentProgramme;
            ViewData["StudentNumber"] = specificStudent.StudentNumber;

            return View(courseRegistration);
        }

        // POST: CourseRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CourseRegistration == null)
            {
                return Problem("Entity set 'SMSContext.CourseRegistration'  is null.");
            }
            var courseRegistration = await _context.CourseRegistration.FindAsync(id);
            if (courseRegistration != null)
            {
                _context.CourseRegistration.Remove(courseRegistration);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "CourseRegistrations", new{ id = _context.CourseRegistration.FirstOrDefault(i => i.Id == id).StudentId });
        }

        private bool CourseRegistrationExists(int id)
        {
          return (_context.CourseRegistration?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
