﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlusMinus.DAL;

namespace PlusMinus.DAL.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PlusMinus.Core.Models.Exercise", b =>
                {
                    b.Property<int>("ExerciseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("ExerciseId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("PlusMinus.Core.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("PlusMinus.Core.Models.OrderProduct", b =>
                {
                    b.Property<int>("OrderProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("OrderProductId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("PlusMinus.Core.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Product");
                });

            modelBuilder.Entity("PlusMinus.Core.Models.Timetable", b =>
                {
                    b.Property<int>("TimetableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TimetableId");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("UserId");

                    b.ToTable("Timetables");
                });

            modelBuilder.Entity("PlusMinus.Core.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Receipt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("SightLeft")
                        .HasColumnType("float");

                    b.Property<double>("SightRight")
                        .HasColumnType("float");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PlusMinus.Core.Models.Accessory", b =>
                {
                    b.HasBaseType("PlusMinus.Core.Models.Product");

                    b.Property<int>("AccessoryType")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Accessory_Color");

                    b.Property<string>("Material")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Accessory_Material");

                    b.HasDiscriminator().HasValue("Accessory");
                });

            modelBuilder.Entity("PlusMinus.Core.Models.Eyecare", b =>
                {
                    b.HasBaseType("PlusMinus.Core.Models.Product");

                    b.Property<int>("Purpose")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Eyecare");
                });

            modelBuilder.Entity("PlusMinus.Core.Models.Frame", b =>
                {
                    b.HasBaseType("PlusMinus.Core.Models.Product");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Frame_Color");

                    b.Property<int>("Form")
                        .HasColumnType("int")
                        .HasColumnName("Frame_Form");

                    b.Property<string>("Material")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Frame_Material");

                    b.HasDiscriminator().HasValue("Frame");
                });

            modelBuilder.Entity("PlusMinus.Core.Models.Glasses", b =>
                {
                    b.HasBaseType("PlusMinus.Core.Models.Product");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Dioptre")
                        .HasColumnType("float")
                        .HasColumnName("Glasses_Dioptre");

                    b.Property<int>("Form")
                        .HasColumnType("int");

                    b.Property<string>("Material")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Glasses");
                });

            modelBuilder.Entity("PlusMinus.Core.Models.Lenses", b =>
                {
                    b.HasBaseType("PlusMinus.Core.Models.Product");

                    b.Property<double>("Dioptre")
                        .HasColumnType("float");

                    b.Property<TimeSpan>("ExpirationDate")
                        .HasColumnType("time");

                    b.HasDiscriminator().HasValue("Lenses");
                });

            modelBuilder.Entity("PlusMinus.Core.Models.Order", b =>
                {
                    b.HasOne("PlusMinus.Core.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlusMinus.Core.Models.OrderProduct", b =>
                {
                    b.HasOne("PlusMinus.Core.Models.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlusMinus.Core.Models.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("PlusMinus.Core.Models.Timetable", b =>
                {
                    b.HasOne("PlusMinus.Core.Models.Exercise", "Exercise")
                        .WithMany("Timetables")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlusMinus.Core.Models.User", "User")
                        .WithMany("Timetables")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlusMinus.Core.Models.Exercise", b =>
                {
                    b.Navigation("Timetables");
                });

            modelBuilder.Entity("PlusMinus.Core.Models.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("PlusMinus.Core.Models.Product", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("PlusMinus.Core.Models.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Timetables");
                });
#pragma warning restore 612, 618
        }
    }
}
