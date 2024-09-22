using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Eventinleveropdracht.Models;

namespace Eventinleveropdracht.Data
{
    public class VoorbeeldDatabase : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Reservatie> Reservaties { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Cashier> Cashiers { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = @"Data Source=.;Initial Catalog=EventDB;Integrated Security=true;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Event configuratie
            modelBuilder.Entity<Event>()
                .Property(v => v.Name)
                .HasMaxLength(50)
                .IsRequired();

            // Event -> Organizer (One-to-One)
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Organiser)
                .WithMany()
                .HasForeignKey(e => e.OrganiserId)
                .OnDelete(DeleteBehavior.Restrict); // Vermijd cascade delete

            // Event -> Reservatie (One-to-Many)
            modelBuilder.Entity<Reservatie>()
                .HasOne(r => r.Event)
                .WithMany(e => e.Reservations)
                .HasForeignKey(r => r.EventID)
                .OnDelete(DeleteBehavior.Restrict); // Geen cascade delete

            // Order -> Guest (One-to-Many) zonder cascade delete
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Guest)
                .WithMany(g => g.Orders)
                .HasForeignKey(o => o.GuestId)
                .OnDelete(DeleteBehavior.Restrict);  // Geen cascade delete

            // Order -> Reservatie (One-to-One of Many) zonder cascade delete
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Reservatie)
                .WithMany()
                .HasForeignKey(o => o.ReservatieId)
                .OnDelete(DeleteBehavior.Restrict); // Geen cascade delete

            // Seed data voor Organizer
            modelBuilder.Entity<Organizer>().HasData(
                new Organizer
                {
                    Id = 1,
                    Name = "John Doe",
                    Email = "johndoe@example.com",
                    Username = "johndoe",
                    Password = "hashedpassword",
                    Role = "Organizer",
                    phone = 123456789
                }
            );

            // Seed data voor Guests
            modelBuilder.Entity<Guest>().HasData(
                new Guest
                {
                    Id = 2,
                    Name = "Jane Doe",
                    Email = "test@gmail.com",
                    Username = "janedoe",
                    Password = "hashedpassword",
                    Role = "Guest"
                },
                new Guest
                {
                    Id = 3,
                    Name = "Jane Doe",
                    Email = "Has@gmail.com",
                    Username = "janedoe2",
                    Password = "hashedpassword",
                    Role = "Guest"
                }
            );

            // Seed data voor Admin
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    Id = 4,
                    Name = "Admin",
                    Email = "admin@gmail.com",
                    Username = "Admin",
                    Password = "hashedpassword",
                    Role = "Admin",
                    AdminCode = 1234
                }
            );

            // Seed data voor Cashier
            modelBuilder.Entity<Cashier>().HasData(
                new Cashier
                {
                    Id = 5,
                    Name = "Cashier",
                    Email = "cashier@gmail.com",
                    Username = "Cashier",
                    Password = "hashedpassword",
                    Role = "Cashier"
                }
            );

            // Seed data voor Events met OrganizerId
            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 2,
                    Name = "Test2",
                    Description = "This is a beautiful event performing a concert of a well-known DJ",
                    FromDate = DateTime.Now,
                    ToDate = DateTime.Now,
                    Location = "Entire venue",
                    Type = "Concert",
                    Requirements = new List<string> { "Ticket", "ID" },
                    MaxParticipants = 500,
                    CurrentParticipants = 50,
                    Image = "ComingSoon.jpg",
                    OrganiserId = 1  // Verwijzing naar de bestaande Organizer
                }
            );

            // Seed data voor Reservatie
            modelBuilder.Entity<Reservatie>().HasData(
                new Reservatie
                {
                    Id = 1,
                    Name = "Jane Doe",
                    Email = "Testing@gmail.com",
                    ReservationNumber = 1234,
                    Description = "This is a test",
                    Date = DateTime.Now,
                    type = "VIP",
                    ammount = 2,
                    Paid = true,
                    Price = 50,
                    EventID = 2,  // Verwijzing naar het Event (Test2)
                    GuestId = 2  // Verwijzing naar Guest (Jane Doe)
                }
            );

            // Seed data voor Order
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    Date = DateTime.Now,
                    Status = "Paid",
                    ReservatieId = 1,  // Verwijzing naar de Reservatie (Id = 1)
                    GuestId = 2  // Verwijzing naar Guest (Id = 2)
                }
            );
        }
    }
}
