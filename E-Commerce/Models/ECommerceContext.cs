using E_Commerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace MVC_Project.Models
{
    public class ECommerceContext:IdentityDbContext<ApplicationIdentityUser>
    {
        public ECommerceContext()
        {
            
        }

        public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options) 
        {
            
        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItem { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Favorite> Favorite { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductImage> ProductImage { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<OrderHistory> OrderHistorys { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CartItem>().HasKey(r => new { r.ProductId, r.CartId });
            modelBuilder.Entity<ProductImage>().HasKey(r => new { r.ProductId, r.Image });
            modelBuilder.Entity<User>().HasKey(u => u.user_id);

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            //optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ECommerceDB;Integrated Security=True;TrustServerCertificate=true;");
        }
    }
}
