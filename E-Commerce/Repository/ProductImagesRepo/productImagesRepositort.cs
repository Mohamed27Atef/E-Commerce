using MVC_Project.Models;

namespace E_Commerce.Repository.ProductImagesRepo
{
    public class productImagesRepositort : IproductImagesRepositort
    {
        private readonly ECommerceContext context;
        public productImagesRepositort(ECommerceContext context)
        {
            this.context = context;
        }
        public void add(ProductImage entity)
        {
            this.context.ProductImage.Add(entity);
        }

        public void delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductImage> getAll(string include = "")
        {
            throw new NotImplementedException();
        }

        public ProductImage getById(int id)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void update(ProductImage entity)
        {
            throw new NotImplementedException();
        }
    }
}
