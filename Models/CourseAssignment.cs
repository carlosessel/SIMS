using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sms.Models
{
    public class CourseAssignment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Instructor")]
        public int InstructorId { get; set; }

        [Display(Name = "Course")]
        public int CourseId { get; set; }

        [Display(Name = "Academic Year")]
        public int AcadYearId { get; set; }

        [Display(Name = "Semester")]
        public int SemesterId { get; set; }

        public Instructor? Instructor { get; set; }

        public Course? Course { get; set; }

        [Display(Name = "Acadmic Year")]
        public AcadYear? AcadYear { get; set; }
        
        public Semester? Semester { get; set; }
    }
}