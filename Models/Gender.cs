using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sms.Models
{
    public class Gender
    {
        public int Id { get; set; }
        public string? GenderType { get; set; }

        public ICollection<Student>? Student { get; set; }
    }
}