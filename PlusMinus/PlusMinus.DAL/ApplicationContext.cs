using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlusMinus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PlusMinus.DAL
{
    public class ApplicationContext : IdentityDbContext<User>
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

        public DbSet<User> AppUsers { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public ApplicationContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        private void SeedUsers(ModelBuilder builder)
        {
            User user = new User
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                LockoutEnabled = false,
                Name = "Admin",
                Surname = "Admin",
                Lastname = "Admin",
            };

            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            var password = passwordHasher.HashPassword(user, "Admin123*");
            user.PasswordHash = password;
            builder.Entity<User>().HasData(user);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Id = "c7b013f0-5201-4317-abd8-c211f91b7330", Name = "Customer", ConcurrencyStamp = "2", NormalizedName = "Customer" }
            );
        }

        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "fab4fac1-c546-41de-aebc-a14da6895711", UserId = "b74ddd14-6340-4840-95c2-db12554843e5" }
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            this.SeedUsers(modelBuilder);
            this.SeedRoles(modelBuilder);
            this.SeedUserRoles(modelBuilder);

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
