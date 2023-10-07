using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;

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
        public IActionResult getAll()
        {
            var allProducts = context.Product.Include(p=>p.Images).ToList();
            if (allProducts == null)
                return RedirectToAction("Error", "Home");

            return View(allProducts);
        }
        // Get By Id
        public IActionResult getById(int id)
        {
            Product prd = context.Product.Include(p => p.Images).FirstOrDefault(p=>p.Id==id);
            if (prd == null)
                return RedirectToAction("Error", "Home");

            return View(prd);
        }
        // Get By Name
        public IActionResult getByName(string name)
        {
            Product prd = context.Product.Include(p => p.Images).FirstOrDefault(p=> p.Name == name);
            if (prd == null)
                return RedirectToAction("Error", "Home");

            return View("getById", prd);
        }
        // Get By Brand 
        public IActionResult getByBrand(string brand)
        {
            var prds = context.Product.Include(p => p.Images).Where(p => p.Brand == brand).ToList();
            if (prds == null)
                return RedirectToAction("Error","Home");

            return View("getAll", prds);
        }
        #endregion

        #region Atef Task

        // Post Product 
        #endregion

        #region Ghaly Task

        // Update Product

        #endregion

        #region Sayed Task

        // Delete Product 
        #endregion
    }
}
