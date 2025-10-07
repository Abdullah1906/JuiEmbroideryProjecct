namespace JES.Models
{
    public class Base
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; } = false;

        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
