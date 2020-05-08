﻿// <auto-generated />
using System;
using ES.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ES.Data.Migrations
{
    [DbContext(typeof(CoreDBContext))]
    partial class CoreDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ES.Domain.Entities.Currency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Algorithm")
                        .HasColumnType("text");

                    b.Property<long?>("BlockNumber")
                        .HasColumnType("bigint");

                    b.Property<decimal?>("BlockReward")
                        .HasColumnType("numeric");

                    b.Property<string>("Fullname")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal?>("NetHashesPerSecond")
                        .HasColumnType("numeric");

                    b.Property<string>("SmartContractAddress")
                        .HasColumnType("text");

                    b.Property<string>("Symbol")
                        .HasColumnType("text");

                    b.Property<decimal?>("TotalCoinsMined")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("ES.Domain.Entities.Exchange", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<string>("Grade")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("WebSite")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Exchanges");
                });

            modelBuilder.Entity("ES.Domain.Entities.Pair", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CurrencyFromId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CurrencyToId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ExchangeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyFromId");

                    b.HasIndex("CurrencyToId");

                    b.HasIndex("ExchangeId");

                    b.ToTable("Pairs");
                });

            modelBuilder.Entity("ES.Domain.Entities.Pair", b =>
                {
                    b.HasOne("ES.Domain.Entities.Currency", "CurrencyFrom")
                        .WithMany()
                        .HasForeignKey("CurrencyFromId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ES.Domain.Entities.Currency", "CurrencyTo")
                        .WithMany()
                        .HasForeignKey("CurrencyToId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ES.Domain.Entities.Exchange", null)
                        .WithMany("Pairs")
                        .HasForeignKey("ExchangeId");
                });
#pragma warning restore 612, 618
        }
    }
}
