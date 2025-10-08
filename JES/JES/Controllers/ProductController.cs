using JES.DB;
using JES.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JES.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductController(DataContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Category()
        {
            var viewModel = new CategoryIndexViewModel
            {
                Categories = _context.Categories.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    _context.Categories.Add(category);
                }
                else
                {
                    _context.Categories.Update(category);
                }
                _context.SaveChanges();
                return RedirectToAction("Category");
            }

            var viewModel = new CategoryIndexViewModel
            {
                Category = category,
                Categories = _context.Categories.ToList()
            };
            return View("Category", viewModel);
        }

        public IActionResult EditCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();

            var viewModel = new CategoryIndexViewModel
            {
                Category = category,
                Categories = _context.Categories.ToList()
            };
            return View("Category", viewModel);
        }

        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            return RedirectToAction("Category");
        }

        public IActionResult Products()
        {
            var viewModel = new ProductIndexViewModel
            {
                Product = new ProductVm(),
                Products = _context.Products.Include(p => p.Category).ToList(),
                Categories = _context.Categories.ToList()
            };

            return View(viewModel); // ✅ Important
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveProducts(ProductVm product)
        {
            if (ModelState.IsValid)
            {
                string? imagePath = product.ExistingImageUrl; // Keep existing image if none uploaded

                // Handle image upload
                if (product.Image != null && product.Image.Length > 0)
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(product.Image.FileName);
                    string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "products");

                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    string filePath = Path.Combine(uploadsFolder, fileName);
                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    product.Image.CopyTo(fileStream);

                    imagePath = "/uploads/products/" + fileName;
                }

                // Map view model to entity
                var productEntity = new Product
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    CategoryId = product.CategoryId,
                    ImageUrl = imagePath,
                    IsActive = true,
                    IsDelete = false
                };

                if (product.Id == 0)
                {
                    productEntity.CreatedBy = User.Identity?.Name;
                    productEntity.CreatedAt = DateTime.UtcNow;
                    _context.Products.Add(productEntity);
                }
                else
                {
                    productEntity.UpdatedBy = User.Identity?.Name;
                    productEntity.UpdatedAt = DateTime.UtcNow;
                    _context.Products.Update(productEntity);
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Products));
            }

            // Return view with validation errors
            var viewModel = new ProductIndexViewModel
            {
                Product = product ?? new ProductVm(),
                Products = _context.Products
                    .Include(p => p.Category)
                    .ToList(),
                Categories = _context.Categories.ToList()
            };

            return View("Products", viewModel);
        }

        public IActionResult EditProducts(int id)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);

            if (product == null) return NotFound();

            var productVM = new ProductVm
            {
                Id = product.Id,
                ProductName = product.ProductName,
                CategoryId = product.CategoryId,
                Category = product.Category,
                ExistingImageUrl = product.ImageUrl
            };

            var viewModel = new ProductIndexViewModel
            {
                Product = productVM,
                Products = _context.Products
                    .Include(p => p.Category)
                    .ToList(),
                Categories = _context.Categories.ToList()
            };

            return View("Products", viewModel);
        }

        public IActionResult DeleteProducts(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                // Delete image file if it exists
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    var oldPath = Path.Combine(_env.WebRootPath, product.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                _context.Products.Remove(product);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Products));
        }

    }
}
