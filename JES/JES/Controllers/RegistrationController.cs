using JES.DB;
using JES.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JES.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly DataContext _context;
        public RegistrationController(DataContext context)
        {
            _context = context;
        }
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); 
            }

            var exist = await _context.Users.FirstOrDefaultAsync(x => x.U_EMAIL == model.U_EMAIL);
            if (exist != null)
            {
                ViewBag.error = "User already exists.";
                return View(model);
            }

            var user = new User
            {
                U_NAME = model.U_NAME,
                U_EMAIL = model.U_EMAIL,
                U_PHONE_NO = model.U_PHONE_NO,
                U_ADDRESS = model.U_ADDRESS,
                U_PASSWORD = model.U_PASSWORD, 
                userTypeId = 2,
                CreatedAt = DateTime.Now,
                CreatedBy = model.U_EMAIL
            };

            if (model.ProfileImage != null && model.ProfileImage.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(model.ProfileImage.FileName);
                var filePath = Path.Combine("wwwroot/uploads/profiles", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ProfileImage.CopyToAsync(stream);
                }

                user.ProfileImageUrl = "/uploads/profiles/" + fileName;
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Registration successful!";
            return RedirectToAction("Login", "Login");
        }



        [HttpGet("RegisterAdmin")]
        public IActionResult RegisterAdmin()
        {
            return View();
        }


        [HttpPost("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin(UserVm model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid form data.";
                return View(model);
            }

            var exist = await _context.Users.FirstOrDefaultAsync(x => x.U_EMAIL == model.U_EMAIL);
            if (exist != null)
            {
                TempData["Error"] = "User already exists.";
                return View(model);
            }

            var user = new User
            {
                U_NAME = model.U_NAME,
                U_EMAIL = model.U_EMAIL,
                U_PHONE_NO = model.U_PHONE_NO,
                U_ADDRESS = model.U_ADDRESS,
                U_PASSWORD = model.U_PASSWORD, // ⚠ hash this in production
                userTypeId = 1, // Admin
                CreatedAt = DateTime.Now,
                CreatedBy = model.U_EMAIL
            };

            if (model.ProfileImage != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(model.ProfileImage.FileName);
                var filePath = Path.Combine("wwwroot/uploads/profiles", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ProfileImage.CopyToAsync(stream);
                }
                user.ProfileImageUrl = "/uploads/profiles/" + fileName;
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Admin registration successful!";
            return RedirectToAction("Login", "Login");
        }


        [HttpPost("ForgetPasswordAdmin")]
        public IActionResult AdminForgetP()
        {
            return View();

        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(UserVm usr)
        {
            try
            {


                // Check if user exists by email
                var exist = await _context.Users
                    .FirstOrDefaultAsync(x => x.U_EMAIL == usr.U_EMAIL && x.userTypeId == 1);

                if (exist != null)
                {
                    // Update the password
                    exist.U_PASSWORD = usr.U_PASSWORD;

                    // Save changes to database
                    _context.Users.Update(exist);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Password has been updated successfully!";
                    return RedirectToAction("Login", "Login");
                }

                // User not found
                ViewBag.error = "User Not Found In Database";
                return View("AdminForgetP", usr);
            }
            catch (Exception ex)
            {
                // Log exception (recommended in real app)
                ViewBag.error = "An unexpected error occurred.";
                return View("AdminForgetP", usr);
            }
        }
    }
}
