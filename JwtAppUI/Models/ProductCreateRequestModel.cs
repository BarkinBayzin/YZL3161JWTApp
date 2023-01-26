using System.ComponentModel.DataAnnotations;

namespace JwtAppUI.Models
{
    public class ProductCreateRequestModel
    {
        [Required(ErrorMessage = "Ürün Adı Boş Olamaz")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "Ürün Stok Miktarı Boş Olamaz")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "Ürün Fiyatı Boş Olamaz")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Ürün Kategorisi Boş Olamaz")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Ürün Tedarikçisi Boş Olamaz")]
        public int SupplierId { get; set; }
    }
}
