﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistance.Restaurant.Infrastructure.Context;

#nullable disable

namespace Persistance.Restaurant.Infrastructure.Migrations
{
    [DbContext(typeof(RestContext))]
    [Migration("20240323180508_FixingKeys")]
    partial class FixingKeys
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Restaurant.Core.Domain.Entities.Ingridients", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ingridients", (string)null);
                });

            modelBuilder.Entity("Restaurant.Core.Domain.Entities.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OrderStatus", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "In process"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Completed"
                        });
                });

            modelBuilder.Entity("Restaurant.Core.Domain.Entities.Orders", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderStatusId")
                        .HasColumnType("int");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TableId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderStatusId");

                    b.HasIndex("TableId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("Restaurant.Core.Domain.Entities.PlateOrders", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("PlateId")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "PlateId");

                    b.HasIndex("PlateId");

                    b.ToTable("PlateOrders", (string)null);
                });

            modelBuilder.Entity("Restaurant.Core.Domain.Entities.Plates", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PeopleAmount")
                        .HasColumnType("int");

                    b.Property<int>("PlateCategoriesId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("PlateCategoriesId");

                    b.ToTable("Plates", (string)null);
                });

            modelBuilder.Entity("Restaurant.Core.Domain.Entities.PlatesCategories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PlateCategories", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Entry"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Strong Plate"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Drinks"
                        });
                });

            modelBuilder.Entity("Restaurant.Core.Domain.Entities.PlatesIngridients", b =>
                {
                    b.Property<int>("PlateId")
                        .HasColumnType("int");

                    b.Property<int>("IngridientsId")
                        .HasColumnType("int");

                    b.HasKey("PlateId", "IngridientsId");

                    b.HasIndex("IngridientsId");

                    b.ToTable("PlateIngridients", (string)null);
                });

            modelBuilder.Entity("Restaurant.Core.Domain.Entities.TableStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TableStatus", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Available"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Attention process"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Attended"
                        });
                });

            modelBuilder.Entity("Restaurant.Core.Domain.Entities.Tables", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PeopleAmount")
                        .HasColumnType("int");

                    b.Property<int>("TableStatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TableStatusId");

                    b.ToTable("Tables", (string)null);
                });

            modelBuilder.Entity("Restaurant.Core.Domain.Entities.Orders", b =>
                {
                    b.HasOne("Restaurant.Core.Domain.Entities.OrderStatus", "OrderStatus")
                        .WithMany("Orders")
                        .HasForeignKey("OrderStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Restaurant.Core.Domain.Entities.Tables", "Table")
                        .WithMany("Orders")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderStatus");

                    b.Navigation("Table");
                });

            modelBuilder.Entity("Restaurant.Core.Domain.Entities.PlateOrders", b =>
                {
                    b.HasOne("Restaurant.Core.Domain.Entities.Orders", "Orders")
                        .WithMany("PlateOrders")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Restaurant.Core.Domain.Entities.Plates", "Plates")
                        .WithMany("PlateOrders")
                        .HasForeignKey("PlateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orders");

                    b.Navigation("Plates");
                });

            modelBuilder.Entity("Restaurant.Core.Domain.Entities.Plates", b =>
                {
                    b.HasOne("Restaurant.Core.Domain.Entities.PlatesCategories", "PlatesCategories")
                        .WithMany("Plates")
                        .HasForeignKey("PlateCategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlatesCategories");
                });

            modelBuilder.Entity("Restaurant.Core.Domain.Entities.PlatesIngridients", b =>
                {
                    b.HasOne("Restaurant.Core.Domain.Entities.Ingridients", "Ingridients")
                        .WithMany("PlatesIngridients")
                        .HasForeignKey("IngridientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Restaurant.Core.Domain.Entities.Plates", "Plates")
                        .WithMany("PlatesIngridients")
                        .HasForeignKey("IngridientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingridients");

                    b.Navigation("Plates");
                });

            modelBuilder.Entity("Restaurant.Core.Domain.Entities.Tables", b =>
                {
                    b.HasOne("Restaurant.Core.Domain.Entities.TableStatus", "Status")
                        .WithMany("Tables")
                        .HasForeignKey("TableStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Restaurant.Core.Domain.Entities.Ingridients", b =>
                {
                    b.Navigation("PlatesIngridients");
                });

            modelBuilder.Entity("Restaurant.Core.Domain.Entities.OrderStatus", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Restaurant.Core.Domain.Entities.Orders", b =>
                {
                    b.Navigation("PlateOrders");
                });

            modelBuilder.Entity("Restaurant.Core.Domain.Entities.Plates", b =>
                {
                    b.Navigation("PlateOrders");

                    b.Navigation("PlatesIngridients");
                });

            modelBuilder.Entity("Restaurant.Core.Domain.Entities.PlatesCategories", b =>
                {
                    b.Navigation("Plates");
                });

            modelBuilder.Entity("Restaurant.Core.Domain.Entities.TableStatus", b =>
                {
                    b.Navigation("Tables");
                });

            modelBuilder.Entity("Restaurant.Core.Domain.Entities.Tables", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
