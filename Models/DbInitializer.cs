using System;
using System.Linq;

namespace CMCS.Models
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Seed Users
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { Username = "lecturer1", Password = "pass123", Role = "Lecturer", Email = "lecturer1@cmcs.com", HourlyRate = 300 },
                    new User { Username = "lecturer2", Password = "pass123", Role = "Lecturer", Email = "lecturer2@cmcs.com", HourlyRate = 280 },
                    new User { Username = "lecturer3", Password = "pass123", Role = "Lecturer", Email = "lecturer3@cmcs.com", HourlyRate = 350 },
                    new User { Username = "coord1", Password = "pass123", Role = "Coordinator", Email = "coord1@cmcs.com" },
                    new User { Username = "manager1", Password = "pass123", Role = "Manager", Email = "manager1@cmcs.com" },
                    new User { Username = "hr1", Password = "pass123", Role = "HR", Email = "hr1@cmcs.com" }
                );
                context.SaveChanges();
            }

            // Seed Claims
            if (!context.Claims.Any())
            {
                context.Claims.AddRange(
                    new Claim { LecturerName = "lecturer1", HoursWorked = 20, HourlyRate = 300, TotalPay = 6000, Status = "Pending", SubmittedDate = DateTime.Now.AddDays(-6) },
                    new Claim { LecturerName = "lecturer1", HoursWorked = 18, HourlyRate = 300, TotalPay = 5400, Status = "Approved", SubmittedDate = DateTime.Now.AddDays(-3) },
                    new Claim { LecturerName = "lecturer2", HoursWorked = 25, HourlyRate = 280, TotalPay = 7000, Status = "Pending", SubmittedDate = DateTime.Now.AddDays(-5) },
                    new Claim { LecturerName = "lecturer2", HoursWorked = 10, HourlyRate = 280, TotalPay = 2800, Status = "Approved", SubmittedDate = DateTime.Now.AddDays(-2) },
                    new Claim { LecturerName = "lecturer3", HoursWorked = 15, HourlyRate = 350, TotalPay = 5250, Status = "Rejected", SubmittedDate = DateTime.Now.AddDays(-4) },
                    new Claim { LecturerName = "lecturer3", HoursWorked = 12, HourlyRate = 350, TotalPay = 4200, Status = "Approved", SubmittedDate = DateTime.Now.AddDays(-1) }
                );
                context.SaveChanges();
            }
        }
    }
}
