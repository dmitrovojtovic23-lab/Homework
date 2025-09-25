using _05_IntroToEF.AirportApp.Models.AirportApp.Models;
using _05_IntroToEF.AirportApp.Models;
using _05_IntroToEF.Entities;
using _05_IntroToEF.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Flight = _05_IntroToEF.AirportApp.Models.Flight;
using Airplane = _05_IntroToEF.AirportApp.Models.Airplane;
namespace _05_IntroToEF
{

    public class AirportDbContext: DbContext
    {
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<AccountDetail> AccountDetails => Set<AccountDetail>();
        public DbSet<Country> Countries => Set<Country>();
        public DbSet<City> Cities => Set<City>();
        public DbSet<AirplaneType> AirplaneTypes => Set<AirplaneType>();
        public DbSet<AirportApp.Models.Airplane> Airplanes => Set<AirportApp.Models.Airplane>();
        public DbSet<Flight> Flights => Set<Flight>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;
                                        Initial Catalog = Airport;
                                        Integrated Security=True;
                                        Connect Timeout=5;
                                        Encrypt=False;Trust Server Certificate=False;
                                        Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Detail)
                .WithOne(d => d.Account)
                .HasForeignKey<AccountDetail>(d => d.Id)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Country>()
                .HasMany(c => c.Cities)
                .WithOne(c => c.Country)
                .HasForeignKey(c => c.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<City>()
                .HasMany(c => c.Departures)
                .WithOne(f => f.DepartureCity)
                .HasForeignKey(f => f.DepartureCityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<City>()
                .HasMany(c => c.Arrivals)
                .WithOne(f => f.ArrivalCity)
                .HasForeignKey(f => f.ArrivalCityId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AirplaneType>()
                .HasMany(t => t.Airplanes)
                .WithOne(a => a.Type)
                .HasForeignKey(a => a.AirplaneTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Airplane>()
                .HasMany(a => a.Flights)
                .WithOne(f => f.Airplane)
                .HasForeignKey(f => f.AirplaneId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Flight>()
                .HasMany(f => f.Passengers)
                .WithMany(a => a.Flights)
                .UsingEntity<Dictionary<string, object>>(
                    "Booking",
                    b => b.HasOne<Account>().WithMany().HasForeignKey("AccountId").OnDelete(DeleteBehavior.Cascade),
                    b => b.HasOne<Flight>().WithMany().HasForeignKey("FlightId").OnDelete(DeleteBehavior.Cascade),
                    b =>
                    {
                        b.HasKey("FlightId", "AccountId");
                        b.ToTable("Bookings");
                    });
            modelBuilder.Entity<Account>().HasIndex(a => a.Login).IsUnique();
            modelBuilder.Entity<City>().HasIndex(c => new { c.Name, c.CountryId }).IsUnique();
            modelBuilder.Entity<Airplane>().HasIndex(a => a.Registration).IsUnique();
            SeedData(modelBuilder);
        }
        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1, Name = "Ukraine" },
                new Country { Id = 2, Name = "Poland" }
            );
            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, Name = "Kyiv", CountryId = 1 },
                new City { Id = 2, Name = "Lviv", CountryId = 1 },
                new City { Id = 3, Name = "Warsaw", CountryId = 2 }
            );
            modelBuilder.Entity<AirplaneType>().HasData(
                new AirplaneType { Id = 1, ModelName = "Boeing 737", Capacity = 180 },
                new AirplaneType { Id = 2, ModelName = "Embraer 190", Capacity = 100 }
            );

            modelBuilder.Entity<Airplane>().HasData(
                new Airplane { Id = 1, Registration = "UR-AAA", AirplaneTypeId = 1 },
                new Airplane { Id = 2, Registration = "SP-BBB", AirplaneTypeId = 2 }
            );

            modelBuilder.Entity<Account>().HasData(
                new Account { Id = 1, Login = "ivan", PasswordHash = "hash1" },
                new Account { Id = 2, Login = "olena", PasswordHash = "hash2" }
            );
            modelBuilder.Entity<AccountDetail>().HasData(
                new AccountDetail { Id = 1, FullName = "Ivan Petrov", Phone = "+380671234567" }
            );

            modelBuilder.Entity<Flight>().HasData(
                new Flight
                {
                    Id = 1,
                    FlightNumber = "PS101",
                    DepartureCityId = 1,
                    ArrivalCityId = 2,
                    DepartureTime = new DateTime(2025, 10, 01, 9, 0, 0),
                    ArrivalTime = new DateTime(2025, 10, 01, 10, 10, 0),
                    AirplaneId = 1
                },
                new Flight
                {
                    Id = 2,
                    FlightNumber = "PS102",
                    DepartureCityId = 2,
                    ArrivalCityId = 3,
                    DepartureTime = new DateTime(2025, 10, 01, 12, 0, 0),
                    ArrivalTime = new DateTime(2025, 10, 01, 13, 30, 0),
                    AirplaneId = 2
                }
            );
            modelBuilder.Entity("Booking").HasData(
                new { FlightId = 1, AccountId = 1 }
            );
        }
    }
}
