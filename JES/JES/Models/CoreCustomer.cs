using System.ComponentModel.DataAnnotations;

namespace JES.Models
{
    public class CoreCustomer:Base
    {
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? ImageUrl { get; set; }

    }
    public class CoreCustomerViewModel
    {
        public CoreCustomerVm CoreCustomer { get; set; } = new CoreCustomerVm();
        public List<CoreCustomer> CoreCustomers { get; set; } = new List<CoreCustomer>();
    }
    public class CoreCustomerVm : Base
    {
        [Required]
        public string? CompanyName { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? Email { get; set; }
        public IFormFile? Image { get; set; }
        public string? ExistingImageUrl { get; set; }

    }
}
