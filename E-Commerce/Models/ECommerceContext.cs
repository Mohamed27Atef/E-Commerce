using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MVC_Project.Models
{
    public class ECommerceContext:DbContext
    {
        public ECommerceContext()
        {
            
        }

        public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options) 
        {
            
        }

        public virtual DbSet<Cart>Carts { get; set; }
        public virtual DbSet<CartItem> CartItem { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Favorite> Favorite { get; set; }
        public virtual DbSet<Order>Order { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductImage> ProductImage { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureCompositeKey<CartItem>(modelBuilder, ci => new { ci.ProductId, ci.CartId });
            ConfigureCompositeKey<Favorite>(modelBuilder, f => new { f.UserId, f.ProductId });
            ConfigureCompositeKey<ProductImage>(modelBuilder, k => new { k.ProductId, k.Image });


            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureCompositeKey<TEntity>(ModelBuilder modelBuilder, Expression<Func<TEntity, object>> keyExpression)
            where TEntity : class
        {
            modelBuilder.Entity<TEntity>().HasKey(keyExpression);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ECommerceDB;Integrated Security=True;TrustServerCertificate=true;");
        }
    }
}
