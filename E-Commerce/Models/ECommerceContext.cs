using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MVC_Project.Models
{
    public class ECommerceContext:DbContext
    {
        public DbSet<Cart>Carts { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Favorite> Favorite { get; set; }
        public DbSet<Order>Order { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureCompositeKey<CartItem>(modelBuilder, ci => new { ci.ProductId, ci.CartId });
            ConfigureCompositeKey<Favorite>(modelBuilder, f => new { f.UserId, f.ProductId });
            ConfigureCompositeKey<ProductImage>(modelBuilder, k => new { k.ProductId, k.Image });

            // Other configurations...

            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureCompositeKey<TEntity>(ModelBuilder modelBuilder, Expression<Func<TEntity, object>> keyExpression)
            where TEntity : class
        {
            modelBuilder.Entity<TEntity>().HasKey(keyExpression);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-5AC7LE2\\SQLEXPRESS;Initial Catalog=ECommerceDB;Integrated Security=True;TrustServerCertificate=true;");
        }
    }
}
