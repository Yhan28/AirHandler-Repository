﻿// <auto-generated />
using System;
using AirHandlers.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AirHandlers.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.36");

            modelBuilder.Entity("AirHandlers.Domain.Entities.AirHandler", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FilterChangeDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("IdentifierCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsOperating")
                        .HasColumnType("INTEGER");

                    b.Property<double>("ReferenceHumidity")
                        .HasColumnType("REAL");

                    b.Property<double>("ReferenceTemperature")
                        .HasColumnType("REAL");

                    b.HasKey("ID");

                    b.ToTable("AirHandlers", (string)null);
                });

            modelBuilder.Entity("AirHandlers.Domain.Entities.Recipe", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<double>("ReferenceHumidity")
                        .HasColumnType("REAL");

                    b.Property<double>("ReferenceTemperature")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Recipes", (string)null);
                });

            modelBuilder.Entity("AirHandlers.Domain.Entities.Room", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AssociatedHandlerId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Volume")
                        .HasColumnType("REAL");

                    b.HasKey("ID");

                    b.HasIndex("AssociatedHandlerId");

                    b.ToTable("Rooms", (string)null);
                });

            modelBuilder.Entity("AirHandlers.Domain.Relations.AirHandlerRecipe", b =>
                {
                    b.Property<Guid>("AirHandlerID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RecipeID")
                        .HasColumnType("TEXT");

                    b.HasKey("AirHandlerID", "RecipeID");

                    b.HasIndex("RecipeID");

                    b.ToTable("AirHandlerRecipe");
                });

            modelBuilder.Entity("AirHandlers.Domain.Entities.Room", b =>
                {
                    b.HasOne("AirHandlers.Domain.Entities.AirHandler", "AssociatedHandler")
                        .WithMany("ServedRooms")
                        .HasForeignKey("AssociatedHandlerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssociatedHandler");
                });

            modelBuilder.Entity("AirHandlers.Domain.Relations.AirHandlerRecipe", b =>
                {
                    b.HasOne("AirHandlers.Domain.Entities.AirHandler", "AirHandler")
                        .WithMany("AssociatedRecipes")
                        .HasForeignKey("AirHandlerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AirHandlers.Domain.Entities.Recipe", "Recipe")
                        .WithMany("ApplicableHandlers")
                        .HasForeignKey("RecipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AirHandler");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("AirHandlers.Domain.Entities.AirHandler", b =>
                {
                    b.Navigation("AssociatedRecipes");

                    b.Navigation("ServedRooms");
                });

            modelBuilder.Entity("AirHandlers.Domain.Entities.Recipe", b =>
                {
                    b.Navigation("ApplicableHandlers");
                });
#pragma warning restore 612, 618
        }
    }
}
