using Microsoft.AspNetCore.Mvc;

namespace JES.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}


