using E_Commerce.Models;
using MVC_Project.Models;

namespace E_Commerce.Repository.ProductRepo
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Product getByName(string name);
        Product getByBrand(string brand);
        void AddToFavorites(int productId, int userId);
        List<Product> GetFavoriteProductsForUser(string userId);


    }
}
