using Microsoft.AspNetCore.Mvc;

namespace JES.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
