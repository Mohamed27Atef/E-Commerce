using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;

namespace E_Commerce.Repository.ProductRepo
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceContext context;
        public ProductRepository(ECommerceContext context)
        {
            this.context = context;
        }

        public Product getById(int id)
        {
            var product = context.Product.FirstOrDefault(p => p.Id == id);
            if (product != null)
                return product;

            return null;
        }

        public Product getByName(string name)
        {
            Product product = context.Product.FirstOrDefault(p => p.Name == name);
            if (product != null)
                return product;

            return null;
        }

        public Product getByBrand(string brand)
        {
            Product product = context.Product.FirstOrDefault(p => p.Brand == brand);
            if (product != null)
                return product;

            return null;
        }

      
        public List<Product> getAll(string include = "")
        {

            var query = context.Product.AsQueryable();
            if (!String.IsNullOrEmpty(include))
            {
                var includes = include.Split(",");
                foreach (var inc in includes)
                {
                    query = query.Include(inc.Trim());
                }
            }
            return query.ToList();

        }

        public int add(Product entity)
        {
            int Raws = -1;
            if(entity != null)
            {
                context.Product.Add(entity);
                Raws = context.SaveChanges();
            }
            return Raws;
        }

        public int delete(int id)
        {
            int Raws = -1;
            var product = getById(id);
            if(product != null)
            {
                deleteImage(product.image);
                context.Product.Remove(product);
                Raws = context.SaveChanges();
            }

            return Raws;
        }

        private void deleteImage(string userImage)
        {
            var imageName = @"wwwroot\images\" + userImage;
            FileInfo file = new FileInfo(imageName);
            if (file.Exists)
            {
                file.Delete();
            }
        }

     

        public int update(Product entity)
        {
            int Raws = -1;
           
            if (entity != null)
            {
                context.Product.Update(entity);
                Raws = context.SaveChanges();
            }

            return Raws;
        }


    }
}
