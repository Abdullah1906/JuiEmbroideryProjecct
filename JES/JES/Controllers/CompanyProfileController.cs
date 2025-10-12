using JES.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JES.Controllers
{
    public class CompanyProfileController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;
        public CompanyProfileController(IWebHostEnvironment env,DataContext context)
        {
            _env = env;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }



        // Handles all partial views dynamically with fallback
        public IActionResult LoadSection(string viewName)
        {
            var partialPath = Path.Combine("Views", "CompanyProfile", $"_{viewName}.cshtml");

            // Normalize view name (ignore case)
            viewName = viewName?.Trim() ?? "";

            // ✅ Handle "Collections" (load from DB)
            if (viewName.Equals("Collections", StringComparison.OrdinalIgnoreCase))
            {
                var collections = _context.CollectionItems
                    .Where(x => x.IsActive && !x.IsDelete)
                    .OrderByDescending(x => x.CreatedAt)
                    .ToList();

                return PartialView("_Collection", collections);
            }

            // ✅ For other partials (no data)
            if (System.IO.File.Exists(partialPath))
            {
                return PartialView($"_{viewName}");
            }
            else
            {
                return Content($"<p style='color:#ccc; line-height:1.8;'>The <strong>{viewName}</strong> section is coming soon. Please check back later.</p>", "text/html");
            }
        }

        //public IActionResult Collections()
        //{
        //    return PartialView("_Collections");
        //}

        //public IActionResult OurCoreCustomers()
        //{
        //    return PartialView("_OurCoreCustomers");
        //}

        //public IActionResult AwardsCertification()
        //{
        //    return PartialView("_AwardsCertification");
        //}

        //public IActionResult CodeOfConduct()
        //{
        //    return PartialView("_CodeOfConduct");
        //}

        //public IActionResult Commitment()
        //{
        //    return PartialView("_Commitment");
        //}

        //public IActionResult Vision()
        //{
        //    return PartialView("_Vision");
        //}

        //public IActionResult Mission()
        //{
        //    return PartialView("_Mission");
        //}
    }
}
