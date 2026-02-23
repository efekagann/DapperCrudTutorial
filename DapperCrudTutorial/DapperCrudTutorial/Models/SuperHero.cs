using System.ComponentModel.DataAnnotations;

namespace DapperCrudTutorial.Models
{
    public class SuperHero
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kahraman adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Kahraman adı en fazla 100 karakter olabilir.")]
        [Display(Name = "Kahraman Adı")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ad zorunludur.")]
        [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir.")]
        [Display(Name = "Ad")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Soyad zorunludur.")]
        [StringLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir.")]
        [Display(Name = "Soyad")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Yer zorunludur.")]
        [StringLength(100, ErrorMessage = "Yer en fazla 100 karakter olabilir.")]
        [Display(Name = "Şehir / Yer")]
        public string Place { get; set; } = string.Empty;
    }
}
