using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VueJSDotnet51.Models;

namespace VueJSDotnet51.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options): base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Driver> drivers { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Rider> riders { get; set; }
        public DbSet<Vehicle> vehicles { get; set; }
        public DbSet<Shift> shfits { get; set; }
        public DbSet<Vehicle_ride> vehicle_Rides { get; set; }
        public DbSet<Vehicle_ride_status> vehicle_Ride_Statuses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Driver>();
            //modelBuilder.Entity<Rider>();


        }
    }
}
