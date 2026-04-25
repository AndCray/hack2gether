using hack2gether.Models;

public class Survey
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public Event Event { get; set; }

    public string Title { get; set; }
    public bool IsActive { get; set; }
    public int PointsForCompletion { get; set; }
}

public class SurveyResponse
{
    public int Id { get; set; }
    public int SurveyId { get; set; }
    public Survey Survey { get; set; }

    public int StudentId { get; set; }
    public User Student { get; set; }

    public DateTime SubmittedAt { get; set; }
    public string AnswersJson { get; set; } // simple flexible storage
}