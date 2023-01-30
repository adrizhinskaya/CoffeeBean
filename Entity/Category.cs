using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeBean.Entity
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] CategoryImg { get; set; }
    }
}
