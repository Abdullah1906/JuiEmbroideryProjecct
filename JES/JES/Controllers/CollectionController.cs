using JES.DB;
using JES.Models;
using Microsoft.AspNetCore.Mvc;

namespace JES.Controllers
{
    public class CollectionController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;
        public CollectionController(DataContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            var viewModel = new CollectionViewModel
            {
                CollectionItem = new CollectionItemVm(),
                CollectionItems = _context.CollectionItems.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveGallery(CollectionViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Prepare image path
                string? imagePath = model.CollectionItem.ExistingImageUrl;

                if (model.CollectionItem.Image != null && model.CollectionItem.Image.Length > 0)
                {
                    // Generate unique file name
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.CollectionItem.Image.FileName);

                    // Define upload path
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "Collection");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    string filePath = Path.Combine(uploadsFolder, fileName);

                    // Save file to wwwroot/uploads/products
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.CollectionItem.Image.CopyTo(fileStream);
                    }

                    imagePath = "/uploads/Collection/" + fileName;
                }


                var galleryEntity = new CollectionItem
                {
                    Id = model.CollectionItem.Id,
                    Title = model.CollectionItem.Title,
                    ImageUrl = imagePath,
                    IsActive = true,
                    IsDelete = false
                };


                if (model.CollectionItem.Id == 0)
                {
                    galleryEntity.CreatedBy = User.Identity?.Name;
                    galleryEntity.CreatedAt = DateTime.UtcNow;
                    _context.CollectionItems.Add(galleryEntity);
                }

                else
                {
                    galleryEntity.UpdatedBy = User.Identity?.Name;
                    galleryEntity.UpdatedAt = DateTime.UtcNow;
                    _context.CollectionItems.Update(galleryEntity);
                }


                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // Return view with validation errors
            var viewModel = new CollectionViewModel
            {
                CollectionItem = model.CollectionItem, // keep posted data in form
                CollectionItems = _context.CollectionItems.ToList()
            };


            return View("Index", viewModel);

        }
        public IActionResult EditCollection(int id)
        {
            var gallery = _context.CollectionItems.FirstOrDefault(g => g.Id == id);
            if (gallery == null) return NotFound();

            var galleryVM = new CollectionItemVm
            {
                Title = gallery.Title,
                ExistingImageUrl = gallery.ImageUrl
            };

            var viewModel = new CollectionViewModel
            {
                CollectionItem = galleryVM,
                CollectionItems = _context.CollectionItems.ToList()
            };

            return View("Index", viewModel);
        }

        public IActionResult DeleteCollection(int id)
        {
            var gallery = _context.CollectionItems.Find(id);
            if (gallery != null)
            {
                // Delete image from wwwroot if it exists
                if (!string.IsNullOrEmpty(gallery.ImageUrl))
                {
                    var oldPath = Path.Combine(_env.WebRootPath, gallery.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                _context.CollectionItems.Remove(gallery);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
