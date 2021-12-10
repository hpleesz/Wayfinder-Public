﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WayfinderServer;

namespace WayfinderServer.Migrations
{
    [DbContext(typeof(WayfinderContext))]
    [Migration("20210412212802_PlaceImage")]
    partial class PlaceImage
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WayfinderServer.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlaceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("WayfinderServer.Entities.Floor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int?>("PlaceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.ToTable("Floors");
                });

            modelBuilder.Entity("WayfinderServer.Entities.FloorPlan2D", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FloorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FloorId")
                        .IsUnique()
                        .HasFilter("[FloorId] IS NOT NULL");

                    b.ToTable("FloorPlan2Ds");
                });

            modelBuilder.Entity("WayfinderServer.Entities.FloorPlan3D", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FloorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FloorId")
                        .IsUnique()
                        .HasFilter("[FloorId] IS NOT NULL");

                    b.ToTable("FloorPlan3Ds");
                });

            modelBuilder.Entity("WayfinderServer.Entities.Marker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CloudAnchorId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FloorId")
                        .HasColumnType("int");

                    b.Property<string>("QRCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("XCoordinate")
                        .HasColumnType("real");

                    b.Property<float>("YCoordinate")
                        .HasColumnType("real");

                    b.Property<float>("ZCoordinate")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("FloorId");

                    b.ToTable("Markers");
                });

            modelBuilder.Entity("WayfinderServer.Entities.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Zip")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("WayfinderServer.Entities.Target", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("FloorId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("XCoordinate")
                        .HasColumnType("real");

                    b.Property<float>("YCoordinate")
                        .HasColumnType("real");

                    b.Property<float>("ZCoordinate")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("FloorId");

                    b.ToTable("Targets");
                });

            modelBuilder.Entity("WayfinderServer.Entities.VirtualObject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TargetId")
                        .HasColumnType("int");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int");

                    b.Property<float>("XCoordinate")
                        .HasColumnType("real");

                    b.Property<float>("XRotation")
                        .HasColumnType("real");

                    b.Property<float>("XScale")
                        .HasColumnType("real");

                    b.Property<float>("YCoordinate")
                        .HasColumnType("real");

                    b.Property<float>("YRotation")
                        .HasColumnType("real");

                    b.Property<float>("YScale")
                        .HasColumnType("real");

                    b.Property<float>("ZCoordinate")
                        .HasColumnType("real");

                    b.Property<float>("ZRotation")
                        .HasColumnType("real");

                    b.Property<float>("ZScale")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("TargetId");

                    b.HasIndex("TypeId");

                    b.ToTable("VirtualObjects");
                });

            modelBuilder.Entity("WayfinderServer.Entities.VirtualObjectType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("VirtualObjectTypes");
                });

            modelBuilder.Entity("WayfinderServer.Entities.Category", b =>
                {
                    b.HasOne("WayfinderServer.Entities.Place", "Place")
                        .WithMany("Categories")
                        .HasForeignKey("PlaceId");

                    b.Navigation("Place");
                });

            modelBuilder.Entity("WayfinderServer.Entities.Floor", b =>
                {
                    b.HasOne("WayfinderServer.Entities.Place", "Place")
                        .WithMany("Floors")
                        .HasForeignKey("PlaceId");

                    b.Navigation("Place");
                });

            modelBuilder.Entity("WayfinderServer.Entities.FloorPlan2D", b =>
                {
                    b.HasOne("WayfinderServer.Entities.Floor", "Floor")
                        .WithOne("FloorPlan2D")
                        .HasForeignKey("WayfinderServer.Entities.FloorPlan2D", "FloorId");

                    b.Navigation("Floor");
                });

            modelBuilder.Entity("WayfinderServer.Entities.FloorPlan3D", b =>
                {
                    b.HasOne("WayfinderServer.Entities.Floor", "Floor")
                        .WithOne("FloorPlan3D")
                        .HasForeignKey("WayfinderServer.Entities.FloorPlan3D", "FloorId");

                    b.Navigation("Floor");
                });

            modelBuilder.Entity("WayfinderServer.Entities.Marker", b =>
                {
                    b.HasOne("WayfinderServer.Entities.Floor", "Floor")
                        .WithMany("Markers")
                        .HasForeignKey("FloorId");

                    b.Navigation("Floor");
                });

            modelBuilder.Entity("WayfinderServer.Entities.Target", b =>
                {
                    b.HasOne("WayfinderServer.Entities.Category", "Category")
                        .WithMany("Targets")
                        .HasForeignKey("CategoryId");

                    b.HasOne("WayfinderServer.Entities.Floor", "Floor")
                        .WithMany("Targets")
                        .HasForeignKey("FloorId");

                    b.Navigation("Category");

                    b.Navigation("Floor");
                });

            modelBuilder.Entity("WayfinderServer.Entities.VirtualObject", b =>
                {
                    b.HasOne("WayfinderServer.Entities.Target", "Target")
                        .WithMany("VirtualObjects")
                        .HasForeignKey("TargetId");

                    b.HasOne("WayfinderServer.Entities.VirtualObjectType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");

                    b.Navigation("Target");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("WayfinderServer.Entities.Category", b =>
                {
                    b.Navigation("Targets");
                });

            modelBuilder.Entity("WayfinderServer.Entities.Floor", b =>
                {
                    b.Navigation("FloorPlan2D");

                    b.Navigation("FloorPlan3D");

                    b.Navigation("Markers");

                    b.Navigation("Targets");
                });

            modelBuilder.Entity("WayfinderServer.Entities.Place", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Floors");
                });

            modelBuilder.Entity("WayfinderServer.Entities.Target", b =>
                {
                    b.Navigation("VirtualObjects");
                });
#pragma warning restore 612, 618
        }
    }
}
