namespace JWTAppBackOffice.Core.Domain
{
    public class Shipper
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public decimal Freight { get; set; }

        // Nav Props
        public List<Product> Products { get; set; }
    }
}
