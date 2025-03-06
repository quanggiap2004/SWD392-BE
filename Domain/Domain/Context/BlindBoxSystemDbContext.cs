using Domain.Domain.Entities;
using Domain.Domain.Entities.ApplicationEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain.Domain.Context
{
    public class BlindBoxSystemDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public BlindBoxSystemDbContext(DbContextOptions<BlindBoxSystemDbContext> options)
        : base(options)
        {
        }

        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Box> Boxes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<OrderStatusDetail> OrderStatusDetails { get; set; }
        public DbSet<UserVotedBoxItem> UserVotedBoxItems { get; set; }
        public DbSet<BoxImage> BoxImages { get; set; }
        public DbSet<BoxOption> BoxOptions { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BoxItem> BoxItems { get; set; }
        public DbSet<OnlineSerieBox> OnlineSerieBoxes { get; set; }
        public DbSet<CurrentRolledItem> CurrentRolledItems { get; set; }
        public DbSet<UserRolledItem> UserRolledItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderStatus)
                .WithMany(e => e.Orders)
                .UsingEntity<OrderStatusDetail>();
        }
    }
}
