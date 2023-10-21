using E_Commerce.Models;
using E_Commerce.Repository.CartItemrepo;
using E_Commerce.Repository.cartRepo;
using E_Commerce.Repository.UserRepo;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    public class CartItemController : Controller
    {
        private readonly ICartItemRepository cartItem;
        private readonly ICartRepository icartRepo;
        private readonly IUserRepository iuserRepo;

        public CartItemController(ICartItemRepository cartItem, ICartRepository icartRepo,
            IUserRepository iuserRepo)
        {
            this.cartItem = cartItem;
            this.icartRepo = icartRepo;
            this.iuserRepo = iuserRepo;
        }

        public IActionResult counterCartItem()
        {
            string IDClaim =
               User.Claims
               .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value; // from cookie...
            Cart cart = icartRepo.getCartByUserId(iuserRepo.getUserByApplicationId(IDClaim));
            
            return Json(cartItem.getCounter(cart.Id));
        }

        public IActionResult getTotalPrice()
        {
            string IDClaim =
              User.Claims
              .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value; // from cookie...
            Cart cart = icartRepo.getCartByUserId(iuserRepo.getUserByApplicationId(IDClaim));

            return Json(cartItem.getTotalPrice(cart.Id));

        }


      
    }
}
