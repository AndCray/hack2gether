using hack2gether.Data;
using Microsoft.AspNetCore.Mvc;

public class ClubAdminController : Controller
{
    private readonly ApplicationDbContext _db;

    public ClubAdminController(ApplicationDbContext db) => _db = db;

    // GET: list of events for this club admin
    public IActionResult MyEvents()
    {
        var userId = GetCurrentUserId();

        var events = _db.Events
            .Where(e => e.Club.AdminId == userId)
            .ToList();

        return View(events);
    }

    private int GetCurrentUserId()
    {
        return HttpContext.Session.GetInt32("UserId") ?? 0;
    }

    // GET: create event form
    public IActionResult CreateEvent() => View();

    // POST: create event
    [HttpPost]
    public IActionResult CreateEvent(Event model)
    {
        if (!ModelState.IsValid) return View(model);

        // set defaults
        model.Status = "Pending";
        model.CheckInCode = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();

        _db.Events.Add(model);
        _db.SaveChanges();

        return RedirectToAction("MyEvents");
    }
}