namespace JES.Models
{
    public class OrgNode
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string NodeType { get; set; } // "top-level", "manager-level", "staff-level"
        public int Level { get; set; }
        public int? ParentId { get; set; }
        public int DisplayOrder { get; set; }
        public string ChartType { get; set; } // "factory" or "office"
    }
}
