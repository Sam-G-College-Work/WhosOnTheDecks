using Microsoft.EntityFrameworkCore;
using WhosOnTheDecks.API.Models;

namespace WhosOnTheDecks.API.Data
{
    public class DataContext : DbContext
    {
        // Constructor that takes in DBContextOptions and options from the base class DbContext
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }


        //User DBSet adds user table to Database
        public DbSet<User> Users { get; set; }

        public DbSet<Dj> Djs { get; set; }

        public DbSet<Promoter> Promoters { get; set; }

        public DbSet<Staff> Staff { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Booking> Bookings { get; set; }
    }
}