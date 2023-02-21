using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sms.Models
{
    public class GradingSystem
    {
        public int Id { get; set; }

        public string? Name { get; set; }


        public ICollection<ProgStream>? ProgStream { get; set; }
    }
}