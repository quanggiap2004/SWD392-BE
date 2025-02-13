﻿using BlindBoxSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlindBoxSystem.Domain.Context
{
    public class BlindBoxSystemDbContext: DbContext
    {
        public BlindBoxSystemDbContext(DbContextOptions<BlindBoxSystemDbContext> options)
        : base(options)
        {
        }

        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Box> Boxes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Variant> Variants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<OrderStatusDetail> OrderStatusDetails { get; set; }
        public DbSet<UserVotedBoxItem> UserVotedBoxItems { get; set; }
        public DbSet<BoxImage> BoxImages { get; set; }
        public DbSet<BoxVariant> BoxVariants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserWallet> UserWallets { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BoxItem> BoxItems { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<OnlineSerieBox> OnlineSerieBoxes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BoxItem>()
                .HasMany(e => e.OnlineSerieBoxes)
                .WithMany(e => e.BoxItems)
                .UsingEntity("RolledItem");

            modelBuilder.Entity<Box>()
                .HasMany(e => e.Variants)
                .WithMany(e => e.Boxes)
                .UsingEntity<BoxVariant>(
                    j => j
                        .HasOne(bv => bv.Variant)
                        .WithMany(v => v.BoxVariants)
                        .HasForeignKey(bv => bv.VariantId),
                    j => j
                        .HasOne(bv => bv.Box)
                        .WithMany(b => b.BoxVariants)
                        .HasForeignKey(bv => bv.BoxId),
                    j =>
                    {
                        j.HasKey(bv => bv.BoxVariantId);
                        j.ToTable("BoxVariants");
                    });

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderStatus)
                .WithMany(e => e.Orders)
                .UsingEntity<OrderStatusDetail>();

            modelBuilder.Entity<User>()
                .HasOne(u => u.UserWallet)
                .WithOne(uw => uw.User)
                .HasForeignKey<UserWallet>(u => u.WalletId)
                .IsRequired();
        }
    }
}
