using System.ComponentModel.DataAnnotations;

namespace ASPCMVC08.Models
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Email обязателен для ввода")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Год обязателен для ввода")]
        [Range(1900, 2019, ErrorMessage = "Введите корректный год рождения")]
        [Display(Name = "Год рождения")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Пароль обязателен для ввода")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        public string returnAction { get; set; }
        public string returnController { get; set; }

    }
}
