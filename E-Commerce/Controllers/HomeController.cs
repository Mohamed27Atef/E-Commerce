using E_Commerce.Models;
using E_Commerce.Repository.CartItemrepo;
using E_Commerce.Repository.cartRepo;
using E_Commerce.Repository.CategoryRepo;
using E_Commerce.Repository.ProductRepo;
using E_Commerce.Repository.UserRepo;
using E_Commerce.ViewModel.HomeViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using System;
using System.Diagnostics;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository iproductRepo;
        private readonly ICategoryRepository icategoryRepo;
        private readonly ICartItemRepository iCartitemrepo;
        private readonly ICartRepository icartRepo;
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly IUserRepository iuserRepo;


        public HomeController(IProductRepository iproductRepo, ICategoryRepository icategoryRepo,
            ICartItemRepository iCartitemrepo, ICartRepository icartRepo, UserManager<ApplicationIdentityUser> _userManager,
            IUserRepository IuserRepo, ILogger<HomeController> logger)
        {
            // inject DBContext
            this.iproductRepo = iproductRepo;
            this.icategoryRepo = icategoryRepo;
            this.iCartitemrepo = iCartitemrepo;
            this.icartRepo = icartRepo;
            this._userManager = _userManager;
            this.iuserRepo = IuserRepo;
            _logger = logger;
        }
      

        public IActionResult Index()
        {
            
            return  RedirectToAction("index", "product");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public string GetFromLocalToDB([FromBody] List<ProductLocalStorageVM> products)
        {
            if (User.Identity.IsAuthenticated)
            {
                foreach (var item in products)
                {
                    AddProductToDB(item.product_id);
                }
                return "Gooooooooooooooooooooood";
            }
            return "BaaaaaaaaaaaaaaaaaaaaaaaaaaD";
        }


        public void AddProductToDB(int id)
        {
            string IDClaim =
                User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value; // from cookie...

            User user = iuserRepo.getUserByID(IDClaim);

           Product prd = iproductRepo.getById(id);



            Cart cart = icartRepo.getCartByUserId(user.user_id);
            

            CartItem CartItem = iCartitemrepo.getByPrdIdUserId(prd.Id, cart.Id);


            if (CartItem == null)
            {
                CartItem = new CartItem()
                {
                    ProductId = prd.Id,
                    CartId = cart.Id,
                    Price = prd.Price,
                    Quantity = 1
                };
                cart.CartItems.Add(CartItem);
                iCartitemrepo.add(CartItem);


            }
            else
            {
                CartItem.Price += prd.Price;
                CartItem.Quantity++;
                iCartitemrepo.update(CartItem);

            }
            iCartitemrepo.SaveChanges();
            //return CartItem;

        }


    }
}