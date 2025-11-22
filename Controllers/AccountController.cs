using Microsoft.AspNetCore.Mvc;
using CMCS.Models;
using System.Linq;
namespace CMCS.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        public  AccountController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Login() => View();
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                ViewBag.Error = "Invalid credentials.";
                return View();
            }
            if (user.Role == "HR") return RedirectToAction("Dashboard", "HR");
            if (user.Role == "Lecturer") return RedirectToAction("Dashboard", "Lecturer");
            if (user.Role == "Coordinator") return RedirectToAction("Dashboard", "Coordinator");
            if (user.Role == "Manager") return RedirectToAction("Dashboard", "Manager");
            return View();
        }
    }
}

