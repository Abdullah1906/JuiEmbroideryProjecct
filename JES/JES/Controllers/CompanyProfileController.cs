using Microsoft.AspNetCore.Mvc;

namespace JES.Controllers
{
    public class CompanyProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Collections()
        {
            return PartialView("_Collections");
        }

        public IActionResult OurCoreCustomers()
        {
            return PartialView("_OurCoreCustomers");
        }

        public IActionResult AwardsCertification()
        {
            return PartialView("_AwardsCertification");
        }

        public IActionResult CodeOfConduct()
        {
            return PartialView("_CodeOfConduct");
        }

        public IActionResult Commitment()
        {
            return PartialView("_Commitment");
        }

        public IActionResult Vision()
        {
            return PartialView("_Vision");
        }

        public IActionResult Mission()
        {
            return PartialView("_Mission");
        }
    }
}
