using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sms.Models
{
    public class CourseRegistration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Student")]
        public int StudentId { get; set; }

        [Display(Name = "Course")]
        public int CourseId { get; set; }

        [Display(Name = "Academic Year")]
        public int? AcadYearId { get; set; }

        [Display(Name = "Semester")]
        public int? SemesterId { get; set; }

        [Display(Name = "Year Level")]
        
        public int? YearLevelId { get; set; }
        public int? Score1 { get; set; }
        public int? Score2 { get; set; }
        public int? TotalScore {
            get
            {
                return Score1 + Score2;
            }
        }
        

        public Student? Student { get; set; }
        public Course? Course { get; set; }

        public Semester? Semester { get; set; }

        public YearLevel? YearLevel { get; set; }

        public AcadYear? AcadYear { get; set; }
    }
}