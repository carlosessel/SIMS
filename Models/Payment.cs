using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sms.Models
{
    public class Payment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Student")]
        public int StudentId { get; set; }
        public double Amount { get; set; }
        [Display(Name = "Payment Type")]
        public int PaymentTypeId { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public Student? Student { get; set; }
        [Display(Name = "Payment Type")]
        public PaymentType? PaymentType { get; set; }
    }
}