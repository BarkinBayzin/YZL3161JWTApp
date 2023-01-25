using System.ComponentModel.DataAnnotations;

namespace JwtAppUI.Models
{
    public class ProductUpdateRequestModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ürün Adı Boş Olamaz")]
        public string Name { get; set; }
        public int Stock { get; set; }
        [Required(ErrorMessage = "Ürün Fiyatı Boş Olamaz")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Ürün Kategorisi Boş Olamaz")]
        public int CategoryId { get; set; }
    }
}
