using E_Commerce.Models;
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
        private readonly ECommerceContext context;

        public ProductController(ECommerceContext context)
        {
            // inject DBContext
            this.context = context;
        }


        #region Essa Task
        // Get All
        public IActionResult index()
        {
            var allProducts = context.Product.ToList();
            if (allProducts == null)
                return RedirectToAction("Error", "Home");

            return View(allProducts);
        }
        // Get By Id
        public IActionResult getById(int id)
        {
            Product prd = context.Product.FirstOrDefault(p=>p.Id==id);
            if (prd == null)
                return RedirectToAction("Error", "Home");

            return View(prd);
        }
        // Get By Name
        public IActionResult getByName(string name)
        {
            Product prd = context.Product.FirstOrDefault(p=> p.Name == name);
            if (prd == null)
                return RedirectToAction("Error", "Home");

            return View("getById", prd);
        }
        // Get By Brand 
        public IActionResult getByBrand(string brand)
        {
            var prds = context.Product.Where(p => p.Brand == brand).ToList();
            if (prds == null)
                return RedirectToAction("Error","Home");

            return View("getAll", prds);
        }
        #endregion

        #region Ghaly

        // Post Product 

        public IActionResult addProduct()
        {

            ViewData["Category"] = context.Category.ToList();
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


        public void addProductToDataBase(Product newProduct)
        {
            context.Product.Add(newProduct);
            context.SaveChanges();
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

            addProductToDataBase(newProduct);
        }


        public async Task<string> updateImageAndReturnItsName(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {

                var extention = Path.GetExtension(image.FileName);
                var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                var imageName = fileName.Replace(" ", "") + extention;
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
            UpdateProductViewModel? product = context.Product
                 .Where(p => p.Id == productId)
                 .Select(s => new UpdateProductViewModel()
                 {
                     Brand = s.Brand,
                     id=s.Id,
                     category_id = s.CategoryId,
                     Price = s.Price,
                     Description = s.Description,
                     Name = s.Name, StockQuantity = s.StockQuantity,
                 })
               .FirstOrDefault() ;
            if(product == null)
                return RedirectToAction("index");

            ViewData["Category"] = context.Category.ToList();
            return View(product);
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateProduct(UpdateProductViewModel product)
        {

            if (ModelState.IsValid)
            {
                Product oldProduct = context.Product.Find(product.id);
                if (oldProduct == null)
                {
                    ModelState.AddModelError("", "invalid");
                    ViewData["Category"] = context.Category.ToList();
                    return View( product);
                }else
                {
                    oldProduct.Brand = product.Brand;
                    oldProduct.Description = product.Description;
                    oldProduct.Name = product.Name;
                    oldProduct.CategoryId = product.category_id;
                    oldProduct.Price = product.Price;
                }
                context.Product.Update(oldProduct);
                context.SaveChanges();
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
            var products = context.Product.Include(a => a.Category).ToList();

            return View(products);
        }


        public IActionResult deleteButton(int id)
        {
            var product = context.Product.FirstOrDefault(a => a.Id == id);
            context.Product.Remove(product);
            return RedirectToAction("delete");
        }
        #endregion




    }
}
