using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace JES.Models
{
    public class Product : Base
    {

        
        public string? ProductName { get; set; }

       
        public int CategoryId { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public Category? Category { get; set; }

    }
    public class ProductIndexViewModel
    {

        public ProductVm Product { get; set; } = new ProductVm();
        public List<Product> Products { get; set; } = new();
        public List<Category> Categories { get; set; } = new();



    }
    public class ProductVm : Base
    {

        [Required]
        [StringLength(150)]
        public string? ProductName { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public string? ExistingImageUrl { get; set; }

        public Category? Category { get; set; }

    }
}
