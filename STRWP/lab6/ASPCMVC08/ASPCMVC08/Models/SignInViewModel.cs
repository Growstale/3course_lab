using System.ComponentModel.DataAnnotations;

namespace ASPCMVC08.Models
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Email обязателен для ввода")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Пароль обязателен для ввода")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        public string returnAction { get; set; }
        public string returnController { get; set; }
    }
}
