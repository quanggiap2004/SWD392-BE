﻿// <auto-generated />
using System;
using BlindBoxSystem.Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BlindBoxSystem.Domain.Migrations
{
    [DbContext(typeof(BlindBoxSystemDbContext))]
    partial class BlindBoxSystemDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AddressId"));

                    b.Property<string>("AddressDetail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("Ward")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("AddressId");

                    b.HasIndex("UserId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.BlogPost", b =>
                {
                    b.Property<int>("BlogPostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BlogPostId"));

                    b.Property<string>("BlogPostContent")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BlogPostImage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BlogPostTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("BlogPostId");

                    b.HasIndex("UserId");

                    b.ToTable("BlogPosts");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.Box", b =>
                {
                    b.Property<int>("BoxId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BoxId"));

                    b.Property<string>("BoxDescription")
                        .HasColumnType("text");

                    b.Property<string>("BoxName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("BrandId")
                        .HasColumnType("integer");

                    b.HasKey("BoxId");

                    b.HasIndex("BoxName")
                        .IsUnique();

                    b.HasIndex("BrandId");

                    b.ToTable("Boxes");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.BoxImage", b =>
                {
                    b.Property<int>("BoxImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BoxImageId"));

                    b.Property<int>("BoxId")
                        .HasColumnType("integer");

                    b.Property<string>("BoxImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("BoxImageId");

                    b.HasIndex("BoxId");

                    b.ToTable("BoxImages");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.BoxItem", b =>
                {
                    b.Property<int>("BoxItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BoxItemId"));

                    b.Property<int>("AverageRating")
                        .HasColumnType("integer");

                    b.Property<int>("BoxId")
                        .HasColumnType("integer");

                    b.Property<string>("BoxItemColor")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BoxItemDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BoxItemEyes")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BoxItemName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsSecret")
                        .HasColumnType("boolean");

                    b.Property<int>("NumOfVote")
                        .HasColumnType("integer");

                    b.HasKey("BoxItemId");

                    b.HasIndex("BoxId");

                    b.ToTable("BoxItems");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.BoxVariant", b =>
                {
                    b.Property<int>("BoxVariantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BoxVariantId"));

                    b.Property<int>("BoxId")
                        .HasColumnType("integer");

                    b.Property<string>("BoxVariantName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("BoxVariantPrice")
                        .HasColumnType("real");

                    b.Property<int>("BoxVariantStock")
                        .HasColumnType("integer");

                    b.Property<float>("DisplayPrice")
                        .HasColumnType("real");

                    b.Property<float>("OriginPrice")
                        .HasColumnType("real");

                    b.Property<int>("VariantId")
                        .HasColumnType("integer");

                    b.HasKey("BoxVariantId");

                    b.HasIndex("BoxId");

                    b.HasIndex("VariantId");

                    b.ToTable("BoxVariants", (string)null);
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.Brand", b =>
                {
                    b.Property<int>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BrandId"));

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("BrandId");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FeedbackId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FeedbackContent")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FeedbackType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OrderItemId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("FeedbackId");

                    b.HasIndex("OrderItemId");

                    b.HasIndex("UserId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.OnlineSerieBox", b =>
                {
                    b.Property<int>("OnlineSerieBoxId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OnlineSerieBoxId"));

                    b.Property<int>("BoxId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsSecretOpen")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("Turn")
                        .HasColumnType("integer");

                    b.HasKey("OnlineSerieBoxId");

                    b.HasIndex("BoxId");

                    b.ToTable("OnlineSerieBoxes");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderId"));

                    b.Property<int>("AddressId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsRefund")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("OrderCreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Revenue")
                        .HasColumnType("real");

                    b.Property<float>("TotalPrice")
                        .HasColumnType("real");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("VoucherId")
                        .HasColumnType("integer");

                    b.HasKey("OrderId");

                    b.HasIndex("AddressId");

                    b.HasIndex("UserId");

                    b.HasIndex("VoucherId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderItemId"));

                    b.Property<int>("BoxVariantId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsFeedback")
                        .HasColumnType("boolean");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<float>("OrderPrice")
                        .HasColumnType("real");

                    b.Property<string>("OrderStatusCheckCardImage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("OrderItemId");

                    b.HasIndex("BoxVariantId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.OrderStatus", b =>
                {
                    b.Property<int>("OrderStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderStatusId"));

                    b.Property<string>("OrderStatusName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("OrderStatusId");

                    b.ToTable("OrderStatuses");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.OrderStatusDetail", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("OrderStatusId")
                        .HasColumnType("integer");

                    b.Property<string>("OrderStatusNote")
                        .HasColumnType("text");

                    b.Property<DateTime>("OrderStatusUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("OrderId", "OrderStatusId");

                    b.HasIndex("OrderStatusId");

                    b.ToTable("OrderStatusDetails");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TransactionId"));

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<float>("BalanceAfter")
                        .HasColumnType("real");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("WalletId")
                        .HasColumnType("integer");

                    b.HasKey("TransactionId");

                    b.HasIndex("WalletId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Gender")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.UserVotedBoxItem", b =>
                {
                    b.Property<int>("UserVotedBoxItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserVotedBoxItemId"));

                    b.Property<int>("BoxItemId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("UserVotedBoxItemId");

                    b.HasIndex("BoxItemId");

                    b.HasIndex("UserId");

                    b.ToTable("UserVotedBoxItems");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.UserWallet", b =>
                {
                    b.Property<int>("WalletId")
                        .HasColumnType("integer");

                    b.Property<float>("Balance")
                        .HasColumnType("real");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("WalletId");

                    b.ToTable("UserWallets");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.Variant", b =>
                {
                    b.Property<int>("VariantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("VariantId"));

                    b.Property<string>("VariantName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("VariantId");

                    b.ToTable("Variants");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.Voucher", b =>
                {
                    b.Property<int>("VoucherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("VoucherId"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<float>("MaxDiscount")
                        .HasColumnType("real");

                    b.Property<string>("VoucherCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("VoucherDiscount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("VoucherEndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("VoucherName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("VoucherStartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("VoucherId");

                    b.HasIndex("VoucherCode")
                        .IsUnique();

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("RolledItem", b =>
                {
                    b.Property<int>("BoxItemsBoxItemId")
                        .HasColumnType("integer");

                    b.Property<int>("OnlineSerieBoxesOnlineSerieBoxId")
                        .HasColumnType("integer");

                    b.HasKey("BoxItemsBoxItemId", "OnlineSerieBoxesOnlineSerieBoxId");

                    b.HasIndex("OnlineSerieBoxesOnlineSerieBoxId");

                    b.ToTable("RolledItem");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.Address", b =>
                {
                    b.HasOne("BlindBoxSystem.Domain.Entities.User", "User")
                        .WithMany("Address")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.BlogPost", b =>
                {
                    b.HasOne("BlindBoxSystem.Domain.Entities.User", "User")
                        .WithMany("BlogPosts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.Box", b =>
                {
                    b.HasOne("BlindBoxSystem.Domain.Entities.Brand", "Brand")
                        .WithMany("Box")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.BoxImage", b =>
                {
                    b.HasOne("BlindBoxSystem.Domain.Entities.Box", "Box")
                        .WithMany("BoxImages")
                        .HasForeignKey("BoxId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Box");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.BoxItem", b =>
                {
                    b.HasOne("BlindBoxSystem.Domain.Entities.Box", "Box")
                        .WithMany("BoxItems")
                        .HasForeignKey("BoxId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Box");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.BoxVariant", b =>
                {
                    b.HasOne("BlindBoxSystem.Domain.Entities.Box", "Box")
                        .WithMany("BoxVariants")
                        .HasForeignKey("BoxId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlindBoxSystem.Domain.Entities.Variant", "Variant")
                        .WithMany("BoxVariants")
                        .HasForeignKey("VariantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Box");

                    b.Navigation("Variant");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.Feedback", b =>
                {
                    b.HasOne("BlindBoxSystem.Domain.Entities.OrderItem", "OrderItem")
                        .WithMany("Feedback")
                        .HasForeignKey("OrderItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlindBoxSystem.Domain.Entities.User", "User")
                        .WithMany("Feedbacks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderItem");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.OnlineSerieBox", b =>
                {
                    b.HasOne("BlindBoxSystem.Domain.Entities.Box", "Box")
                        .WithMany("OnlineSerieBoxes")
                        .HasForeignKey("BoxId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Box");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.Order", b =>
                {
                    b.HasOne("BlindBoxSystem.Domain.Entities.Address", "Address")
                        .WithMany("Orders")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlindBoxSystem.Domain.Entities.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlindBoxSystem.Domain.Entities.Voucher", "Voucher")
                        .WithMany("Orders")
                        .HasForeignKey("VoucherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("User");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.OrderItem", b =>
                {
                    b.HasOne("BlindBoxSystem.Domain.Entities.BoxVariant", "BoxVariant")
                        .WithMany("OrderItem")
                        .HasForeignKey("BoxVariantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlindBoxSystem.Domain.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BoxVariant");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.OrderStatusDetail", b =>
                {
                    b.HasOne("BlindBoxSystem.Domain.Entities.Order", "Order")
                        .WithMany("OrderStatusDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlindBoxSystem.Domain.Entities.OrderStatus", "OrderStatus")
                        .WithMany("OrderStatusDetail")
                        .HasForeignKey("OrderStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("OrderStatus");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("BlindBoxSystem.Domain.Entities.UserWallet", "UserWallet")
                        .WithMany("Transactions")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserWallet");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.User", b =>
                {
                    b.HasOne("BlindBoxSystem.Domain.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.UserVotedBoxItem", b =>
                {
                    b.HasOne("BlindBoxSystem.Domain.Entities.BoxItem", "BoxItem")
                        .WithMany("UserVotedBoxItems")
                        .HasForeignKey("BoxItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlindBoxSystem.Domain.Entities.User", "User")
                        .WithMany("UserVotedBoxItems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BoxItem");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.UserWallet", b =>
                {
                    b.HasOne("BlindBoxSystem.Domain.Entities.User", "User")
                        .WithOne("UserWallet")
                        .HasForeignKey("BlindBoxSystem.Domain.Entities.UserWallet", "WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RolledItem", b =>
                {
                    b.HasOne("BlindBoxSystem.Domain.Entities.BoxItem", null)
                        .WithMany()
                        .HasForeignKey("BoxItemsBoxItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlindBoxSystem.Domain.Entities.OnlineSerieBox", null)
                        .WithMany()
                        .HasForeignKey("OnlineSerieBoxesOnlineSerieBoxId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.Address", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.Box", b =>
                {
                    b.Navigation("BoxImages");

                    b.Navigation("BoxItems");

                    b.Navigation("BoxVariants");

                    b.Navigation("OnlineSerieBoxes");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.BoxItem", b =>
                {
                    b.Navigation("UserVotedBoxItems");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.BoxVariant", b =>
                {
                    b.Navigation("OrderItem");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.Brand", b =>
                {
                    b.Navigation("Box");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("OrderStatusDetails");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.OrderItem", b =>
                {
                    b.Navigation("Feedback");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.OrderStatus", b =>
                {
                    b.Navigation("OrderStatusDetail");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.User", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("BlogPosts");

                    b.Navigation("Feedbacks");

                    b.Navigation("Orders");

                    b.Navigation("UserVotedBoxItems");

                    b.Navigation("UserWallet");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.UserWallet", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.Variant", b =>
                {
                    b.Navigation("BoxVariants");
                });

            modelBuilder.Entity("BlindBoxSystem.Domain.Entities.Voucher", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
