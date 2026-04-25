using Microsoft.AspNetCore.Mvc;
using hack2gether.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace hack2gether.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AccountController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Student/Login.cshtml");
        }

        [HttpPost]
        public IActionResult Login(string email, string password, string role)
        {
            var user = _db.Users
                .FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                ViewBag.Error = "Invalid email or password";
                return View("~/Views/Student/Login.cshtml");
            }

            // Convert dropdown values to match DB values
            string normalizedRole = role switch
            {
                "ClubAdmin" => "Club Admin",
                "EngagementStaff" => "Engagement Staff",
                _ => role
            };

            // Ensure selected role matches the user's actual role
            if (user.Role != normalizedRole)
            {
                ViewBag.Error = "Incorrect role selected";
                return View("~/Views/Student/Login.cshtml");
            }

            // Store user info in session
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            // Redirect based on role
            return normalizedRole switch
            {
                "Student" => RedirectToAction("StudentDashboard", "Student"),
                "Club Admin" => RedirectToAction("AdminDashboard", "ClubAdmin"),
                "Engagement Staff" => RedirectToAction("EngagementDashboard", "EngagementStaff"),
                _ => RedirectToAction("Index", "Home")
            };
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}