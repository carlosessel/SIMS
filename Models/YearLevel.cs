using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sms.Models
{
    public class YearLevel
    {
        public int Id { get; set; }
        public string? YearLevelName { get; set; }

        public ICollection<CourseRegistration>? CourseRegistration { get; set; }
        public ICollection<Bill>? Bill { get; set; }
    }
}