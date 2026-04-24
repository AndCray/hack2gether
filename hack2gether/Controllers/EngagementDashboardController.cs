using hack2gether.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class EngagementDashboardController : Controller
{
    public IActionResult Index()
    {
        var vm = new EngagementDashboardViewModel
        {
            PendingEvents = AdminDashboardController.PendingEvents,
            AllEvents = AdminDashboardController.ApprovedEvents
        };

        return View("EngagementDashboard", vm);
    }

    // Approve an event
    [HttpPost]
    public IActionResult ApproveEvent(int eventId)
    {
        var ev = AdminDashboardController.PendingEvents
            .FirstOrDefault(e => e.Id == eventId);

        if (ev != null)
        {
            AdminDashboardController.PendingEvents.Remove(ev);
            AdminDashboardController.ApprovedEvents.Add(ev);
        }

        return RedirectToAction("Index");
    }

    // Reject an event
    [HttpPost]
    public IActionResult RejectEvent(int eventId)
    {
        AdminDashboardController.PendingEvents
            .RemoveAll(e => e.Id == eventId);

        return RedirectToAction("Index");
    }

    // Route a notification to a department
    [HttpPost]
    public IActionResult RouteNotification(string message, string department)
    {
        // Placeholder for real notification routing logic
        Console.WriteLine($"Notification sent to {department}: {message}");

        TempData["NotificationSent"] = $"Message sent to {department}";
        return RedirectToAction("Index");
    }
}