using hack2gether.Data;
using Microsoft.AspNetCore.Mvc;

public class SurveyController : Controller
{
    private readonly ApplicationDbContext _db;
    private int GetCurrentUserId()
    {
        return HttpContext.Session.GetInt32("UserId") ?? 0;
    }
    public SurveyController(ApplicationDbContext db) => _db = db;

    public IActionResult Create(int eventId)
    {
        var survey = new Survey { EventId = eventId };
        return View(survey);
    }

    [HttpPost]
    public IActionResult Create(Survey model)
    {
        if (!ModelState.IsValid) return View(model);

        model.IsActive = true;
        _db.Surveys.Add(model);
        _db.SaveChanges();

        return RedirectToAction("EngagementDashboard", "EngagementStaff");
    }

    // Student fills survey
    [HttpPost]
    public IActionResult SubmitResponse(int surveyId, string answersJson)
    {

    var userId = GetCurrentUserId();

        bool alreadySubmitted = _db.SurveyResponses
            .Any(r => r.SurveyId == surveyId && r.StudentId == userId);

        if (!alreadySubmitted)
        {
            var response = new SurveyResponse
            {
                SurveyId = surveyId,
                StudentId = userId,
                SubmittedAt = DateTime.UtcNow,
                AnswersJson = answersJson
            };

            _db.SurveyResponses.Add(response);

            var survey = _db.Surveys.First(s => s.Id == surveyId);
            var student = _db.Users.First(u => u.Id == userId);
            student.Points += survey.PointsForCompletion;

            _db.SaveChanges();
        }

        return RedirectToAction("StudentDashboard", "Student");
    }
}