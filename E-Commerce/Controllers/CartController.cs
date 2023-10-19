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

        
        public IActionResult addToCart(int id)
        {
            return View();
        }
        [HttpDelete]
        public void removeCartItem(int prodcut_id, int cart_id)
        {
            cartItemRepository.deleteCart(prodcut_id, cart_id);
            cartItemRepository.SaveChanges();
        }
        
    }
}
