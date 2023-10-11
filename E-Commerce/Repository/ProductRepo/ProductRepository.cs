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
            var product = context.Product.Include(i=>i.Images).FirstOrDefault(p => p.Id == id);
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

        public void add(Product entity)
        {
            
            if(entity != null)
            {
                context.Product.Add(entity);
           
            }
           
        }

        public void delete(int id)
        {
            var product = getById(id);
            if(product != null)
            {
                deleteImage(product.image);
                context.Product.Remove(product);
            }

        
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

     

        public void update(Product entity)
        {
          
            if (entity != null)
                context.Product.Update(entity);
                
        }

       public int SaveChanges()
        {
            return context.SaveChanges();
        }


    }
}
