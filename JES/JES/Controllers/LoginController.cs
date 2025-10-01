using Microsoft.AspNetCore.Mvc;

namespace JES.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
