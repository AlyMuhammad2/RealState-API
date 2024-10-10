using DAL.EntitiesConfig;
using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourProjectNamespace.Models;

namespace DAL.Data
{
    public class Context : IdentityDbContext<User, Role, int>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        //public DbSet<Product> Products { get; set; }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<tasks> Tasks { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Payment> Payments { get; set; }
       public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new UserRoleConfig());
            modelBuilder.Entity<Product>()
                       .HasDiscriminator<string>("ProductType")
                       .HasValue<Apartment>("Apartment")
                       .HasValue<House>("House")
                       .HasValue<Villa>("Villa");
            modelBuilder.Entity<Product>()
    .HasOne(e => e.Agency)
    .WithMany(e => e.Products)
    .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Agent>()
 .HasOne(e => e.Agency)
 .WithMany(e => e.Agents)
 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
