using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyJokesApp.Models;

namespace MyJokesApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Item>().HasData(
                new Item{Id = 4, Name="Book", Price=120, SerialNumberId = 1}
            );
            modelBuilder.Entity<SerialNumber>().HasData(
                new SerialNumber{Id = 1, Name="BOOK101", ItemId = 4}
            );
            modelBuilder.Entity<Category>().HasData(
                new Category{Id = 1, Name="Electronics"},
                new Category{Id = 2, Name="Books"}
            );
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Item> Items{ get; set; }
        public DbSet<SerialNumber> SerialNumbers{ get; set; }
        public DbSet<Category> Categories{ get; set; }
    }
}