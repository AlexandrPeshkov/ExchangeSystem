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

            modelBuilder.Entity("ES.Domain.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasAlternateKey("Email");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("ES.Domain.Entities.Candle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Close")
                        .HasColumnType("numeric");

                    b.Property<Guid>("ExchangePairId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ExchangePairId1")
                        .HasColumnType("uuid");

                    b.Property<decimal>("High")
                        .HasColumnType("numeric");

                    b.Property<long>("Interval")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Low")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Open")
                        .HasColumnType("numeric");

                    b.Property<long>("TimeClose")
                        .HasColumnType("bigint");

                    b.Property<long>("TimeOpen")
                        .HasColumnType("bigint");

                    b.Property<decimal>("VolumeFrom")
                        .HasColumnType("numeric");

                    b.Property<decimal>("VolumeTo")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasAlternateKey("TimeOpen", "TimeClose", "ExchangePairId");

                    b.HasIndex("ExchangePairId");

                    b.HasIndex("ExchangePairId1");

                    b.ToTable("Candles");
                });

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
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal?>("TotalCoinsMined")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasAlternateKey("Symbol");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("ES.Domain.Entities.Exchange", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CentralizationType")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<string>("Grade")
                        .HasColumnType("text");

                    b.Property<decimal>("GradePoints")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("OrderBook")
                        .HasColumnType("boolean");

                    b.Property<bool>("Trades")
                        .HasColumnType("boolean");

                    b.Property<string>("WebSite")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Exchanges");
                });

            modelBuilder.Entity("ES.Domain.Entities.ExchangePair", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CurrencyFromId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CurrencyToId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ExchangeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasAlternateKey("ExchangeId", "CurrencyToId", "CurrencyFromId");

                    b.HasIndex("CurrencyFromId");

                    b.HasIndex("CurrencyToId");

                    b.ToTable("Pairs");
                });

            modelBuilder.Entity("ES.Domain.Entities.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uuid");

                    b.Property<string>("Predicate")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("CurrencyId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("ES.Domain.Entities.Candle", b =>
                {
                    b.HasOne("ES.Domain.Entities.ExchangePair", "ExchangePair")
                        .WithMany()
                        .HasForeignKey("ExchangePairId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ES.Domain.Entities.ExchangePair", null)
                        .WithMany()
                        .HasForeignKey("ExchangePairId1");
                });

            modelBuilder.Entity("ES.Domain.Entities.ExchangePair", b =>
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

                    b.HasOne("ES.Domain.Entities.Exchange", "Exchange")
                        .WithMany("Pairs")
                        .HasForeignKey("ExchangeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ES.Domain.Entities.Subscription", b =>
                {
                    b.HasOne("ES.Domain.Entities.Account", null)
                        .WithMany("Subscriptions")
                        .HasForeignKey("AccountId");

                    b.HasOne("ES.Domain.Entities.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
