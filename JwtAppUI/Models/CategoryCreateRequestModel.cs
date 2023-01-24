using System.ComponentModel.DataAnnotations;

namespace JwtAppUI.Models
{
    public class CategoryCreateRequestModel
    {
        [Required(ErrorMessage = "Kategori Adı Boş Bırakılamaz")]
        public string Definition { get; set; }
    }
}
