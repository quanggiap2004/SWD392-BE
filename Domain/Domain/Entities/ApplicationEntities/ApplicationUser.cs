using Microsoft.AspNetCore.Identity;

namespace Domain.Domain.Entities.ApplicationEntities
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsActive { get; set; } = true;
        public string FullName { get; set; }
        public string Password { get; set; }
    }
}
