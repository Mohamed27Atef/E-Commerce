using E_Commerce.Models;
using E_Commerce.Repository.CartItemrepo;
using E_Commerce.Repository.cartRepo;
using E_Commerce.Repository.CategoryRepo;
using E_Commerce.Repository.ProductImagesRepo;
using E_Commerce.Repository.ProductRepo;
using E_Commerce.Repository.ReviewRepo;
using E_Commerce.Repository.UserRepo;
using E_Commerce.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.RegularExpressions;


namespace E_Commerce.Controllers
{
  
    public class ProductController : Controller
    {
        private readonly IProductRepository iproductRepo;
        private readonly ICategoryRepository icategoryRepo;
        private readonly ICartItemRepository iCartitemrepo;
        private readonly ICartRepository icartRepo;
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly IUserRepository iuserRepo;
        private readonly IReviewRepo ireviewRepo;
        private readonly IproductImagesRepositort iproductImagesRepo;
        public ProductController(IProductRepository iproductRepo,
            ICategoryRepository icategoryRepo, 
            ICartItemRepository iCartitemrepo,
            ICartRepository icartRepo,
            UserManager<ApplicationIdentityUser> _userManager,
            IUserRepository IuserRepo,
            IReviewRepo ireview,
            IproductImagesRepositort _iproductImagesRepo)
        {
            // inject DBContext
            this.iproductRepo = iproductRepo;
            this.icategoryRepo = icategoryRepo;
            this.iCartitemrepo = iCartitemrepo;
            this.icartRepo = icartRepo;
            this._userManager = _userManager;
            this.iuserRepo = IuserRepo;
            this.ireviewRepo = ireview;
            this.iproductImagesRepo = _iproductImagesRepo;
        }

        // Get All
      
        public IActionResult index()
        {
            if (User.Identity.IsAuthenticated)
            {
                string applicationUserId = User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

                int user_id = iuserRepo.getUserByApplicationId(applicationUserId);
                if(user_id == 0)
                    createUser(applicationUserId);
                if(!icartRepo.CheckIfUserHasCart(user_id))
                    createUserCart(user_id);
            }

           
            ViewData["category"] = icategoryRepo.getAll();
            return View();
        }

        // get all product 

        public IActionResult getAllProduct()
        {
            var allProducts = iproductRepo.getAll();
            return PartialView("_getAllPartial", allProducts);
        }



        // Get By Id

        public IActionResult getById(int id)
        {

            Product prd = iproductRepo.getById(id);
            if (prd == null) return Json("");
            var newModel = new
            {
                prd.Id,
                prd.Name,
                prd.image,
                prd.Price
            };
            return Json(newModel);
        }


        // Get By Name
        [AllowAnonymous]
        public IActionResult getByName(string name)
        {
            Product prd = iproductRepo.getByName(name);

            return View("getById", prd);
        }
        // Get By Brand 
        [AllowAnonymous]
        public IActionResult getByBrand(string brand)
        {
            var prds = iproductRepo.getByBrand( brand);


            return View("getAll", prds);
        }


