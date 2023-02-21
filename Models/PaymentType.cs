using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sms.Models
{
    public class PaymentType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Payment>? Payment { get; set; }
    }
}