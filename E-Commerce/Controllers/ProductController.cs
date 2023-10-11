using E_Commerce.Models;
using E_Commerce.Repository.CategoryRepo;
using E_Commerce.Repository.ProductRepo;
using E_Commerce.ViewModel;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using System.Text.RegularExpressions;

namespace E_Commerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository iproductRepo;
        private readonly ICategoryRepository icategoryRepo;


        public ProductController(IProductRepository iproductRepo, ICategoryRepository icategoryRepo)
        {
            // inject DBContext
            this.iproductRepo = iproductRepo;
            this.icategoryRepo = icategoryRepo;
        }


        #region Essa Task
        // Get All
        public IActionResult index()
        {

            var allProducts = iproductRepo.getAll();

            return View(allProducts);
        }
        // Get By Id
        public IActionResult getById(int id)
        {

            Product prd = iproductRepo.getById(id);

            return View(prd);
        }
        // Get By Name
        public IActionResult getByName(string name)
        {
            Product prd = iproductRepo.getByName(name);


            return View("getById", prd);
        }
        // Get By Brand 
        public IActionResult getByBrand(string brand)
        {
            var prds = iproductRepo.getByBrand( brand);


            return View("getAll", prds);
        }
        #endregion

        #region Ghaly

        // Post Product 

        public IActionResult addProduct()
        {

            ViewData["Category"] = icategoryRepo.getAll();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> addProduct(AddProdcutviewModel product, IFormFile image)
        {

            if (ModelState.IsValid)
            {
                string imageName = await updateImageAndReturnItsName(image);

                if(imageName != null)
                {
                    // map addProductViewModel to productModel and add this product to the database
                    mapAddProductViewModelToProductModelAndAddToDB(product, imageName);
                    return RedirectToAction("index");
                }
                else
                    ModelState.AddModelError("", "invalid image");
            }

            return RedirectToAction();
        }



        public void mapAddProductViewModelToProductModelAndAddToDB(AddProdcutviewModel product, string imageName)
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
            return View(product);
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

        #endregion

        #region Sayed Task

        // Delete Product 
        public IActionResult delete()
        {
            //var products = context.Product.Include(a => a.Category).ToList();
            var products = iproductRepo.getAll("Category");
            return View(products);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult delete(int id)
        {
            iproductRepo.delete(id);

            return RedirectToAction("delete");
        }


        // Product Details

        public IActionResult Details(int id)
        {
            var prpduct = iproductRepo.getById(id);
            

            return View("details", prpduct);
        }

        #endregion

    }
}
