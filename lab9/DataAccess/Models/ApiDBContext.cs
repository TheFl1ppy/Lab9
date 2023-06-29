﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BackendApi.Models
{
    public partial class ApiDBContext : DbContext
    {
        public ApiDBContext()
        {
        }

        public ApiDBContext(DbContextOptions<ApiDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Delivery> Deliveries { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrdersItem> OrdersItems { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Spec> Specs { get; set; } = null!;
        public virtual DbSet<SpecsItem> SpecsItems { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.CartsId);

                entity.Property(e => e.CartsId)
                    .ValueGeneratedNever()
                    .HasColumnName("carts_id");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserCarts");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemsCarts");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId)
                    .ValueGeneratedNever()
                    .HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .HasColumnName("category_name");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            });

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.ToTable("Delivery");

                entity.Property(e => e.DeliveryId)
                    .ValueGeneratedNever()
                    .HasColumnName("delivery_id");

                entity.Property(e => e.DeliveryType)
                    .HasMaxLength(50)
                    .HasColumnName("delivery_type");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("order_id");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.DeliveryId).HasColumnName("delivery_id");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.HasOne(d => d.Delivery)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.DeliveryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Delivery");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsersOrders");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Status");
            });

            modelBuilder.Entity<OrdersItem>(entity =>
            {
                entity.HasKey(e => e.OrderItemId);

                entity.ToTable("Orders_item");

                entity.Property(e => e.OrderItemId)
                    .ValueGeneratedNever()
                    .HasColumnName("order_item_id");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.OrdersItems)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemsOrders_item");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.Property(e => e.ItemId)
                    .ValueGeneratedNever()
                    .HasColumnName("item_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.ItemDescription)
                    .HasMaxLength(100)
                    .HasColumnName("item_description");

                entity.Property(e => e.ItemName)
                    .HasMaxLength(50)
                    .HasColumnName("item_name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.WarehouseQuantity).HasColumnName("warehouse_quantity");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Products_Categories");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("role_id");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<Spec>(entity =>
            {
                entity.Property(e => e.SpecId)
                    .ValueGeneratedNever()
                    .HasColumnName("spec_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.SpecName)
                    .HasMaxLength(50)
                    .HasColumnName("spec_name");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Specs)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Specs_Categories");
            });

            modelBuilder.Entity<SpecsItem>(entity =>
            {
                entity.HasKey(e => e.SpecItemId);

                entity.ToTable("Specs_item");

                entity.Property(e => e.SpecItemId)
                    .ValueGeneratedNever()
                    .HasColumnName("spec_item_id");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.SpecId).HasColumnName("spec_id");

                entity.Property(e => e.SpecValue)
                    .HasMaxLength(50)
                    .HasColumnName("spec_value");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.SpecsItems)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemsSpecs_item");

                entity.HasOne(d => d.Spec)
                    .WithMany(p => p.SpecsItems)
                    .HasForeignKey(d => d.SpecId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpecsSpecs_item");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.StatusId)
                    .ValueGeneratedNever()
                    .HasColumnName("status_id");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.StatusType)
                    .HasMaxLength(50)
                    .HasColumnName("status_type");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.id_user);

                entity.Property(e => e.id_user)
                    .ValueGeneratedNever()
                    .HasColumnName("id_user");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .HasColumnName("login");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
