using JES.DB;
using JES.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Login(LoginVM usr)
        {

            if (string.IsNullOrWhiteSpace(usr.U_EMAIL))
            {
                ViewBag.error = "Email cannot be empty.";

                return View(usr);
            }

            if (string.IsNullOrWhiteSpace(usr.U_PASSWORD))
            {
                ViewBag.error = "Password cannot be empty.";

                return View(usr);
            }

            // Validate user credentials from DB
            var exist = await _context.Users
                .Where(x => x.U_EMAIL == usr.U_EMAIL && x.U_PASSWORD == usr.U_PASSWORD)
                .FirstOrDefaultAsync();

            if (exist == null)
            {
                ViewBag.error = "User not found. Please check email and password.";

                return View(usr);
            }

            ////Verify password
            //var passwordHasher = new PasswordHasher<User>();
            //var result = passwordHasher.VerifyHashedPassword(usr, usr.U_PASSWORD!, usr.U_PASSWORD!);

            //if (result != PasswordVerificationResult.Success)
            //{
            //    ViewBag.error = "Incorrect password.";

            //    return View(usr);
            //}

            if (exist != null)
            {

         
                var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, exist.U_NAME),  
                            new Claim(ClaimTypes.Email, exist.U_EMAIL),
                            new Claim("Id", exist.Id.ToString()),
                            new Claim("UserTypeId", exist.userTypeId.ToString()),
                            new Claim("ProfileImageUrl", string.IsNullOrEmpty(exist.ProfileImageUrl) ? "" : exist.ProfileImageUrl)
                        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);



                await HttpContext.SignInAsync(
                   CookieAuthenticationDefaults.AuthenticationScheme,
                   claimsPrincipal,
                   new AuthenticationProperties
                   {
                       IsPersistent = true,

                   });

                // Redirect based on user type
                if (exist.userTypeId == 1)
                {
                    return RedirectToAction("DashBoard", "Home");
                }
                else if (exist.userTypeId == 3)
                {
                    return RedirectToAction("SupplierList", "S_R");
                }
                else if (exist.userTypeId == 4)
                {
                    return RedirectToAction("RiderRequ", "S_R");
                }
                else
                {
                    return RedirectToAction("Index", "Landing");
                }
            }

            ViewBag.error = "User Not Found In Database";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // Clear the entire session

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();


            return RedirectToAction("Index", "Home");
        }
        public IActionResult ForgetPassword()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(User usr)
        {
            try
            {

                var exist = await _context.Users
                    .FirstOrDefaultAsync(x => x.U_EMAIL == usr.U_EMAIL && x.userTypeId == 2);

                if (exist != null)
                {

                    exist.U_PASSWORD = usr.U_PASSWORD;

                    _context.Users.Update(exist);
                    await _context.SaveChangesAsync();



                    return RedirectToAction("Login", "Login");
                }

                // User not found
                ViewBag.error = "User not found in database.";
                return View(usr);
            }
            catch (Exception ex)
            {

                ViewBag.error = "An unexpected error occurred.";
                return View(usr);
            }
        }

    }
}
