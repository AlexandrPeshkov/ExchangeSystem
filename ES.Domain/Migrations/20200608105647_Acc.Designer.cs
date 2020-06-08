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
    [Migration("20200608105647_Acc")]
    partial class Acc
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("ES.Domain.Entities.Candle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Close")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("ExchangePairId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("High")
                        .HasColumnType("numeric");

                    b.Property<long>("Interval")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Low")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Open")
                        .HasColumnType("numeric");

                    b.Property<Guid>("PairId")
                        .HasColumnType("uuid");

                    b.Property<long>("TimeClose")
                        .HasColumnType("bigint");

                    b.Property<long>("TimeOpen")
                        .HasColumnType("bigint");

                    b.Property<decimal>("VolumeFrom")
                        .HasColumnType("numeric");

                    b.Property<decimal>("VolumeTo")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasAlternateKey("TimeOpen", "TimeClose", "PairId");

                    b.HasIndex("ExchangePairId");

                    b.HasIndex("PairId");

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

                    b.Property<Guid?>("SubscriptionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal?>("TotalCoinsMined")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasAlternateKey("Symbol");

                    b.HasIndex("SubscriptionId");

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

                    b.Property<string>("Predicate")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("ES.Domain.Entities.Candle", b =>
                {
                    b.HasOne("ES.Domain.Entities.ExchangePair", null)
                        .WithMany()
                        .HasForeignKey("ExchangePairId");

                    b.HasOne("ES.Domain.Entities.ExchangePair", "Pair")
                        .WithMany()
                        .HasForeignKey("PairId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ES.Domain.Entities.Currency", b =>
                {
                    b.HasOne("ES.Domain.Entities.Subscription", null)
                        .WithMany("Currencies")
                        .HasForeignKey("SubscriptionId");
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
                });
#pragma warning restore 612, 618
        }
    }
}
