using Microsoft.AspNetCore.Mvc;
using CMCS.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CMCS.Controllers
{
    public class LecturerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public LecturerController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        // NEW ACTION

        public IActionResult Dashboard()
        {
            var claims = _context.Claims.ToList();
            return View(claims);
        }

        // GET: Submit Claim
        public IActionResult SubmitClaim()
        {
            return View();
        }

        // POST: Submit Claim
        [HttpPost]
        public async Task<IActionResult> SubmitClaim(
     [Bind("LecturerName,HoursWorked,HourlyRate")] Claim claim,
     IFormFile? upload)
        {
            if (!ModelState.IsValid)
                return View(claim);

            claim.Status = "Pending";
            claim.SubmittedDate = DateTime.Now;
            claim.TotalPay = claim.HoursWorked * claim.HourlyRate;

            // ---------------- SAFE FILE VALIDATION ----------------
            if (upload != null && upload.Length > 0)
            {
                // Allowed extensions
                string[] allowedExtensions = { ".pdf", ".docx", ".xlsx" };

                var ext = Path.GetExtension(upload.FileName).ToLower();

                if (!allowedExtensions.Contains(ext))
                {
                    ModelState.AddModelError("", "Invalid file type. Only PDF, DOCX, and XLSX are allowed.");
                    return View(claim);
                }

                // Max allowed size (5 MB)
                long maxSizeBytes = 5 * 1024 * 1024;

                if (upload.Length > maxSizeBytes)
                {
                    ModelState.AddModelError("", "File too large. Maximum size is 5 MB.");
                    return View(claim);
                }

                // Save the file
                var folder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var fileName = Guid.NewGuid() + "_" + upload.FileName;
                var path = Path.Combine(folder, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await upload.CopyToAsync(stream);
                }

                claim.DocumentPath = "/uploads/" + fileName;
            }
            

            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Claim submitted successfully!";
            return RedirectToAction(nameof(SubmitClaim));
        }
    }
}
