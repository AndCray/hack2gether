using Microsoft.AspNetCore.Mvc;
using hack2gether.Data;
using hack2gether.Models;
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

        // ---------------- LOGIN ----------------
        [HttpPost]
        public IActionResult Login(string email, string password, string role)
        {
            var user = _db.Users
                .FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                ViewBag.Error = "Invalid email or password";
                return View("~/Views/Student/StudentDashboard.cshtml");
            }

            // Normalize dropdown role to match DB
            string normalizedRole = role switch
            {
                "Club Admin" => "Club Admin",
                "Engagement Staff" => "Engagement Staff",
                "Student" => "Student",
                _ => role
            };

            if (user.Role.Trim() != normalizedRole)
            {
                ViewBag.Error = "Incorrect role selected";
                return View("~/Views/Student/StudentDashboard.cshtml");
            }

            // Log user in
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            return RedirectToDashboard(normalizedRole);
        }

        // ---------------- SIGNUP ----------------
        [HttpPost]
        public IActionResult Register(string email, string password, string role)
        {
            // Normalize dropdown role to match DB
            string normalizedRole = role switch
            {
                "Club Admin" => "Club Admin",
                "Engagement Staff" => "Engagement Staff",
                "Student" => "Student",
                _ => role
            };

            // Create new user
            var newUser = new User
            {
                Email = email,
                Username = email.Split('@')[0], // simple username
                Password = password,
                Role = normalizedRole
            };

            _db.Users.Add(newUser);
            _db.SaveChanges();

            // Auto-login after signup
            HttpContext.Session.SetString("Username", newUser.Username);
            HttpContext.Session.SetString("Role", newUser.Role);

            return RedirectToDashboard(normalizedRole);
        }

        // ---------------- LOGOUT ----------------
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // ---------------- HELPER ----------------
        private IActionResult RedirectToDashboard(string role)
        {
            return role switch
            {
                "Student" => RedirectToAction("StudentDashboard", "Student"),
                "Club Admin" => RedirectToAction("Dashboard", "ClubAdmin"),
                "Engagement Staff" => RedirectToAction("EngagementDashboard", "EngagementStaff"),
                _ => RedirectToAction("Index", "Home")
        };
        }
    }
}