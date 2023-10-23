using E_Commerce.Models;
using MVC_Project.Models;

namespace E_Commerce.Repository.ProductRepo
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Product getByName(string name);
        Product getByBrand(string brand);

        Product getbyid(int id);

        List<Product> search(string search, int catId);
        List<Product> getByCategory(int categoryId);

    }
}
