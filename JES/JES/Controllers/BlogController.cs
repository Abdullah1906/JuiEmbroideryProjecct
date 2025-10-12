using Microsoft.AspNetCore.Mvc;

namespace JES.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}


