using E_Commerce.Models;
using E_Commerce.Repository.CartItemrepo;
using E_Commerce.Repository.cartRepo;
using E_Commerce.Repository.CategoryRepo;
using E_Commerce.Repository.ProductRepo;
using E_Commerce.Repository.ReviewRepo;
using E_Commerce.Repository.UserRepo;
using E_Commerce.ViewModel.OrderViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProductRepository iproductRepo;
        private readonly ICategoryRepository icategoryRepo;
        private readonly ICartItemRepository iCartitemrepo;
        private readonly ICartRepository icartRepo;
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly IUserRepository iuserRepo;
        private readonly IReviewRepo ireviewRepo;

        public OrderController(IProductRepository iproductRepo, ICategoryRepository icategoryRepo,
            ICartItemRepository iCartitemrepo, ICartRepository icartRepo, UserManager<ApplicationIdentityUser> _userManager,
            IUserRepository IuserRepo, IReviewRepo ireview)
        {
            // inject DBContext
            this.iproductRepo = iproductRepo;
            this.icategoryRepo = icategoryRepo;
            this.iCartitemrepo = iCartitemrepo;
            this.icartRepo = icartRepo;
            this._userManager = _userManager;
            this.iuserRepo = IuserRepo;
            this.ireviewRepo = ireview;
        }
        [Authorize]
        public IActionResult Index()
        {
            List<OrderedItemForUserVM> orderedItem = getproductByCartItem();

            return View(orderedItem);
        }
        [Authorize]
        public List<OrderedItemForUserVM> getproductByCartItem()
        {
            string IDClaim =
                User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value; // from cookie...

            User user = iuserRepo.getUserByID(IDClaim);
            Cart cart = icartRepo.getCartByUserId(user.user_id);

            var allCartItemOfCurrentUser = iCartitemrepo.getCartItemByCardId(cart.Id);
            List<Product> prdLst = new List<Product>();
            foreach (var cartItem in allCartItemOfCurrentUser)
            {
                Product product = iproductRepo.getById(cartItem.ProductId);
                prdLst.Add(product);
            }

            List<OrderedItemForUserVM> orderedItemForUserVM = new List<OrderedItemForUserVM>();

            for (int i = 0; i < prdLst.Count; i++)
            {
                OrderedItemForUserVM order = new OrderedItemForUserVM
                {
                    Name = prdLst[i].Name,
                    Brand = prdLst[i].Brand,
                    image = prdLst[i].image,
                    Quantity = allCartItemOfCurrentUser[i].Quantity,
                    TotalPrice = allCartItemOfCurrentUser[i].Price
                };

                orderedItemForUserVM.Add(order);
            }

            return orderedItemForUserVM;
        }
    }
}
