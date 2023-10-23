using E_Commerce.Repository.CategoryRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using System.Data;

namespace E_Commerce.Controllers
{
    [Authorize(Roles = "admin")]
    public class categoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public categoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [Authorize(Roles = "admin")]
        public IActionResult addCategory()
        {
            return View("addCategory");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult addCategory(Category cateogry)
        {

            categoryRepository.add(cateogry);
            categoryRepository.SaveChanges();
            return RedirectToAction("index", "product");
        }

        public IActionResult getCategory()
        {
            var categires = categoryRepository.getAll();
            return View(categires);
        }
        
        public IActionResult EditeCategory()
        {
            return View();
        }

    }
}
