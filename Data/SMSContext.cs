using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sms.Models;

namespace SMS.Data
{
    public class SMSContext : DbContext
    {
        public SMSContext (DbContextOptions<SMSContext> options)
            : base(options)
        {
        }

        public DbSet<sms.Models.Student> Student { get; set; } = default!;
        public DbSet<sms.Models.Country> Country { get; set; } = default!;
        public DbSet<sms.Models.Gender> Gender { get; set; } = default!;
        public DbSet<sms.Models.MaritalStatus> MaritalStatus { get; set; } = default!;
        public DbSet<sms.Models.Address> Address { get; set; } = default!;
        public DbSet<sms.Models.Course> Course { get; set; } = default!;
        public DbSet<sms.Models.Streams> Streams { get; set; } = default!;
        public DbSet<sms.Models.Programme> Programme { get; set; } = default!;
        public DbSet<sms.Models.ProgStream> ProgStream { get; set; } = default!;
        public DbSet<sms.Models.GradingSystem> GradingSystem { get; set; } = default!;
        public DbSet<sms.Models.Department> Department { get; set; } = default!;
        public DbSet<sms.Models.YearLevel> YearLevel { get; set; } = default!;
        public DbSet<sms.Models.Semester> Semester { get; set; } = default!;
        public DbSet<sms.Models.CourseRegistration> CourseRegistration { get; set; } = default!;
        public DbSet<sms.Models.AcadYear> AcadYear { get; set; } = default!;
        public DbSet<sms.Models.Payment> Payment { get; set; } = default!;
        public DbSet<sms.Models.PaymentType> PaymentType { get; set; } = default!;
        public DbSet<sms.Models.Bill> Bill { get; set; } = default!;
        public DbSet<sms.Models.StudentStatus> StudentStatus { get; set; } = default!;
        public DbSet<sms.Models.Instructor> Instructor { get; set; } = default!;
        public DbSet<sms.Models.CourseAssignment> CourseAssignment { get; set; } = default!;
        
        
    }
}
