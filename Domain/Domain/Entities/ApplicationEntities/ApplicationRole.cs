using Microsoft.AspNetCore.Identity;

namespace Domain.Domain.Entities.ApplicationEntities
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}
