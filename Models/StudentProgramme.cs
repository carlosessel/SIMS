using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sms.Models
{
    public class StudentProgramme
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ProgStreamId { get; set; }
        public int AcadYearEnrolled { get; set; }
        public int SemEnrolled { get; set; }
        public int AdmissionYearLevelId { get; set; }
        public DateOnly? AdmissionDate { get; set; }
        public int CampusId { get; set; }
    }
}