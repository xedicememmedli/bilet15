using Microsoft.AspNetCore.Identity;

namespace Bilet_15.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }

    }
}
