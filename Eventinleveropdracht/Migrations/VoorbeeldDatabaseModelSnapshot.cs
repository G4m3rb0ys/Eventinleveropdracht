﻿// <auto-generated />
using System;
using Eventinleveropdracht.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Eventinleveropdracht.Migrations
{
    [DbContext(typeof(VoorbeeldDatabase))]
    partial class VoorbeeldDatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Eventinleveropdracht.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CurrentParticipants")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxParticipants")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("OrganiserId")
                        .HasColumnType("int");

                    b.Property<string>("Requirements")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrganiserId");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            CurrentParticipants = 50,
                            Description = "This is a beautiful event performing a concert of a well-known DJ",
                            FromDate = new DateTime(2024, 10, 11, 10, 27, 41, 518, DateTimeKind.Local).AddTicks(5572),
                            Image = "ComingSoon.jpg",
                            Location = "Entire venue",
                            MaxParticipants = 500,
                            Name = "Test2",
                            OrganiserId = 1,
                            Requirements = "[\"Ticket\",\"ID\"]",
                            ToDate = new DateTime(2024, 10, 11, 10, 27, 41, 518, DateTimeKind.Local).AddTicks(5637),
                            Type = "Concert"
                        });
                });

            modelBuilder.Entity("Eventinleveropdracht.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("GuestId")
                        .HasColumnType("int");

                    b.Property<int>("ReservatieId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GuestId");

                    b.HasIndex("ReservatieId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTime(2024, 10, 11, 10, 27, 41, 518, DateTimeKind.Local).AddTicks(5679),
                            GuestId = 2,
                            ReservatieId = 1,
                            Status = "Paid"
                        });
                });

            modelBuilder.Entity("Eventinleveropdracht.Models.Reservatie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EventID")
                        .HasColumnType("int");

                    b.Property<int?>("GuestId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Paid")
                        .HasColumnType("bit");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("ReservationNumber")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EventID");

                    b.HasIndex("GuestId");

                    b.ToTable("Reservaties");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 2,
                            Date = new DateTime(2024, 10, 11, 10, 27, 41, 518, DateTimeKind.Local).AddTicks(5663),
                            Email = "Testing@gmail.com",
                            EventID = 2,
                            Name = "Jane Doe",
                            Paid = true,
                            Price = 50,
                            ReservationNumber = 1234,
                            Type = "VIP"
                        });
                });

            modelBuilder.Entity("Eventinleveropdracht.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator().HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Eventinleveropdracht.Models.Admin", b =>
                {
                    b.HasBaseType("Eventinleveropdracht.Models.User");

                    b.Property<int>("AdminCode")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Admin");

                    b.HasData(
                        new
                        {
                            Id = 4,
                            Email = "admin@gmail.com",
                            Name = "Admin",
                            Password = "hashedpassword",
                            Role = "Admin",
                            Username = "Admin",
                            AdminCode = 1234
                        });
                });

            modelBuilder.Entity("Eventinleveropdracht.Models.Cashier", b =>
                {
                    b.HasBaseType("Eventinleveropdracht.Models.User");

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.HasIndex("EventId");

                    b.HasDiscriminator().HasValue("Cashier");

                    b.HasData(
                        new
                        {
                            Id = 5,
                            Email = "cashier@gmail.com",
                            Name = "Cashier",
                            Password = "hashedpassword",
                            Role = "Cashier",
                            Username = "Cashier"
                        });
                });

            modelBuilder.Entity("Eventinleveropdracht.Models.Guest", b =>
                {
                    b.HasBaseType("Eventinleveropdracht.Models.User");

                    b.HasDiscriminator().HasValue("Guest");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Email = "test@gmail.com",
                            Name = "Jane Doe",
                            Password = "hashedpassword",
                            Role = "Guest",
                            Username = "janedoe"
                        },
                        new
                        {
                            Id = 3,
                            Email = "Has@gmail.com",
                            Name = "Jane Doe",
                            Password = "hashedpassword",
                            Role = "Guest",
                            Username = "janedoe2"
                        });
                });

            modelBuilder.Entity("Eventinleveropdracht.Models.Organizer", b =>
                {
                    b.HasBaseType("Eventinleveropdracht.Models.User");

                    b.Property<int>("phone")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Organizer");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "johndoe@example.com",
                            Name = "John Doe",
                            Password = "hashedpassword",
                            Role = "Organizer",
                            Username = "johndoe",
                            phone = 123456789
                        });
                });

            modelBuilder.Entity("Eventinleveropdracht.Models.Event", b =>
                {
                    b.HasOne("Eventinleveropdracht.Models.Organizer", "Organiser")
                        .WithMany()
                        .HasForeignKey("OrganiserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Organiser");
                });

            modelBuilder.Entity("Eventinleveropdracht.Models.Order", b =>
                {
                    b.HasOne("Eventinleveropdracht.Models.Guest", "Guest")
                        .WithMany("Orders")
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Eventinleveropdracht.Models.Reservatie", "Reservatie")
                        .WithMany()
                        .HasForeignKey("ReservatieId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Guest");

                    b.Navigation("Reservatie");
                });

            modelBuilder.Entity("Eventinleveropdracht.Models.Reservatie", b =>
                {
                    b.HasOne("Eventinleveropdracht.Models.Event", "Event")
                        .WithMany("Reservations")
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Eventinleveropdracht.Models.Guest", null)
                        .WithMany("Reservaties")
                        .HasForeignKey("GuestId");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Eventinleveropdracht.Models.Cashier", b =>
                {
                    b.HasOne("Eventinleveropdracht.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Eventinleveropdracht.Models.Event", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Eventinleveropdracht.Models.Guest", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Reservaties");
                });
#pragma warning restore 612, 618
        }
    }
}
