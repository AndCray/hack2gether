namespace hack2gether.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string ClubName { get; set; }
        public string Status { get; internal set; }
    }
}