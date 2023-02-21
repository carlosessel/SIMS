using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace sms.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        [Display(Name = "Programme")]
        public int ProgStreamId { get; set; }
        [Display(Name = "Academic Year")]
        public int AcadYearId { get; set; }
        [Display(Name = "Year Level")]
        public int YearLevelId { get; set; }
        //public int StudentStatusId { get; set; }

        [Display(Name = "Programme")]
        public ProgStream? ProgStream { get; set; }
        [Display(Name = "Academic Year")]
        public AcadYear? AcadYear { get; set; }
        [Display(Name = "Year Level")]
        public YearLevel? YearLevel { get; set; }
        //public StudentStatus? StudentStatus { get; set; }
    }
}