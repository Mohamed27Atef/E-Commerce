using E_Commerce.Repository.CartItemrepo;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartItemRepository cartItemRepository;

        public CartController(ICartItemRepository cartItemRepository)
        {
            this.cartItemRepository = cartItemRepository;
        }

        //public List<CartItem> getUserCartItem()
        //{
        //    string IDClaim =
        //        User.Claims
        //        .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value; // from cookie...
        //    return cartItemRepository.getCartItemByUserId();
        //}
    }
}
