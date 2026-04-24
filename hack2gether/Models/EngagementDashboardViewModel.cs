using Microsoft.AspNetCore.Mvc;

namespace hack2gether.Models
{
    public class EngagementDashboardViewModel
    {
        public List<EventModel> PendingEvents { get; set; }
        public List<EventModel> AllEvents { get; set; }
    }
}
