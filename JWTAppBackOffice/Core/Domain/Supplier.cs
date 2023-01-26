namespace JWTAppBackOffice.Core.Domain
{
    public class Supplier
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public decimal Freight { get; set; }

        // Nav Props
        public List<Product> Products { get; set; }
    }
}
