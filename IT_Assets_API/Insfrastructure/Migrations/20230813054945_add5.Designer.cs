﻿// <auto-generated />
using System;
using Insfrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Insfrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230813054945_add5")]
    partial class add5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DomainModelEntity.Models.Admin", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fullname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("DomainModelEntity.Models.AssetTypes", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("asset_type_code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("asset_type_description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("AssetTypes");
                });

            modelBuilder.Entity("DomainModelEntity.Models.EmployeeAssets", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("asset_id")
                        .HasColumnType("int");

                    b.Property<string>("condition_out")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("condition_returned")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("date_out")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("date_returned")
                        .HasColumnType("datetime2");

                    b.Property<int>("employee_id")
                        .HasColumnType("int");

                    b.Property<string>("other_details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("status")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.ToTable("EmployeeAssets");
                });

            modelBuilder.Entity("DomainModelEntity.Models.Employees", b =>
                {
                    b.Property<int>("employee_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("employee_id"), 1L, 1);

                    b.Property<string>("avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("department")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email_address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("first_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("last_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("other_details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("employee_id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("DomainModelEntity.Models.ITAssetInventory", b =>
                {
                    b.Property<int>("it_asset_inventory_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("it_asset_inventory_id"), 1L, 1);

                    b.Property<int>("asset_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("inventory_date")
                        .HasColumnType("datetime2");

                    b.Property<int>("number_assigned")
                        .HasColumnType("int");

                    b.Property<int>("number_in_stock")
                        .HasColumnType("int");

                    b.Property<string>("other_details")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("it_asset_inventory_id");

                    b.ToTable("ITAssetInventory");
                });

            modelBuilder.Entity("DomainModelEntity.Models.ITAssets", b =>
                {
                    b.Property<int>("asset_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("asset_id"), 1L, 1);

                    b.Property<string>("assetImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("asset_type_code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("other_details")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("asset_id");

                    b.ToTable("ITAssets");
                });
#pragma warning restore 612, 618
        }
    }
}
