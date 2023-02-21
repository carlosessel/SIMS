using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sms.Models.ViewModels
{
    public class ProgStreamViewModel
    {
        public int Id { get; set; }
        public int ProgrammeId { get; set; }
        public int StreamsId { get; set; }

        public int GradingSystemId { get; set; }
        public int DepartmentId { get; set; }

        public ICollection<CourseRegistration>? CourseRegistration{ get; set; }
        public GradingSystem? GradingSystem { get; set; }
        public Programme? Programme { get; set; }
        public Streams? Streams { get; set; }
        public ICollection<Student>? Student { get; set; }
        public Department? Department { get; set; }
    }
}