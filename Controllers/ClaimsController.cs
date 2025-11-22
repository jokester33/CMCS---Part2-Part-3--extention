using Microsoft.AspNetCore.Mvc;
using CMCS.Models;

namespace CMCS.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ClaimsController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public IActionResult SubmitClaim()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitClaim(Claim claim, IFormFile? upload)
        {
            if (ModelState.IsValid)
            {
                claim.Status = "Pending";
                claim.SubmittedDate = DateTime.Now;
                claim.TotalPay = claim.HoursWorked * claim.HourlyRate;

                if (upload != null && upload.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid() + "_" + Path.GetFileName(upload.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await upload.CopyToAsync(fileStream);
                    }

                    var doc = new SupportingDocument
                    {
                        FileName = upload.FileName,
                        FilePath = "/uploads/" + uniqueFileName,
                        Claim = claim
                    };

                    _db.SupportingDocuments.Add(doc);
                }

                _db.Claims.Add(claim);
                await _db.SaveChangesAsync();

                TempData["Success"] = "Claim submitted successfully!";
                return RedirectToAction("SubmitClaim");
            }

            return View(claim);
        }
    }
}
