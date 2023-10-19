using MVC_Project.Models;

namespace E_Commerce.Repository.CartItemrepo
{
    public interface ICartItemRepository:IGenericRepository<CartItem>
    {
        public CartItem getByPrdIdUserId(int prdId, int cardId);
        public List<CartItem> getCartItemByCardId(int cartId);
        public void deleteCart(int product_id, int cart_id);
    }
}
