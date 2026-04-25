using hack2gether.Models;

public class Event
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Status { get; set; } // Pending, Approved, etc.
    public int ClubId { get; set; }
    public Club Club { get; set; }

    public bool RequiresRegistration { get; set; }
    public string CheckInCode { get; set; } // for digital check-in
}