using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebPortalServer
{
    public partial class WebPortalDBContext : DbContext
    {
        public WebPortalDBContext()
        {
        }

        public WebPortalDBContext(DbContextOptions<WebPortalDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderProduct> OrderProduct { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductSize> ProductSize { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("date");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__CustomerI__59063A47");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__StatusId__5812160E");
            });

            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Oder)
                    .WithMany(p => p.OrderProduct)
                    .HasForeignKey(d => d.OderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderProd__OderI__59FA5E80");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderProduct)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderProd__Produ__5AEE82B9");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('New')");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Categor__5BE2A6F2");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__SizeId__5CD6CB2B");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(10);

            });

            modelBuilder.Entity<ProductSize>(entity =>
            {
                entity.Property(e => e.Size)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Medium')");
            });

            OnModelCreatingPartial(modelBuilder);

            //base.OnModelCreating(modelBuilder);
        }

        void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            SetupSizes(modelBuilder);
            SetupStatuses(modelBuilder);
            SetupCategories(modelBuilder);


        }

        void SetupSizes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductSize>().HasData(
                new ProductSize[]
                {
                    new ProductSize { Id = 1, Size = "Tiny" },
                    new ProductSize { Id = 2, Size = "Small" },
                    new ProductSize { Id = 3, Size = "Medium" },
                    new ProductSize { Id = 4, Size = "Big" },
                    new ProductSize { Id = 5, Size = "Large" },
                });
        }

        void SetupStatuses(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderStatus>().HasData(
                new OrderStatus[]
                {
                    new OrderStatus { Id = 1, Status = "New" },
                    new OrderStatus { Id = 2, Status = "Processing" },
                    new OrderStatus { Id = 3, Status = "Delivering" },
                    new OrderStatus { Id = 4, Status = "Done" },
                });
        }

        void SetupCategories(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory[]
                {
                    //new ProductCategory { Id = 1, Category = "" },
                });
        }
    }
}