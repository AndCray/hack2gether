using hack2gether.Data;
using hack2gether.Models;
using Microsoft.AspNetCore.Mvc;

namespace hack2gether.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;
        public StudentController(ApplicationDbContext db) => _db = db;

        [HttpPost]
        public IActionResult RegisterForEvent(int eventId)
        {
            var userId = GetCurrentUserId();

            // optional: store a registration table, or just rely on Attendance at check-in
            // For now, you could just mark intent in a simple table later.

            return RedirectToAction("StudentDashboard");
        }

        private int GetCurrentUserId()
        {
            return HttpContext.Session.GetInt32("UserId") ?? 0;
        }

    [HttpPost]
        public IActionResult CheckIn(int eventId, string code)
        {
            var userId = GetCurrentUserId();

            var evt = _db.Events.FirstOrDefault(e => e.Id == eventId && e.CheckInCode == code);
            if (evt == null)
            {
                // invalid code
                TempData["Error"] = "Invalid check-in code.";
                return RedirectToAction("StudentDashboard");
            }

            // prevent duplicate check-in
            bool alreadyCheckedIn = _db.Attendance
                .Any(a => a.EventId == eventId && a.StudentId == Convert.ToInt32(userId));

            if (!alreadyCheckedIn)
            {
                var attendance = new Attendance
                {
                    EventId = eventId,
                    StudentId = (int)userId,
                    Timestamp = DateTime.UtcNow,
                    PointsAwarded = 10 // example
                };

                _db.Attendance.Add(attendance);

                var student = _db.Users.First(u => u.Id == Convert.ToInt32(userId));
                student.Points += attendance.PointsAwarded;

                _db.SaveChanges();
            }

            return RedirectToAction("StudentDashboard");
        }
        public IActionResult Leaderboard()
        {
            var topStudents = _db.Users
                .Where(u => u.Role == "Student")
                .OrderByDescending(u => u.Points)
                .Take(20)
                .ToList();

            return View(topStudents);
        }
    }
}