using JES.DB;
using JES.Models;
using Microsoft.AspNetCore.Mvc;

namespace JES.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly DataContext _context;
        public LoginController(ILogger<LoginController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                var validation = _context.Users.FirstOrDefault(x => x.U_EMAIL == login.U_EMAIL && x.U_PASSWORD == login.U_PASSWORD);
                if(validation != null)
                {
                    // HttpContext.Session.SetString("U_EMAIL", validation.U_EMAIL);
                    // HttpContext.Session.SetString("U_NAME", validation.U_NAME);
                    // HttpContext.Session.SetInt32("userTypeId", validation.userTypeId ?? 0);
                    // For demonstration purposes, using TempData to store user info
                    TempData["U_EMAIL"] = validation.U_EMAIL;
                    TempData["U_NAME"] = validation.U_NAME;
                    TempData["userTypeId"] = validation.userTypeId ?? 0;
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "Home");
            }
            return View(login);
        }
    }
}
