using System.ComponentModel.DataAnnotations;

namespace DietApp.Models
{
    public class DiyetisyenRegisterViewModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string SurName { get; set; } = string.Empty;
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string Email { get; set; } = string.Empty;


        [Required]
        public string PhoneNumber { get; set; } = string.Empty;


        [Required]
        [StringLength(100, ErrorMessage = "Şifre en az {2} ve en fazla {1} karakter olmalıdır.", MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Şifreler eşleşmiyor.Lütfen belirlediğiniz şifrenin aynısını giriniz")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        public IFormFile GraduationCertificate { get; set; }

        [Required]
        public IFormFile Transkript { get; set; } 
    }
}
