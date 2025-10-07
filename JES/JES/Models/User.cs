using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JES.Models
{
    public class User : Base
    {

        public string? U_NAME { get; set; }
        [Required]
        public string? U_EMAIL { get; set; }
        [Required]
        public string? U_PHONE_NO { get; set; }
        public string? U_ADDRESS { get; set; }
        [Required]
        public string? U_PASSWORD { get; set; }
        public int? userTypeId { get; set; }
        [NotMapped]
        [Compare("U_PASSWORD", ErrorMessage = "Passwords do not match.")]
        public string? U_CONFIRM_PASSWORD { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpiry { get; set; }


    }
    public class UserVm : Base
    {

        public string? U_NAME { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string? U_EMAIL { get; set; }
        [Required(ErrorMessage ="Phone Number is required")]
        public string? U_PHONE_NO { get; set; }
        public string? U_ADDRESS { get; set; }
        [Required(ErrorMessage = "*Password is required.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain at least one lowercase, one uppercase, one number and one special character.")]
        public string? U_PASSWORD { get; set; }
        public int? userTypeId { get; set; }
        [NotMapped]
        [Compare("U_PASSWORD", ErrorMessage = "Passwords do not match.")]
        public string? U_CONFIRM_PASSWORD { get; set; }

        public string? ProfileImageUrl { get; set; }

        public IFormFile? ProfileImage { get; set; }


    }
    public class LoginVM
    {
        [Required]
        public string? U_EMAIL { get; set; }
        [Required]
        public string? U_PASSWORD { get; set; }
    }
    public class RegisterUserVM
    {
        public UserVm User { get; set; } = new();
        public List<UserVm> Users { get; set; } = new();
    }
}



