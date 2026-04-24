using Microsoft.AspNetCore.Mvc;

namespace hack2gether.Models
{
    public class EventModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ClubName { get; set; }
        public string SubmittedBy { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
    }
}
