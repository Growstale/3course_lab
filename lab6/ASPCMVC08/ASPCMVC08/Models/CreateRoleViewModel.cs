using System.ComponentModel.DataAnnotations;

namespace ASPCMVC08.Models
{
    public class CreateRoleViewModel
    {
        public string returnAction { get; set; }
        public string returnController { get; set; }
        [Required(ErrorMessage = "Имя роли обязательно для ввода")]
        [Display(Name = "Имя роли")]
        public string name { get; set; }
    }
}