        // Post Product 
        [Authorize(Roles = "admin")]
        public IActionResult addProduct()
        {

            ViewData["Category"] = icategoryRepo.getAll();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> addProduct(AddProdcutviewModel product, IFormFile image, List<IFormFile> files)
        {
            List<string> filesNames = new List<string>();
            if (ModelState.IsValid)
            {
                string imageName = await updateImageAndReturnItsName(image);
                foreach (var file in files)
                {
                    string fileName = await updateImageAndReturnItsName(file);
                    filesNames.Add(fileName);

                }
                if (imageName != null)
                {
                    // map addProductViewModel to productModel and add this product to the database
                    mapAddProductViewModelToProductModelAndAddToDB(product, imageName, filesNames);
                    return RedirectToAction("index");
                }
                else
                    ModelState.AddModelError("", "invalid image");
            }

            return RedirectToAction();
        }


        public void mapAddProductViewModelToProductModelAndAddToDB(AddProdcutviewModel product, string imageName, List<string> fileNames)
        {
            Product newProduct = new Product()
            {
                Brand = product.Brand,
                Name = product.Name,
                image = imageName,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryId = product.category_id,
            };

            iproductRepo.add(newProduct);
            iproductRepo.SaveChanges();

            //add objects of images for one product
            if (fileNames != null)
            {
                foreach (var item in fileNames)
                {
                    ProductImage productImages = new ProductImage()
                    {
                        ProductId = newProduct.Id,
                        Image = item
                    };
                    iproductImagesRepo.add(productImages);
                }
                iproductImagesRepo.SaveChanges();
            }

        }


        public async Task<string> updateImageAndReturnItsName(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {

                var extention = Path.GetExtension(image.FileName);
                var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                var imageName = fileName + new Guid() + extention;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", imageName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
                return imageName;
            }

            return null;
        }

        // Update Product
        [Authorize(Roles = "admin")]
        public IActionResult UpdateProduct(int productId)
        {

            Product? product = iproductRepo.getById(productId);
            var prdVM = new UpdateProductViewModel()
            {

                Brand = product.Brand,
                id = product.Id,
                category_id = product.CategoryId,
                Price = product.Price,
                Description = product.Description,
                Name = product.Name,
                StockQuantity = product.StockQuantity,
            };

            if(product == null)
                return RedirectToAction("index");

            ViewData["Category"] = icategoryRepo.getAll();
            return View(prdVM);
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateProduct(UpdateProductViewModel product)
        {

            if (ModelState.IsValid)
            {
                Product oldProduct = iproductRepo.getById(product.id);
                if (oldProduct == null)
                {
                    ModelState.AddModelError("", "invalid");
                    ViewData["Category"] = icategoryRepo.getAll();
                    return View( product);
                }else
                {
                    oldProduct.Brand = product.Brand;
                    oldProduct.Description = product.Description;
                    oldProduct.Name = product.Name;
                    oldProduct.CategoryId = product.category_id;
                    oldProduct.Price = product.Price;
                }
                iproductRepo.update(oldProduct);
                iproductRepo.SaveChanges();


            }

            return RedirectToAction("index");
        }


        public bool checkImage(string image)
        {
            Regex regexPattern = new Regex("[.](JPE?G|PNG|GIF|BMP|jpe?g|png|gif|bmp)$");
            return regexPattern.IsMatch(image);
        }

        public bool checkStockQuantity(int StockQuantity)
        {
            return StockQuantity >= 0;
        }



        // Delete Product 
        public IActionResult delete()
        {
            var products = iproductRepo.getAll("Category");
            return View(products);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult delete(int id)
        {
            iproductRepo.delete(id);
            iproductRepo.SaveChanges();

            return RedirectToAction("delete");
        }




        //Product Details

        public IActionResult Details(int id)
        {
            if (User.Identity.IsAuthenticated)
            {

                string applicationUserId = User.Claims
                       .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

                int user_id = iuserRepo.getUserByApplicationId(applicationUserId);

                bool isReviewed = ireviewRepo.getByUserId(user_id, id);
                ViewData["isReview"] = isReviewed;
            }
            ViewData["reviews"] = ireviewRepo.getByProduct(id);
            var product = iproductRepo.getById(id);
            return View("details", product);
        }

        [HttpPost]
        public IActionResult addReview(Review review)
        {
            if (ModelState.IsValid)
            {
                
                Review newReview = new Review()
                {
                    ProductId = review.ProductId,
                    UserId = review.UserId,
                    Rate = review.Rate,
                    Text = review.Text,
                    PostDate = DateTime.Now
                };

                ireviewRepo.add(newReview);
                ireviewRepo.SaveChanges();

                return RedirectToAction("index");
            }
            else
            {
                return RedirectToAction();
            }
        }

        

        public IActionResult DeleteCartItemById(int id)
        {
            iCartitemrepo.delete(id);
            iCartitemrepo.SaveChanges();
            return RedirectToAction("Index", "Order");
        }



        public IActionResult search(string search, int cat = 0) {
            List<Product> products = iproductRepo.search(search, cat);
            return PartialView("_getAllPartial", products);
        }



        void createUser(string applicationUserId)
        {
            User user = new User()
            {
                ApplicationIdentityUser_id = applicationUserId,
            };

            iuserRepo.add(user);
            iuserRepo.SaveChanges();

        }

        void createUserCart(int userId)
        {
            Cart cart = new Cart()
            {
                user_id = userId,
                TotalPrice = 0,
            };

            icartRepo.add(cart);
            icartRepo.SaveChanges();

        }

        public IActionResult GetFilteredProducts(int categoryId)
        {
            List<Product> filteredProducts;

            if (categoryId > 0)
            {
                filteredProducts = iproductRepo.getByCategory(categoryId);
            }
            else
            {
                filteredProducts = iproductRepo.getAll();
            }

            return PartialView("_getAllPartial", filteredProducts);
        }


    }



}
