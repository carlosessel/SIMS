using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sms.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string? ResidenceAddress { get; set; }
        public string? PostalAddress { get; set; }
        public string? HomeAddress { get; set; }
    }
}