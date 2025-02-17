using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlindBoxSystem.Domain.Context
{
    public class IdentityServiceContext : IdentityDbContext

    {
        public IdentityServiceContext(DbContextOptions<IdentityServiceContext> options) : base(options)
        {

        }
    }
}
