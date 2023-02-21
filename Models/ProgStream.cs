using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sms.Models
{
    public class ProgStream
    {
        public int Id { get; set; }
        [Display(Name = "Programe")]
        public int ProgrammeId { get; set; }
        [Display(Name = "Stream")]
        public int StreamsId { get; set; }
        [Display(Name = "Programme Stream Name")]
        public string? ProgStreamName  { get; set; }
        [Display(Name = "Grading System")]
        public int GradingSystemId { get; set; }
        public int DepartmentId { get; set; }

        public GradingSystem? GradingSystem { get; set; }
        public Programme? Programme { get; set; }
        [Display(Name = "Stream")]
        public Streams? Streams { get; set; }
        public ICollection<Student>? Student { get; set; }
        public Department? Department { get; set; }
        public ICollection<Bill>? Bill { get; set; }
    }
}