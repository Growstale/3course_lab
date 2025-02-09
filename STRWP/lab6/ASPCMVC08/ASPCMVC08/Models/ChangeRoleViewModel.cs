using Microsoft.AspNetCore.Identity;

namespace ASPCMVC08.Models
{
    public class ChangeRoleViewModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public IList<string> ChosenRoles { get; set; }

        public string returnAction { get; set; }
        public string returnController { get; set; }

        public ChangeRoleViewModel()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}
