using Microsoft.AspNetCore.Identity;

namespace MNF_PORTAL_Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
