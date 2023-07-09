using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Models.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Action> Actions { get; set; }

        public virtual DbSet<AgriculturalItem> AgriculturalItems { get; set; }

        public virtual DbSet<Buyer> Buyers { get; set; }

        public virtual DbSet<Engineer> Engineers { get; set; }

        public virtual DbSet<EngineerType> EngineerTypes { get; set; }

        public virtual DbSet<Farmer> Farmers { get; set; }

        public virtual DbSet<Item> Items { get; set; }

        public virtual DbSet<Land> Lands { get; set; }

        public virtual DbSet<LandType> LandTypes { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        public virtual DbSet<Plan> Plans { get; set; }

        public virtual DbSet<Purchase> Purchases { get; set; }

        public virtual DbSet<PurchaseDetail> PurchaseDetails { get; set; }

        public virtual DbSet<SoilType> SoilTypes { get; set; }

        public virtual DbSet<Step> Steps { get; set; }

        public virtual DbSet<StepDetail> StepDetails { get; set; }

        public virtual DbSet<Store> Stores { get; set; }

        public virtual DbSet<StoreType> StoreTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Engineer>(entity =>
            {
                entity.HasOne(d => d.EngineerType).WithMany(p => p.Engineers)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Engineer_Engineer_Type");

                entity.HasOne(d => d.HeadEngineer).WithMany(p => p.InverseHeadEngineer).HasConstraintName("FK_Engineer_Engineer");
            });

            modelBuilder.Entity<EngineerType>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Type");
            });

            modelBuilder.Entity<Land>(entity =>
            {
                entity.HasOne(d => d.Farmer).WithMany(p => p.Lands)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Land_Farmer");

                entity.HasOne(d => d.SoilType).WithMany(p => p.Lands)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Land_Soil_Type");

                entity.HasOne(d => d.Type).WithMany(p => p.Lands)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Land_Land_Type");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(d => d.AdminEngineer).WithMany(p => p.OrderAdminEngineers)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Engineer");

                entity.HasOne(d => d.Buyer).WithMany(p => p.Orders)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Buyer");

                entity.HasOne(d => d.ExecutiveTeam).WithMany(p => p.OrderExecutiveTeams).HasConstraintName("FK_Order_Engineer1");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasOne(d => d.Item).WithMany(p => p.OrderDetails)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Details_Item");

                entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Details_Order");
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Order_Distribution");

                entity.HasOne(d => d.Land).WithMany(p => p.Plans)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Distribution_Land");

                entity.HasOne(d => d.OrderDetails).WithMany(p => p.Plans)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Distribution_Order_Details");
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasOne(d => d.Plan).WithMany(p => p.Purchases)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Purchase_Order_Distribution");

                entity.HasOne(d => d.Store).WithMany(p => p.Purchases)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Purchase_Store");
            });

            modelBuilder.Entity<PurchaseDetail>(entity =>
            {
                entity.HasOne(d => d.Purchase).WithMany(p => p.PurchaseDetails)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Purchase_Details_Purchase");
            });

            modelBuilder.Entity<Step>(entity =>
            {
                entity.HasOne(d => d.Action).WithMany(p => p.Steps)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Step_Action");

                entity.HasOne(d => d.Plan).WithMany(p => p.Steps)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Step_Plan");
            });

            modelBuilder.Entity<StepDetail>(entity =>
            {
                entity.HasOne(d => d.AgriculturalItem).WithMany(p => p.StepDetails)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Step_Details_Agricultural_Item");

                entity.HasOne(d => d.Step).WithMany(p => p.StepDetails)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Step_Details_Step");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasOne(d => d.Type).WithMany(p => p.Stores)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Store_Store_Type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}