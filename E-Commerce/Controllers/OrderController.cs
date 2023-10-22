using E_Commerce.Models;
using E_Commerce.Repository.CartItemrepo;
using E_Commerce.Repository.cartRepo;
using E_Commerce.Repository.CategoryRepo;
using E_Commerce.Repository.OrderRepo;
using E_Commerce.Repository.ProductRepo;
using E_Commerce.Repository.ReviewRepo;
using E_Commerce.Repository.UserRepo;
using E_Commerce.Repository.OrderHistoryRepo;
using E_Commerce.ViewModel.OrderViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.AspNetCore.Authentication;

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
        private readonly IOrderHistoryRepository iorderHistoryRepo;



        public OrderController(IProductRepository iproductRepo, ICategoryRepository icategoryRepo,
            ICartItemRepository iCartitemrepo, ICartRepository icartRepo, UserManager<ApplicationIdentityUser> _userManager,
            IUserRepository IuserRepo, IReviewRepo ireview,IOrderRepository _iorderRepo, IOrderHistoryRepository _iorderHistoryRepo)
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
            iorderHistoryRepo = _iorderHistoryRepo;
        }
        [Authorize]
        public IActionResult Index()
        {
            List<OrderedItemForUserVM> orderedItem = getproductByCartItem();
            if (orderedItem.Count == 0)
                return RedirectToAction("index", "product");

            return View(orderedItem);
        }

        public IActionResult getTotalPrice()
        {
            List<OrderedItemForUserVM> orderedItem = getproductByCartItem();
            decimal totalPrice = 0;
            foreach(OrderedItemForUserVM item in orderedItem)
            {
                totalPrice += item.price;
            }
            return Json(totalPrice);
        }

        [Authorize]
        public List<OrderedItemForUserVM> getproductByCartItem()
        {
            string IDClaim =
               User.Claims
               .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value; // from cookie...

            Cart cart = icartRepo.getCartByUserId(iuserRepo.getUserByApplicationId(IDClaim));

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
                    amountStock = prdLst[i].StockQuantity
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

        [HttpGet]
        public IActionResult saveOrder(decimal totalPrice)
        {
            return PartialView("createOrder", totalPrice);
        }

        [HttpPost]
        public IActionResult saveOrder(OrderCheckedUserVM orderChecked)
        {
            string IDClaim =
               User.Claims
               .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value; // from cookie...
            int user_id = iuserRepo.getUserByApplicationId(IDClaim);
            Cart cart = icartRepo.getCartByUserId(user_id);
            Order order = new Order()
                {
                    cart_id = cart.Id,
                    TotalPrice = orderChecked.TotalPrice,
                    Status = OrderStatus.Shipped,
                    OrderDate = DateTime.Now,
                    UserId = user_id,
                    Street = orderChecked.Street,
                    City = orderChecked.City,
                    Country = orderChecked.Country
            };


            iorderRepo.add(order);
            iorderRepo.SaveChanges();
            var cartitems = iCartitemrepo.getAll();

            //get all cartitem transfer it to history
            List<OrderHistory> history = new List<OrderHistory>();

            foreach (var item in cartitems)
            {
                OrderHistory newHistory = new OrderHistory { 
                OrderId = order.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price

                };
            iorderHistoryRepo.add(newHistory);
            }
            iorderHistoryRepo.SaveChanges();


            //delet all cartitem for user after order checked
            foreach (var item in cartitems)
            {
                iCartitemrepo.delete(item.ProductId);
            }
            iCartitemrepo.SaveChanges();
            return View(order);
         }

        public IActionResult OrderHistory()
        {

            string IDClaim =
               User.Claims
               .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value; // from cookie...
            int user_id = iuserRepo.getUserByApplicationId(IDClaim);

            List<Order> userOrders = iorderRepo.getAll()
                .Where(order => order.UserId == user_id)
                .ToList();


            return View(userOrders);

        }

        public IActionResult OrderDetails(int id)
        {
            Order order = iorderRepo.getById(id);

            if (order != null)
            {
                Cart cart = icartRepo.getById(order.cart_id);
                List<OrderHistory> cartItems = iorderHistoryRepo.GetByOrderId(id);


                ViewBag.Order = order;
                ViewBag.CartItems = cartItems;


                return View();
            }
            else return View();


        }

    }
}
