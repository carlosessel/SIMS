using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

namespace sms.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string Title { get; set; }
        public string CourseName
        {
            get
            {
                return CourseCode + " " + Title;
            }
        }
        public int Credits { get; set; }
        public int DepartmentID { get; set; } 

        public ICollection<CourseRegistration>? CourseRegistration { get; set; }
        public Department? Department { get; set; }
        public ICollection<CourseAssignment>? CourseAssignment { get; set; }
    }
}