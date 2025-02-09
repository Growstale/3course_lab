using System.ComponentModel.DataAnnotations;

namespace ASPCMVC08.Models
{
    public class DeleteRoleViewModel
    {
        public string returnAction { get; set; }
        public string returnController { get; set; }
        public string id { get; set; }
    }
}
