namespace JwtAppUI.Models
{
    public class ProductListResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public CategoryListResponseModel Category { get; set; }

        public int SupplierId { get; set; }
        public SupplierListResponseModel Supplier { get; set; }
    }
}
