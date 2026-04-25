using Microsoft.AspNetCore.Mvc;
using hack2gether.Data;

namespace hack2gether.Controllers
{
    public class ClubAdminController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ClubAdminController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Dashboard()
        {
            // No model needed for the new dashboard
            return View("AdminDashboard");
        }

        public IActionResult Officers()
        {
            return View();
        }

        public IActionResult CreateEvent()
        {
            return View();
        }

        public IActionResult CheckIn()
        {
            return View();
        }

        public IActionResult Presence()
        {
            return View();
        }
    }
}