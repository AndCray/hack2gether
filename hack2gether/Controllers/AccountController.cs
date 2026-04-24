using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace hack2gether.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // ---------------- REGISTER ----------------
        [HttpPost]
        public async Task<IActionResult> Register(string email, string password, string role)
        {
            // Ensure roles exist
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

            var user = new IdentityUser { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
                return RedirectToAction("Index", "Home"); // returns to login page
            }

            return RedirectToAction("Index", "Home");
        }

        // ---------------- LOGIN ----------------
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, string role)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return RedirectToAction("Index", "Home");

            // Check password
            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);

            if (!result.Succeeded)
                return RedirectToAction("Index", "Home");

            // Check if user has the selected role
            if (!await _userManager.IsInRoleAsync(user, role))
                return RedirectToAction("Index", "Home");

            // Redirect based on role
            if (role == "Student")
                return RedirectToAction("Dashboard", "Student");

            if (role == "ClubAdmin")
                return RedirectToAction("Dashboard", "ClubAdmin");

            if (role == "EngagementStaff")
                return RedirectToAction("Dashboard", "EngagementStaff");

            return RedirectToAction("Index", "Home");
        }
    }
}