using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sms.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public ICollection<ProgStream>? ProgStream { get; set; }
        public ICollection<Course>? Course { get; set; }

        public ICollection<Instructor>? Instructor { get; set; }

    }
}