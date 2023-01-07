using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeBean.Entity
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        public Category Cathegory { get; set; }
        public string CategoryId { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
