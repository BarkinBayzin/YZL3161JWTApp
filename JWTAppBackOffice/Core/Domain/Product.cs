namespace JWTAppBackOffice.Core.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Definition { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
