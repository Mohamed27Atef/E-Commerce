using MVC_Project.Models;

namespace E_Commerce.Repository.CartItemrepo
{
    public interface ICartItemRepository:IGenericRepository<CartItem>
    {
        public CartItem getByPrdIdUserId(int prdId, int cardId);
    }
}
