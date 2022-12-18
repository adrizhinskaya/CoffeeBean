namespace CoffeeBean.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public Category Category { get; set; }
    }

    public enum Category
    {
        Coffee, 
        Equipment, 
        Tableware, 
        Optional
    }
}
