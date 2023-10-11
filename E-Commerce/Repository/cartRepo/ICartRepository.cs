using MVC_Project.Models;

namespace E_Commerce.Repository.cartRepo
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        public Cart getCartByUserId(int id);
    }
}
