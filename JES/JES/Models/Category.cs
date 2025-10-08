using System.ComponentModel.DataAnnotations;

namespace JES.Models
{
    public class Category : Base
    {

        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Product>? Products { get; set; }

    }
    public class CategoryIndexViewModel
    {
        public Category Category { get; set; } = new Category();
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
