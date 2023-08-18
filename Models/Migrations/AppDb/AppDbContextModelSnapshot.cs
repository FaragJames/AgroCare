﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models.Models;

#nullable disable

namespace Models.Migrations.AppDb
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.Models.Action", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Action");
                });

            modelBuilder.Entity("Models.Models.AgriculturalItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Agricultural_Item");
                });

            modelBuilder.Entity("Models.Models.Buyer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Buyer");
                });

            modelBuilder.Entity("Models.Models.Engineer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EngineerTypeId")
                        .HasColumnType("int")
                        .HasColumnName("Engineer_Type_Id");

                    b.Property<int?>("HeadEngineerId")
                        .HasColumnType("int")
                        .HasColumnName("Head_Engineer_Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<float>("Salary")
                        .HasColumnType("real");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("EngineerTypeId");

                    b.HasIndex("HeadEngineerId");

                    b.ToTable("Engineer");
                });

            modelBuilder.Entity("Models.Models.EngineerType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .HasName("PK_Type");

                    b.ToTable("Engineer_Type");
                });

            modelBuilder.Entity("Models.Models.Farmer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Farmer");
                });

            modelBuilder.Entity("Models.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Image_Path");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("Models.Models.Land", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Area")
                        .HasColumnType("real");

                    b.Property<int>("FarmerId")
                        .HasColumnType("int")
                        .HasColumnName("Farmer_Id");

                    b.Property<bool>("HasWell")
                        .HasColumnType("bit")
                        .HasColumnName("Has_Well");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("SoilTypeId")
                        .HasColumnType("int")
                        .HasColumnName("Soil_Type_Id");

                    b.Property<int>("TypeId")
                        .HasColumnType("int")
                        .HasColumnName("Type_Id");

                    b.HasKey("Id");

                    b.HasIndex("FarmerId");

                    b.HasIndex("SoilTypeId");

                    b.HasIndex("TypeId");

                    b.ToTable("Land");
                });

            modelBuilder.Entity("Models.Models.LandType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Land_Type");
                });

            modelBuilder.Entity("Models.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdminEngineerId")
                        .HasColumnType("int")
                        .HasColumnName("Admin_Engineer_Id");

                    b.Property<int>("BuyerId")
                        .HasColumnType("int")
                        .HasColumnName("Buyer_Id");

                    b.Property<bool>("ClickedByAdmin")
                        .HasColumnType("bit")
                        .HasColumnName("Clicked_By_Admin");

                    b.Property<bool>("ClickedByBuyer")
                        .HasColumnType("bit")
                        .HasColumnName("Clicked_By_Buyer");

                    b.Property<bool>("ClickedByTeam")
                        .HasColumnType("bit")
                        .HasColumnName("Clicked_By_Team");

                    b.Property<int?>("ExecutiveTeamId")
                        .HasColumnType("int")
                        .HasColumnName("Executive_Team_Id");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("date")
                        .HasColumnName("Order_Date");

                    b.HasKey("Id");

                    b.HasIndex("AdminEngineerId");

                    b.HasIndex("BuyerId");

                    b.HasIndex("ExecutiveTeamId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Models.Models.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DeliveryDate")
                        .HasColumnType("date")
                        .HasColumnName("Delivery_Date");

                    b.Property<string>("Feedback")
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("ItemId")
                        .HasColumnType("int")
                        .HasColumnName("Item_Id");

                    b.Property<float>("KiloPrice")
                        .HasColumnType("real")
                        .HasColumnName("Kilo_Price");

                    b.Property<int>("Kilos")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("Order_Id");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("Order_Details");
                });

            modelBuilder.Entity("Models.Models.Plan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("ClickByFarmer")
                        .HasColumnType("bit")
                        .HasColumnName("Clicked_By_Farmer");

                    b.Property<DateTime>("FinishDate")
                        .HasColumnType("date")
                        .HasColumnName("Finish_Date");

                    b.Property<int>("LandId")
                        .HasColumnType("int")
                        .HasColumnName("Land_Id");

                    b.Property<int>("OrderDetailsId")
                        .HasColumnType("int")
                        .HasColumnName("Order_Details_Id");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date")
                        .HasColumnName("Start_Date");

                    b.HasKey("Id")
                        .HasName("PK_Order_Distribution");

                    b.HasIndex("LandId");

                    b.HasIndex("OrderDetailsId");

                    b.ToTable("Plan");
                });

            modelBuilder.Entity("Models.Models.Purchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<int>("PlanId")
                        .HasColumnType("int")
                        .HasColumnName("Plan_Id");

                    b.Property<int>("StoreId")
                        .HasColumnType("int")
                        .HasColumnName("Store_Id");

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.HasIndex("StoreId");

                    b.ToTable("Purchase");
                });

            modelBuilder.Entity("Models.Models.PurchaseDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Details")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsRented")
                        .HasColumnType("bit")
                        .HasColumnName("Is_Rented");

                    b.Property<string>("Item")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("PurchaseId")
                        .HasColumnType("int")
                        .HasColumnName("Purchase_Id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PurchaseId");

                    b.ToTable("Purchase_Details");
                });

            modelBuilder.Entity("Models.Models.SoilType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Soil_Type");
                });

            modelBuilder.Entity("Models.Models.Step", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ActionId")
                        .HasColumnType("int")
                        .HasColumnName("Action_Id");

                    b.Property<DateTime>("EstimatedFinishDate")
                        .HasColumnType("date")
                        .HasColumnName("Estimated_Finish_Date");

                    b.Property<DateTime>("EstimatedStartDate")
                        .HasColumnType("date")
                        .HasColumnName("Estimated_Start_Date");

                    b.Property<bool>("IsChecked")
                        .HasColumnType("bit")
                        .HasColumnName("Is_Checked");

                    b.Property<int>("PlanId")
                        .HasColumnType("int")
                        .HasColumnName("Plan_Id");

                    b.HasKey("Id");

                    b.HasIndex("ActionId");

                    b.HasIndex("PlanId");

                    b.ToTable("Step");
                });

            modelBuilder.Entity("Models.Models.StepDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AgriculturalItemId")
                        .HasColumnType("int")
                        .HasColumnName("Agricultural_Item_Id");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<int>("StepId")
                        .HasColumnType("int")
                        .HasColumnName("Step_Id");

                    b.HasKey("Id");

                    b.HasIndex("AgriculturalItemId");

                    b.HasIndex("StepId");

                    b.ToTable("Step_Details");
                });

            modelBuilder.Entity("Models.Models.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Image_Path");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int")
                        .HasColumnName("Type_Id");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Store");
                });

            modelBuilder.Entity("Models.Models.StoreType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Store_Type");
                });

            modelBuilder.Entity("Models.Models.Engineer", b =>
                {
                    b.HasOne("Models.Models.EngineerType", "EngineerType")
                        .WithMany("Engineers")
                        .HasForeignKey("EngineerTypeId")
                        .IsRequired()
                        .HasConstraintName("FK_Engineer_Engineer_Type");

                    b.HasOne("Models.Models.Engineer", "HeadEngineer")
                        .WithMany("InverseHeadEngineer")
                        .HasForeignKey("HeadEngineerId")
                        .HasConstraintName("FK_Engineer_Engineer");

                    b.Navigation("EngineerType");

                    b.Navigation("HeadEngineer");
                });

            modelBuilder.Entity("Models.Models.Land", b =>
                {
                    b.HasOne("Models.Models.Farmer", "Farmer")
                        .WithMany("Lands")
                        .HasForeignKey("FarmerId")
                        .IsRequired()
                        .HasConstraintName("FK_Land_Farmer");

                    b.HasOne("Models.Models.SoilType", "SoilType")
                        .WithMany("Lands")
                        .HasForeignKey("SoilTypeId")
                        .IsRequired()
                        .HasConstraintName("FK_Land_Soil_Type");

                    b.HasOne("Models.Models.LandType", "Type")
                        .WithMany("Lands")
                        .HasForeignKey("TypeId")
                        .IsRequired()
                        .HasConstraintName("FK_Land_Land_Type");

                    b.Navigation("Farmer");

                    b.Navigation("SoilType");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Models.Models.Order", b =>
                {
                    b.HasOne("Models.Models.Engineer", "AdminEngineer")
                        .WithMany("OrderAdminEngineers")
                        .HasForeignKey("AdminEngineerId")
                        .IsRequired()
                        .HasConstraintName("FK_Order_Engineer");

                    b.HasOne("Models.Models.Buyer", "Buyer")
                        .WithMany("Orders")
                        .HasForeignKey("BuyerId")
                        .IsRequired()
                        .HasConstraintName("FK_Order_Buyer");

                    b.HasOne("Models.Models.Engineer", "ExecutiveTeam")
                        .WithMany("OrderExecutiveTeams")
                        .HasForeignKey("ExecutiveTeamId")
                        .HasConstraintName("FK_Order_Engineer1");

                    b.Navigation("AdminEngineer");

                    b.Navigation("Buyer");

                    b.Navigation("ExecutiveTeam");
                });

            modelBuilder.Entity("Models.Models.OrderDetail", b =>
                {
                    b.HasOne("Models.Models.Item", "Item")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ItemId")
                        .IsRequired()
                        .HasConstraintName("FK_Order_Details_Item");

                    b.HasOne("Models.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK_Order_Details_Order");

                    b.Navigation("Item");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Models.Models.Plan", b =>
                {
                    b.HasOne("Models.Models.Land", "Land")
                        .WithMany("Plans")
                        .HasForeignKey("LandId")
                        .IsRequired()
                        .HasConstraintName("FK_Order_Distribution_Land");

                    b.HasOne("Models.Models.OrderDetail", "OrderDetails")
                        .WithMany("Plans")
                        .HasForeignKey("OrderDetailsId")
                        .IsRequired()
                        .HasConstraintName("FK_Order_Distribution_Order_Details");

                    b.Navigation("Land");

                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Models.Models.Purchase", b =>
                {
                    b.HasOne("Models.Models.Plan", "Plan")
                        .WithMany("Purchases")
                        .HasForeignKey("PlanId")
                        .IsRequired()
                        .HasConstraintName("FK_Purchase_Order_Distribution");

                    b.HasOne("Models.Models.Store", "Store")
                        .WithMany("Purchases")
                        .HasForeignKey("StoreId")
                        .IsRequired()
                        .HasConstraintName("FK_Purchase_Store");

                    b.Navigation("Plan");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("Models.Models.PurchaseDetail", b =>
                {
                    b.HasOne("Models.Models.Purchase", "Purchase")
                        .WithMany("PurchaseDetails")
                        .HasForeignKey("PurchaseId")
                        .IsRequired()
                        .HasConstraintName("FK_Purchase_Details_Purchase");

                    b.Navigation("Purchase");
                });

            modelBuilder.Entity("Models.Models.Step", b =>
                {
                    b.HasOne("Models.Models.Action", "Action")
                        .WithMany("Steps")
                        .HasForeignKey("ActionId")
                        .IsRequired()
                        .HasConstraintName("FK_Step_Action");

                    b.HasOne("Models.Models.Plan", "Plan")
                        .WithMany("Steps")
                        .HasForeignKey("PlanId")
                        .IsRequired()
                        .HasConstraintName("FK_Step_Plan");

                    b.Navigation("Action");

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("Models.Models.StepDetail", b =>
                {
                    b.HasOne("Models.Models.AgriculturalItem", "AgriculturalItem")
                        .WithMany("StepDetails")
                        .HasForeignKey("AgriculturalItemId")
                        .IsRequired()
                        .HasConstraintName("FK_Step_Details_Agricultural_Item");

                    b.HasOne("Models.Models.Step", "Step")
                        .WithMany("StepDetails")
                        .HasForeignKey("StepId")
                        .IsRequired()
                        .HasConstraintName("FK_Step_Details_Step");

                    b.Navigation("AgriculturalItem");

                    b.Navigation("Step");
                });

            modelBuilder.Entity("Models.Models.Store", b =>
                {
                    b.HasOne("Models.Models.StoreType", "Type")
                        .WithMany("Stores")
                        .HasForeignKey("TypeId")
                        .IsRequired()
                        .HasConstraintName("FK_Store_Store_Type");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Models.Models.Action", b =>
                {
                    b.Navigation("Steps");
                });

            modelBuilder.Entity("Models.Models.AgriculturalItem", b =>
                {
                    b.Navigation("StepDetails");
                });

            modelBuilder.Entity("Models.Models.Buyer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Models.Models.Engineer", b =>
                {
                    b.Navigation("InverseHeadEngineer");

                    b.Navigation("OrderAdminEngineers");

                    b.Navigation("OrderExecutiveTeams");
                });

            modelBuilder.Entity("Models.Models.EngineerType", b =>
                {
                    b.Navigation("Engineers");
                });

            modelBuilder.Entity("Models.Models.Farmer", b =>
                {
                    b.Navigation("Lands");
                });

            modelBuilder.Entity("Models.Models.Item", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Models.Models.Land", b =>
                {
                    b.Navigation("Plans");
                });

            modelBuilder.Entity("Models.Models.LandType", b =>
                {
                    b.Navigation("Lands");
                });

            modelBuilder.Entity("Models.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Models.Models.OrderDetail", b =>
                {
                    b.Navigation("Plans");
                });

            modelBuilder.Entity("Models.Models.Plan", b =>
                {
                    b.Navigation("Purchases");

                    b.Navigation("Steps");
                });

            modelBuilder.Entity("Models.Models.Purchase", b =>
                {
                    b.Navigation("PurchaseDetails");
                });

            modelBuilder.Entity("Models.Models.SoilType", b =>
                {
                    b.Navigation("Lands");
                });

            modelBuilder.Entity("Models.Models.Step", b =>
                {
                    b.Navigation("StepDetails");
                });

            modelBuilder.Entity("Models.Models.Store", b =>
                {
                    b.Navigation("Purchases");
                });

            modelBuilder.Entity("Models.Models.StoreType", b =>
                {
                    b.Navigation("Stores");
                });
#pragma warning restore 612, 618
        }
    }
}
