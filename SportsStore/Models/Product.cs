namespace SportsStore.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

       public override bool Equals(object obj)
        {
            Product p = obj as Product;
            if (p == null) return false;
            return p.ProductId == ProductId;
        }     
    }


}