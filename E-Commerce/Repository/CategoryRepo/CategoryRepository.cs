using MVC_Project.Models;

namespace E_Commerce.Repository.CategoryRepo
{
    public class CategoryRepository : ICategoryRepository
    {
       
        private readonly ECommerceContext context;
        public CategoryRepository(ECommerceContext context)
        {
            this.context = context;
        }
        public int add(Category entity)
        {
            throw new NotImplementedException();
        }

        public int delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Category> getAll(string include = "")
        {
            return context.Category.ToList();
        }

        public Category getById(int id)
        {
            throw new NotImplementedException();
        }

        public int update(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
