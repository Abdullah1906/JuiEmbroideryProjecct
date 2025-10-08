using JES.DB;
using JES.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JES.Controllers
{
    public class CoreCustomerController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;
        public CoreCustomerController(DataContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            var viewModel = new CoreCustomerViewModel
            {
                CoreCustomer = new CoreCustomerVm(),
                CoreCustomers = _context.CoreCustomers.ToList()
            };

            return View(viewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveCoreCustomer(CoreCustomerVm coreCustomerVm)
        {
            if (ModelState.IsValid)
            {
                string? imagePath = coreCustomerVm.ExistingImageUrl; // Keep existing image if none uploaded

                // Handle image upload
                if (coreCustomerVm.Image != null && coreCustomerVm.Image.Length > 0)
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(coreCustomerVm.Image.FileName);
                    string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "customers");

                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    string filePath = Path.Combine(uploadsFolder, fileName);
                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    coreCustomerVm.Image.CopyTo(fileStream);

                    imagePath = "/uploads/customers/" + fileName;
                }

                // Map ViewModel to Entity
                var customerEntity = new CoreCustomer
                {
                    Id = coreCustomerVm.Id,
                    CompanyName = coreCustomerVm.CompanyName,
                    Address = coreCustomerVm.Address,
                    Phone = coreCustomerVm.Phone,
                    Email = coreCustomerVm.Email,
                    ImageUrl = imagePath,
                    IsActive = true,
                    IsDelete = false
                };

                if (coreCustomerVm.Id == 0)
                {
                    customerEntity.CreatedBy = User.Identity?.Name;
                    customerEntity.CreatedAt = DateTime.UtcNow;
                    _context.CoreCustomers.Add(customerEntity);
                }
                else
                {
                    customerEntity.UpdatedBy = User.Identity?.Name;
                    customerEntity.UpdatedAt = DateTime.UtcNow;
                    _context.CoreCustomers.Update(customerEntity);
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(CoreCustomer));
            }

            // If validation fails, reload the view
            var viewModel = new CoreCustomerViewModel
            {
                CoreCustomer = coreCustomerVm ?? new CoreCustomerVm(),
                CoreCustomers = _context.CoreCustomers.ToList()
            };

            return View("CoreCustomer", viewModel);
        }

        public IActionResult EditCoreCustomer(int id)
        {
            var customer = _context.CoreCustomers.FirstOrDefault(c => c.Id == id);

            if (customer == null) return NotFound();

            var customerVm = new CoreCustomerVm
            {
                Id = customer.Id,
                CompanyName = customer.CompanyName,
                Address = customer.Address,
                Phone = customer.Phone,
                Email = customer.Email,
                ExistingImageUrl = customer.ImageUrl
            };

            var viewModel = new CoreCustomerViewModel
            {
                CoreCustomer = customerVm,
                CoreCustomers = _context.CoreCustomers.ToList()
            };

            return View("CoreCustomer", viewModel);
        }


        public IActionResult DeleteCoreCustomer(int id)
        {
            var customer = _context.CoreCustomers.Find(id);
            if (customer != null)
            {
                // Delete image file if it exists
                if (!string.IsNullOrEmpty(customer.ImageUrl))
                {
                    var oldPath = Path.Combine(_env.WebRootPath, customer.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                _context.CoreCustomers.Remove(customer);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
