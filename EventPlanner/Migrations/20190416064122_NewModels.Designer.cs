﻿// <auto-generated />
using System;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EventPlanner.Migrations
{
    [DbContext(typeof(EventPlannerContext))]
    [Migration("20190416064122_NewModels")]
    partial class NewModels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EventPlanner.Models.Accommodation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adress");

                    b.Property<int>("Capacity");

                    b.Property<int>("LocationID");

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.Property<string>("Type");

                    b.HasKey("ID");

                    b.HasIndex("LocationID");

                    b.ToTable("Accommodation");
                });

            modelBuilder.Entity("EventPlanner.Models.Event", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LocationID");

                    b.Property<string>("Name");

                    b.Property<DateTime>("Time");

                    b.HasKey("ID");

                    b.HasIndex("LocationID");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("EventPlanner.Models.Location", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adress");

                    b.Property<string>("Area");

                    b.Property<int>("Capacity");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("EventPlanner.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Admin");

                    b.Property<int>("Age");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("EventPlanner.Models.Accommodation", b =>
                {
                    b.HasOne("EventPlanner.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EventPlanner.Models.Event", b =>
                {
                    b.HasOne("EventPlanner.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
