using System.ComponentModel.DataAnnotations;

namespace Shopping.Data.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }

        public Product Product { get; set; }

        [Display(Name = "Foto")]
        public string ImageId { get; set; }

        //TODO: Pending to change to the correct path
        [Display(Name = "Foto")]
        
        public string? ImageFullPath => string.IsNullOrEmpty(ImageId)
            ? $"https://localhost:7101/images/noimage.png"
            : $"https://localhost:7101{ImageId.Substring(1)}";
    }

}
