using E_Commerce.Models;
using E_Commerce.Repository.CartItemrepo;
using E_Commerce.Repository.cartRepo;
using E_Commerce.Repository.CategoryRepo;
using E_Commerce.Repository.OrderRepo;
using E_Commerce.Repository.ProductRepo;
using E_Commerce.Repository.ReviewRepo;
using E_Commerce.Repository.UserRepo;
using E_Commerce.ViewModel.OrderViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using System.Diagnostics;
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
        private readonly IOrderRepository iorderRepo;

        public OrderController(IProductRepository iproductRepo, ICategoryRepository icategoryRepo,
            ICartItemRepository iCartitemrepo, ICartRepository icartRepo, UserManager<ApplicationIdentityUser> _userManager,
            IUserRepository IuserRepo, IReviewRepo ireview,IOrderRepository _iorderRepo)
        {
            // inject DBContext
            this.iproductRepo = iproductRepo;
            this.icategoryRepo = icategoryRepo;
            this.iCartitemrepo = iCartitemrepo;
            this.icartRepo = icartRepo;
            this._userManager = _userManager;
            this.iuserRepo = IuserRepo;
            this.ireviewRepo = ireview;
            iorderRepo = _iorderRepo;
        }
        [Authorize]
        public IActionResult Index()
        {
            List<OrderedItemForUserVM> orderedItem = getproductByCartItem();

            return View(orderedItem);
        }
        public User getUser()
        {
            string IDClaim =
               User.Claims
               .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value; // from cookie...

            User user = iuserRepo.getUserByID(IDClaim);

            return user;
        }
        [Authorize]
        public List<OrderedItemForUserVM> getproductByCartItem()
        {
            string IDClaim =
               User.Claims
               .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value; // from cookie...

            User user = getUser();
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
                    id = prdLst[i].Id,
                    Name = prdLst[i].Name,
                    Brand = prdLst[i].Brand,
                    image = prdLst[i].image,
                    cart_id = iuserRepo.getUserByApplicationId(IDClaim),
                    price = prdLst[i].Price,
                    Quantity = allCartItemOfCurrentUser[i].Quantity,
                    TotalPrice = allCartItemOfCurrentUser[i].Price
                };

                orderedItemForUserVM.Add(order);
            }

            return orderedItemForUserVM;
        }

        public void DelteOrderById(int id)
        {
            iorderRepo.delete(id);
            iorderRepo.SaveChanges();

        }
        [HttpPost]
        public IActionResult saveOrder(string Address,decimal TotalPrice= 0)
        {
            User user = getUser();
            Cart cart = icartRepo.getCartByUserId(user.user_id);
            Order order = new Order()
                {
                    cart_id = cart.Id,
                    TotalPrice = TotalPrice,
                    Status = OrderStatus.Shipped,
                    OrderDate = DateTime.Now,
                    UserId = user.user_id,
                    Address = Address
            };


            iorderRepo.add(order);
            iorderRepo.SaveChanges();


            //delet all cartitem for user after order checked
            var cartitems = iCartitemrepo.getAll();
            foreach (var item in cartitems)
            {
                iCartitemrepo.delete(item.ProductId);
            }
            iCartitemrepo.SaveChanges();
            return View(order);
         }

        public IActionResult OrderHistory()
        {
            User user = getUser();
            

            List<Order> userOrders = iorderRepo.getAll()
                .Where(order => order.UserId == user.user_id)
                .ToList();


            return View(userOrders);

        }

        public IActionResult OrderDetails(int id)
        {
            Order order = iorderRepo.getById(id);

            if (order != null)
            {
                Cart cart = icartRepo.getById(order.cart_id);
                List<CartItem> cartItems = iCartitemrepo.getCartItemByCardId(cart.Id);
                List<decimal> TotalPrice =new List<decimal>();
                int i = 0;
                foreach (var cartItem in cartItems)
                {
                    TotalPrice[i] = cartItem.Quantity * cartItem.Product.Price;
                    i++;

                }

                ViewBag.Order = order;
                ViewBag.CartItems = cartItems;
                ViewBag.TotalPrice = TotalPrice;


                return View();
            }
            else return View();


        }

    }
}
