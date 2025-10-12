namespace JES.Models
{
    public class CollectionItem:Base
    {

        public string Title { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } // e.g. "/uploads/collections/denim.jpg"
       
    }
    public class CollectionViewModel
    {
        public List<CollectionItem> CollectionItems { get; set; } = new();
        public CollectionItemVm CollectionItem { get; set; } = new CollectionItemVm();
    }
    public class CollectionItemVm : Base
    {

        public string? Title { get; set; }
        public IFormFile? Image { get; set; }
        public string? ExistingImageUrl { get; set; }

    }
}
