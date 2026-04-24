using hack2gether.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

public class AdminDashboardController : Controller
{
    // Shared lists used by BOTH dashboards
    public static List<EventModel> PendingEvents = new List<EventModel>();
    public static List<EventModel> ApprovedEvents = new List<EventModel>();

    public IActionResult Index()
    {
        // Admin calendar only shows approved events
        return View("AdminDashboard", ApprovedEvents);
    }

    [HttpPost]
    public IActionResult AddEvent(EventModel model)
    {
        // Assign unique ID
        model.Id = Guid.NewGuid().GetHashCode();

        // Club-submitted events go to Engagement Staff for approval
        PendingEvents.Add(model);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult RemoveEvent(string title)
    {
        // Remove only from approved events (admins cannot remove pending ones)
        ApprovedEvents.RemoveAll(e =>
            e.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

        return RedirectToAction("Index");
    }
}