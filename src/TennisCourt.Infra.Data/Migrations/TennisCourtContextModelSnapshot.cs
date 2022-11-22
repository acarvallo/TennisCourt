﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TennisCourt.Infra.Data.Context;

#nullable disable

namespace TennisCourt.Infra.Data.Migrations
{
    [DbContext(typeof(TennisCourtContext))]
    partial class TennisCourtContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TennisCourt.Domain.Models.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReservedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.ToTable("Reservation");
                });

            modelBuilder.Entity("TennisCourt.Domain.Models.ReservationStatusHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ReservationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ReservationStatus")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.HasIndex("ReservationId");

                    b.ToTable("ReservationStatusHistory");
                });

            modelBuilder.Entity("TennisCourt.Domain.Models.Reservation", b =>
                {
                    b.OwnsOne("TennisCourt.Domain.ValueObjects.Money", "Amount", b1 =>
                        {
                            b1.Property<Guid>("ReservationId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Value")
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Amount");

                            b1.HasKey("ReservationId");

                            b1.ToTable("Reservation");

                            b1.WithOwner()
                                .HasForeignKey("ReservationId");
                        });

                    b.OwnsOne("TennisCourt.Domain.ValueObjects.Money", "RefundAmount", b1 =>
                        {
                            b1.Property<Guid>("ReservationId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Value")
                                .ValueGeneratedOnAdd()
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)")
                                .HasDefaultValue(0m)
                                .HasColumnName("RefundAmount");

                            b1.HasKey("ReservationId");

                            b1.ToTable("Reservation");

                            b1.WithOwner()
                                .HasForeignKey("ReservationId");
                        });

                    b.Navigation("Amount")
                        .IsRequired();

                    b.Navigation("RefundAmount")
                        .IsRequired();
                });

            modelBuilder.Entity("TennisCourt.Domain.Models.ReservationStatusHistory", b =>
                {
                    b.HasOne("TennisCourt.Domain.Models.Reservation", "Reservation")
                        .WithMany("ReservationHistory")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("TennisCourt.Domain.Models.Reservation", b =>
                {
                    b.Navigation("ReservationHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
