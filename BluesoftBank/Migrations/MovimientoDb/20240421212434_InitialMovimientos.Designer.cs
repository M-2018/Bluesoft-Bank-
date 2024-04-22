﻿// <auto-generated />
using System;
using BluesoftBank.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BluesoftBank.Migrations.MovimientoDb
{
    [DbContext(typeof(MovimientoDbContext))]
    [Migration("20240421212434_InitialMovimientos")]
    partial class InitialMovimientos
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BluesoftBank.Models.Movimiento", b =>
                {
                    b.Property<long>("IdMovimiento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdMovimiento"));

                    b.Property<string>("CiudadMovimiento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaMovimiento")
                        .HasColumnType("datetime2");

                    b.Property<long>("IdCuenta")
                        .HasColumnType("bigint");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdMovimiento");

                    b.ToTable("Movimientos");
                });
#pragma warning restore 612, 618
        }
    }
}