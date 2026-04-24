using hack2gether.Models;
using Microsoft.AspNetCore.Mvc;

public class AdminDashboardController : Controller
{
    private static List<EventModel> Events = new List<EventModel>();

    public IActionResult Index()
    {
        return View("AdminDashboard", Events);
    }

    [HttpPost]
    public IActionResult AddEvent(EventModel model)
    {
        Events.Add(model);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult RemoveEvent(string title)
    {
        Events.RemoveAll(e => e.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        return RedirectToAction("Index");
    }
}