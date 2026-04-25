using Microsoft.AspNetCore.Mvc;

namespace hack2gether.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult StudentDashboard()
        {
            return View();
        }
    }
}