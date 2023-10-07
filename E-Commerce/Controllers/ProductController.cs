using Microsoft.AspNetCore.Mvc;
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

        // Get By Id

        // Get By Name

        // Get By Brand 
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
