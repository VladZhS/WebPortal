﻿using System;
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

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=WebPortalDB;Trusted_Connection=True;");
            }
        }
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
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
                    .HasConstraintName("FK__Order__CustomerI__46E78A0C");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__StatusId__34C8D9D1");
            });

            modelBuilder.Entity<OrderProduct>(entity =>
            {

                entity.HasOne(d => d.Oder)
                    .WithMany(p => p.OrderProduct)
                    .HasForeignKey(d => d.OderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderProd__OderI__36B12243");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderProduct)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderProd__Produ__37A5467C");
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

                entity.Property(e => e.CreationDate)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Categor__38996AB5");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__SizeId__398D8EEE");
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
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}