﻿// <auto-generated />
using System;
using ES.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ES.Data.Migrations
{
    [DbContext(typeof(CoreDBContext))]
    [Migration("20200508100719_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ES.Domain.Entities.Exchange", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Change")
                        .HasColumnType("numeric");

                    b.Property<string>("ExchangeId")
                        .HasColumnType("text");

                    b.Property<decimal>("MakerFee")
                        .HasColumnType("numeric");

                    b.Property<int>("Markets")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal>("TakerFee")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Volume")
                        .HasColumnType("numeric");

                    b.Property<string>("WebSite")
                        .HasColumnType("text");

                    b.Property<int>("_ID")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Exchanges");
                });
#pragma warning restore 612, 618
        }
    }
}
