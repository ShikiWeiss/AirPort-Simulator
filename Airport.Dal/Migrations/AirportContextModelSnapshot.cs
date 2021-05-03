﻿// <auto-generated />
using System;
using Airport.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Airport.Dal.Migrations
{
    [DbContext(typeof(AirportContext))]
    partial class AirportContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9");

            modelBuilder.Entity("AirPort.Common.LandingDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Company")
                        .HasColumnType("TEXT");

                    b.Property<int>("PlaneId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RunwayStationId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Landings");
                });

            modelBuilder.Entity("AirPort.Common.PlaneToStationMovementDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EnteredTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExitedTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("PlaneId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ToStationId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("MovementHistory");
                });

            modelBuilder.Entity("AirPort.Common.TakeoffDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlaneId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RunwayStationId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Takeoffs");
                });

            modelBuilder.Entity("Common.Models.Plane", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Company")
                        .HasColumnType("TEXT");

                    b.Property<int>("CurrentStationId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsLanded")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Planes");
                });
#pragma warning restore 612, 618
        }
    }
}
