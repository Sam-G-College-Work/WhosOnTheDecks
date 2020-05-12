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

        //Dj DBSet adds dj details as part of users table to Database
        public DbSet<Dj> Djs { get; set; }

        //Promoter DBSet adds promoter details as part of users table to Database
        public DbSet<Promoter> Promoters { get; set; }

        //Staff DBSet adds staff details as part of users table to Database
        public DbSet<Staff> Staff { get; set; }

        //Events DBSet adds event table to Database
        public DbSet<Event> Events { get; set; }

        //Bookings DBSet adds booking table to Database
        public DbSet<Booking> Bookings { get; set; }

        //Payments DBSet adds payment table to Database
        public DbSet<Payment> Payments { get; set; }
    }
}