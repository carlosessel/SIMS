using System.Threading;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using sms.Models;
using SMS.Data;
using System;
using System.Linq;

namespace SMS.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new SMSContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<SMSContext>>()))
        {
            // Look for any Country
            if (!context.Country.Any())

            {
                var countries = new Country[]{
                new Country{
                    Id=1,
                    CountryCode = "GHA",
                    Name="Ghana",
                    Nationality="Ghana"

                },
                new Country{
                    Id=2,
                    CountryCode = "NGA",
                    Name="Nigeria",
                    Nationality="Nigeria"
                }
            };

            foreach (var country in countries){
                    context.Country.Add(country);
                }
            }

            context.SaveChanges();

            if(!context.Gender.Any()){
                var genders = new Gender[]{
                    new Gender{
                        Id=1,
                        GenderType="Male"
                    },
                    new Gender{
                        Id=2,
                        GenderType="Female"
                    }
                };

                foreach (var gender in genders){
                    context.Gender.Add(gender);
                }
            }

            context.SaveChanges();

            if(!context.MaritalStatus.Any()){
                var maritalStatuses = new MaritalStatus[]{
                    new MaritalStatus{
                        Id=1,
                        Name="Single"
                    },
                    new MaritalStatus{
                        Id=2,
                        Name="Married"
                    },
                };

                foreach (var maritalStatus in maritalStatuses){
                    context.MaritalStatus.Add(maritalStatus);
                } 
            }

            context.SaveChanges();

            if(!context.Streams.Any()){
                var streamss = new Streams[]{
                    new Streams{
                        Id = 1,
                        Name = "Regular"
                    },
                    new Streams{
                        Id = 2,
                        Name = "Parallel"
                    },
                    new Streams{
                        Id = 3,
                        Name = "Distance"
                    }
                };

                foreach (var stream in streamss){
                    context.Streams.Add(stream);
                } 
            }

            context.SaveChanges();

            if(!context.Programme.Any()){
                var programmes = new Programme[]{
                    new Programme{
                        Id = 1,
                        Name = "BSC Computer Science"
                    },
                    new Programme{
                        Id = 2,
                        Name = "BSC Computer Engineering"
                    },
                    new Programme{
                        Id = 3,
                        Name = "BSC Mathematics"
                    }
                };

                foreach (var programme in programmes){
                    context.Programme.Add(programme);
                } 
            };

            context.SaveChanges();

            if(!context.GradingSystem.Any())
            {
                var gradingSystems = new GradingSystem[]
                {
                    new GradingSystem
                    {
                        Id = 1,
                        Name = "KNUST CWA"
                    },
                    new GradingSystem
                    {
                        Id = 2,
                        Name = "UG GPA"
                    }
                };

                foreach (var gradingSystem in gradingSystems)
                {
                    context.GradingSystem.Add(gradingSystem);
                } 
            };

            context.SaveChanges();

            if(!context.Department.Any()){
                var departments = new Department[]{
                    new Department
                    {
                        Id = 1,
                        Name = "Department of Computer Science",
                    },
                    new Department
                    {
                        Id = 2,
                        Name = "Department of Mathematics"
                    },
                    new Department
                    {
                        Id = 3,
                        Name = "Department of Computer Engineering"
                    }
                };

                foreach (var department in departments){
                    context.Department.Add(department);
                } 
            };

            context.SaveChanges();

            if(!context.ProgStream.Any())
            {
                var progStreams = new ProgStream[]
                {
                    new ProgStream
                    {
                        Id = 1,
                        ProgrammeId = context.Programme.Single(p => p.Name == "BSC Computer Science").Id,
                        StreamsId = context.Streams.Single(s => s.Name == "Regular").Id,
                        ProgStreamName = "BSC Computer Science",
                        GradingSystemId = context.GradingSystem.Single(g => g.Name == "KNUST CWA").Id,                        
                        DepartmentId = context.Department.Single(d => d.Name == "Department of Computer Science").Id
                    },
                    new ProgStream
                    {
                        Id = 2,
                        ProgrammeId = context.Programme.Single(p => p.Name == "BSC Computer Science").Id,
                        StreamsId = context.Streams.Single(s => s.Name == "Parallel").Id,
                        ProgStreamName = "BSC Computer Science Parallel",
                        GradingSystemId = context.GradingSystem.Single(g => g.Name == "UG GPA").Id,                        
                        DepartmentId = context.Department.Single(d => d.Name == "Department of Computer Science").Id
                    },
                    new ProgStream
                    {
                        Id = 3,
                        ProgrammeId = context.Programme.Single(p => p.Name == "BSC Computer Science").Id,
                        StreamsId = context.Streams.Single(s => s.Name == "Distance").Id,
                        ProgStreamName = "BSC Computer Science Distance",
                        GradingSystemId = context.GradingSystem.Single(g => g.Name == "KNUST CWA").Id,                        
                        DepartmentId = context.Department.Single(d => d.Name == "Department of Computer Science").Id
                    },
                    new ProgStream
                    {
                        Id = 4,
                        ProgrammeId = context.Programme.Single(p => p.Name == "BSC Mathematics").Id,
                        StreamsId = context.Streams.Single(s => s.Name == "Regular").Id,
                        ProgStreamName = "BSC Mathematics",
                        GradingSystemId = context.GradingSystem.Single(g => g.Name == "KNUST CWA").Id,                        
                        DepartmentId = context.Department.Single(d => d.Name == "Department of Mathematics").Id
                    },
                    new ProgStream
                    {
                        Id = 5,
                        ProgrammeId = context.Programme.Single(p => p.Name == "BSC Mathematics").Id,
                        StreamsId = context.Streams.Single(s => s.Name == "Parallel").Id,
                        ProgStreamName = "BSC Mathematics Parallel",
                        GradingSystemId = context.GradingSystem.Single(g => g.Name == "UG GPA").Id,                        
                        DepartmentId = context.Department.Single(d => d.Name == "Department of Mathematics").Id
                    },
                    new ProgStream
                    {
                        Id = 6,
                        ProgrammeId = context.Programme.Single(p => p.Name == "BSC Mathematics").Id,
                        StreamsId = context.Streams.Single(s => s.Name == "Distance").Id,
                        ProgStreamName = "BSC Mathematics Distance",
                        GradingSystemId = context.GradingSystem.Single(g => g.Name == "KNUST CWA").Id,                        
                        DepartmentId = context.Department.Single(d => d.Name == "Department of Mathematics").Id
                    }
                };

                foreach (var progStream in progStreams)
                {
                    context.ProgStream.Add(progStream);
                } 
            };

            context.SaveChanges();

            if(!context.Course.Any()){
                var courses = new Course[]{
                    new Course{
                        Id = 1,
                        CourseCode = "CSM 111",
                        Title = "Structure Programme Design",
                        Credits = 3,
                        DepartmentID = context.Department.Single(d=>d.Name == "Department of Computer Science").Id
                    },
                    new Course{
                        Id = 2,
                        CourseCode = "CSM 112",
                        Title = "Pure Mathematics",
                        Credits = 3,
                        DepartmentID = context.Department.Single(d=>d.Name == "Department of Computer Science").Id
                    },
                    new Course{
                        Id = 3,
                        CourseCode = "CSM 113",
                        Title = "Discrete Mathematics",
                        Credits = 3,
                        DepartmentID = context.Department.Single(d=>d.Name == "Department of Computer Science").Id
                    },
                    new Course{
                        Id = 4,
                        CourseCode = "CSM 211",
                        Title = "Electronics",
                        Credits = 2,
                        DepartmentID = context.Department.Single(d=>d.Name == "Department of Computer Science").Id
                    },
                    new Course{
                        Id = 5,
                        CourseCode = "CSM 212",
                        Title = "C++",
                        Credits = 3,
                        DepartmentID = context.Department.Single(d=>d.Name == "Department of Computer Science").Id
                    },
                    new Course{
                        Id = 6,
                        CourseCode = "CSM 213",
                        Title = "System Analysis",
                        Credits = 3,
                        DepartmentID = context.Department.Single(d=>d.Name == "Department of Computer Science").Id
                    },
                    new Course{
                        Id = 7,
                        CourseCode = "Maths 111",
                        Title = "General Maths",
                        Credits = 3,
                        DepartmentID = context.Department.Single(d=>d.Name == "Department of Mathematics").Id
                    },
                    new Course{
                        Id = 8,
                        CourseCode = "Maths 112",
                        Title = "Maths Lab",
                        Credits = 3,
                        DepartmentID = context.Department.Single(d=>d.Name == "Department of Mathematics").Id
                    },
                    new Course{
                        Id = 9,
                        CourseCode = "Maths 211",
                        Title = "Advanced Maths",
                        Credits = 3,
                        DepartmentID = context.Department.Single(d=>d.Name == "Department of Mathematics").Id
                    },
                    new Course{
                        Id = 10,
                        CourseCode = "Maths 212",
                        Title = "Applied Maths",
                        Credits = 3,
                        DepartmentID = context.Department.Single(d=>d.Name == "Department of Mathematics").Id
                    },
                    new Course{
                        Id = 11,
                        CourseCode = "CEM 111",
                        Title = "Electronics",
                        Credits = 3,
                        DepartmentID = context.Department.Single(d=>d.Name == "Department of Computer Engineering").Id
                    },
                    new Course{
                        Id = 12,
                        CourseCode = "CEM 112",
                        Title = "Python",
                        Credits = 3,
                        DepartmentID = context.Department.Single(d=>d.Name == "Department of Computer Engineering").Id
                    },
                    new Course{
                        Id = 13,
                        CourseCode = "CEM 211",
                        Title = "Maths Lab",
                        Credits = 2,
                        DepartmentID = context.Department.Single(d=>d.Name == "Department of Computer Engineering").Id
                    },
                    new Course{
                        Id = 14,
                        CourseCode = "CEM 212",
                        Title = "Circuit Engineering ",
                        Credits = 3,
                        DepartmentID = context.Department.Single(d=>d.Name == "Department of Computer Engineering").Id
                    },
                };

                foreach (var course in courses){
                    context.Course.Add(course);
                } 
            }

            context.SaveChanges();

            if(!context.YearLevel.Any())
            {
                var yearLevels = new YearLevel[]
                {
                    new YearLevel
                    {
                        Id = 1,
                        YearLevelName = "100"
                    },
                    new YearLevel
                    {
                        Id = 2,
                        YearLevelName = "200"
                    },
                    new YearLevel
                    {
                        Id = 3,
                        YearLevelName = "300"
                    },
                    new YearLevel
                    {
                        Id = 4,
                        YearLevelName = "400"
                    },
                    new YearLevel
                    {
                        Id = 5,
                        YearLevelName = "500"
                    },
                    new YearLevel
                    {
                        Id = 6,
                        YearLevelName = "600"
                    },
                    new YearLevel
                    {
                        Id = 7,
                        YearLevelName = "700"
                    }
                };

                foreach(var yearLevel in yearLevels)
                {
                    context.YearLevel.Add(yearLevel);
                }
            }
            context.SaveChanges();

            if(!context.Semester.Any())
            {
                var semesters = new Semester[]
                {
                    new Semester
                    {
                        Id = 1,
                        Name = "First Semester"
                    },
                    new Semester
                    {
                        Id = 2,
                        Name = "Second Semester"
                    }
                };
                foreach(var semester in semesters)
                {
                    context.Semester.Add(semester);
                }
            }
            context.SaveChanges();

            if (!context.AcadYear.Any())
            {
                var acadYears = new AcadYear[]
                {
                    new AcadYear
                    {
                        Id = 1,
                        Name = "2022/2023"
                    },
                    new AcadYear
                    {
                        Id = 2,
                        Name = "2023/2024"
                    }
                };

                foreach(var acadYear in acadYears)
                {
                    context.AcadYear.Add(acadYear);
                }
            }

            context.SaveChanges();

            if(!context.PaymentType.Any())
            {
                var paymentTypes = new PaymentType[]
                {
                    new PaymentType
                    {
                        Id = 1,
                        Name = "Bank"
                    },
                    new PaymentType
                    {
                        Id = 2,
                        Name = "Mobile Money"
                    }
                };
                foreach (var paymentType in paymentTypes)
                {
                    context.PaymentType.Add(paymentType);
                }
            }
            context.SaveChanges();

            if(!context.StudentStatus.Any())
            {
                var studentStatuss = new StudentStatus[]
                {
                    new StudentStatus
                    {
                        Id = 1,
                        Name = "Freshers"
                    },
                    new StudentStatus
                    {
                        Id = 2,
                        Name = "Continuing Students"
                    }
                };
                foreach (var studentStatus in studentStatuss)
                {
                    context.StudentStatus.Add(studentStatus);
                }
            }
            context.SaveChanges();
        }    
    }
}
