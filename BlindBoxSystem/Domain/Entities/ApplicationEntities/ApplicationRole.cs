using Microsoft.AspNetCore.Identity;

namespace BlindBoxSystem.Domain.Entities.ApplicationEntities
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}
