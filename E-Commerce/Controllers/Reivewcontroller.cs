using E_Commerce.Repository.ProductRepo;
using E_Commerce.Repository.ReviewRepo;
using E_Commerce.Repository.UserRepo;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    public class Reivewcontroller : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IReviewRepo reviewRepo;
        private readonly IProductRepository productRepository;

        public Reivewcontroller(IUserRepository userRepository, IReviewRepo reviewRepo, IProductRepository productRepository)
        {
            this.userRepository = userRepository;
            this.reviewRepo = reviewRepo;
            this.productRepository = productRepository;
        }


        public IActionResult postReview(int productId, string txt, int rate = 0)
        {
            string IDClaim =
               User.Claims
               .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value; // from cookie...

            Review review = new Review()
            {
                PostDate = DateTime.Now,
                ProductId = productId,
                Text = txt,
                Rate= rate,
                UserId = userRepository.getUserByApplicationId(IDClaim)
            };

            reviewRepo.add(review);
            reviewRepo.SaveChanges();

            List<Review> reivews = reviewRepo.getByProduct(productId);

            float totalReview = 0;

            foreach (var item in reivews)
            {
                totalReview += item.Rate;
            }

            Product product = productRepository.getbyid(productId);
            product.rate = (totalReview / reivews.Count()).ToString("0.00");

            productRepository.update(product);
            productRepository.SaveChanges();



            return Json("done");
        }
    }
}
