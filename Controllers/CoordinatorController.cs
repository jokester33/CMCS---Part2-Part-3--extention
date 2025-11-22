using Microsoft.AspNetCore.Mvc;
using CMCS.Models;
using System.Linq;

namespace CMCS.Controllers
{
    public class CoordinatorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoordinatorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Show all pending claims
        public IActionResult Dashboard()
        {
            var claims = _context.Claims
                .Where(c => c.Status == "Pending")
                .OrderBy(c => c.SubmittedDate)
                .ToList();

            return View(claims);
        }

        [HttpPost]
        public IActionResult ApproveClaim(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim != null)
            {
                claim.Status = "Forwarded to Manager";
                _context.Update(claim);
                _context.SaveChanges();
                TempData["Success"] = "Claim forwarded to Manager successfully.";
            }
            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public IActionResult RejectClaim(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim != null)
            {
                claim.Status = "Rejected by Coordinator";
                _context.Update(claim);
                _context.SaveChanges();
                TempData["Success"] = "Claim rejected successfully.";
            }
            return RedirectToAction(nameof(Dashboard));
        }
    }
}
