using Microsoft.EntityFrameworkCore;
using PlusMinus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusMinus.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Accessory> Accessories { get; set; }

        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<Eyecare> Eyecares { get; set; }

        public DbSet<Frame> Frames { get; set; }

        public DbSet<Glasses> Glasses { get; set; }

        public DbSet<Lenses> Lenses { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Timetable> Timetables { get; set; }

        public DbSet<User> Users { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public ApplicationContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProduct>()
                .HasOne(p => p.Order)
                .WithMany(b => b.OrderProducts);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(p => p.Product)
                .WithMany(b => b.OrderProducts);
            
            modelBuilder.Entity<Timetable>()
                .HasOne(p => p.Exercise)
                .WithMany(b => b.Timetables);
            
            modelBuilder.Entity<Timetable>()
                .HasOne(p => p.User)
                .WithMany(b => b.Timetables);
            
            modelBuilder.Entity<Order>()
                .HasOne(p => p.User)
                .WithMany(b => b.Orders);
            
        }
    }
}
