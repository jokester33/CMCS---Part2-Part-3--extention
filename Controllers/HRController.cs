using Microsoft.AspNetCore.Mvc;
using CMCS.Models;
using System.Linq;
using System.Text;
using CMCS.Models;


namespace CMCS.Controllers
{
    public class HRController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HRController(ApplicationDbContext context)
        {
            _context = context;
        }



        // HR Dashboard – view all approved claims
        public IActionResult Dashboard()
        {
            var approvedClaims = _context.Claims
                .Where(c => c.Status == "Approved")
                .OrderBy(c => c.LecturerName)
                .ToList();

            // summary by lecturer using LINQ
            var summary = approvedClaims
                .GroupBy(c => c.LecturerName)
                .Select(g => new HrSummaryViewModel
                {
                    LecturerName = g.Key,
                    TotalClaims = g.Count(),
                    TotalHours = g.Sum(x => x.HoursWorked),
                    TotalPaid = g.Sum(x => x.TotalPay)
                })
                .ToList();

            var model = new HrDashboardViewModel
            {
                Claims = approvedClaims,
                Summary = summary
            };

            return View(model);
        }

        // export CSV report of approved claims
        public IActionResult ExportReport()
        {
            var claims = _context.Claims
                .Where(c => c.Status == "Approved")
                .OrderBy(c => c.LecturerName)
                .ToList();

            var sb = new StringBuilder();
            sb.AppendLine("LecturerName,SubmittedDate,HoursWorked,HourlyRate,TotalPay,Status");

            foreach (var c in claims)
                sb.AppendLine($"{c.LecturerName},{c.SubmittedDate:yyyy-MM-dd},{c.HoursWorked},{c.HourlyRate},{c.TotalPay},{c.Status}");

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv", "ApprovedClaimsReport.csv");
        }

        // GET: Edit Lecturer Data
        public IActionResult EditLecturer(int id)
        {
            var lecturer = _context.Users.FirstOrDefault(u => u.Id == id && u.Role == "Lecturer");
            if (lecturer == null) return NotFound();
            return View(lecturer);
        }

        // POST: Save Lecturer Data
        [HttpPost]
        public IActionResult EditLecturer(User lecturer)
        {
            if (ModelState.IsValid)
            {
                var existing = _context.Users.FirstOrDefault(u => u.Id == lecturer.Id);
                if (existing != null)
                {
                    existing.Username = lecturer.Username;
                    existing.Password = lecturer.Password;
                    existing.HourlyRate = lecturer.HourlyRate;
                    existing.Email = lecturer.Email;
                    _context.Update(existing);
                    _context.SaveChanges();
                    TempData["Success"] = "Lecturer details updated successfully!";
                    return RedirectToAction(nameof(Dashboard));
                }
            }
            return View(lecturer);
        }
    }
}
