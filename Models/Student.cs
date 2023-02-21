using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sms.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Display(Name = "Student Number")]
        
        public int StudentNumber { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string? MiddleName { get; set; }
        [Display(Name = "Last Name")]
        
        public string LastName { get; set; }
        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return LastName + " " + MiddleName + " " +  FirstName;
            }
        }
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string? Email { get; set; }
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Programme")]
        public int? ProgStreamId { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }
        [Display(Name = "Gender")]
        public int GenderId { get; set; }
        [Display(Name = "Marital Status")]
        public int MaritalStatusId { get; set; }        

        // public int BirthCountryId { get; set; }
        // public int HomeCountryId { get; set; }
        
        
        
        

        public Country? Country { get; set; }
        // public Address? Address { get; set; }
        public Gender? Gender { get; set; }

        public MaritalStatus? MaritalStatus { get; set; }
        [Display(Name = "Programme")]
        public ProgStream? ProgStream { get; set; }

        public ICollection<CourseRegistration>? CourseRegistration { get; set; }
        public ICollection<Payment>? Payment { get; set; }

    }
}