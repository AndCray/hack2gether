using Microsoft.AspNetCore.Mvc;
using hack2gether.Data;
using hack2gether.Models;
using System.Linq;

namespace hack2gether.Controllers
{
    public class EngagementStaffController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EngagementStaffController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult EngagementDashboard()
        {
            var model = new EngagementDashboardViewModel
            {
                // Events = _db.Events.ToList(),
                Clubs = _db.Clubs.ToList(),
                Attendance = _db.Attendance.ToList()
            };

            return View(model);
        }
        private int GetCurrentUserId()
        {
            return HttpContext.Session.GetInt32("UserId") ?? 0;
        }

        public IActionResult Dashboard()
        {
            // No model needed for the new dashboard
            return View("EngagementDashboard");
        }
    }
}