using System.ComponentModel.DataAnnotations;

namespace ASPCMVC08.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Пароль обязателен для ввода")]
        [DataType(DataType.Password)]
        [Display(Name = "Текущий пароль")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Новый пароль обязателен для ввода")]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Подтвеждение нового пароля обязательно для ввода")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите новый пароль")]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmNewPassword { get; set; }

        public string returnAction { get; set; }
        public string returnController { get; set; }

    }
}
