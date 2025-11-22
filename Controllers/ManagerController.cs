using Microsoft.AspNetCore.Mvc;
using CMCS.Models;
using System.Linq;

namespace CMCS.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManagerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // View claims that were forwarded by the Coordinator
        public IActionResult Dashboard()
        {
            var claims = _context.Claims
                .Where(c => c.Status == "Forwarded to Manager")
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
                claim.Status = "Approved";
                _context.Update(claim);
                _context.SaveChanges();
                TempData["Success"] = "Claim approved successfully!";
            }
            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public IActionResult RejectClaim(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim != null)
            {
                claim.Status = "Rejected by Manager";
                _context.Update(claim);
                _context.SaveChanges();
                TempData["Success"] = "Claim rejected.";
            }
            return RedirectToAction(nameof(Dashboard));
        }
    }
}
