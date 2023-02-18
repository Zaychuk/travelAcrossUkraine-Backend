﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAcrossUkraine.WebApi.Context;

#nullable disable

namespace TravelAcrossUkraine.WebApi.Migrations
{
    [DbContext(typeof(TravelAcrossUkraineContext))]
    [Migration("20230212094210_AddedPolygonsTable")]
    partial class AddedPolygonsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TravelAcrossUkraine.WebApi.Entities.GeoPointEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("CoordinateX")
                        .HasPrecision(20, 10)
                        .HasColumnType("decimal(20,10)");

                    b.Property<decimal>("CoordinateY")
                        .HasPrecision(20, 10)
                        .HasColumnType("decimal(20,10)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("PolygonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("SequenceNumber")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PolygonId");

                    b.ToTable("GeoPoints");
                });

            modelBuilder.Entity("TravelAcrossUkraine.WebApi.Entities.PolygonEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Polygons");
                });

            modelBuilder.Entity("TravelAcrossUkraine.WebApi.Entities.GeoPointEntity", b =>
                {
                    b.HasOne("TravelAcrossUkraine.WebApi.Entities.PolygonEntity", "Polygon")
                        .WithMany("GeoPoints")
                        .HasForeignKey("PolygonId");

                    b.Navigation("Polygon");
                });

            modelBuilder.Entity("TravelAcrossUkraine.WebApi.Entities.PolygonEntity", b =>
                {
                    b.Navigation("GeoPoints");
                });
#pragma warning restore 612, 618
        }
    }
}
