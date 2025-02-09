using Microsoft.AspNetCore.Identity;

namespace ASPCMVC08.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}
