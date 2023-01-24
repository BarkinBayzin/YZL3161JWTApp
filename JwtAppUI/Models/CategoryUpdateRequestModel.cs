using System.ComponentModel.DataAnnotations;

namespace JwtAppUI.Models
{
    public class CategoryUpdateRequestModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Kategori Adı Boş Bırakılamaz..!")]
        public string Definition { get; set; }
    }
}
