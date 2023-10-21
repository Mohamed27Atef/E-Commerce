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

        public Reivewcontroller(IUserRepository userRepository, IReviewRepo reviewRepo)
        {
            this.userRepository = userRepository;
            this.reviewRepo = reviewRepo;
        }


        public IActionResult postReview(int productId, string txt)
        {
            string IDClaim =
               User.Claims
               .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value; // from cookie...

            Review review = new Review()
            {
                PostDate = DateTime.Now,
                ProductId = productId,
                Text = txt,
                Rate= 3,
                UserId = userRepository.getUserByApplicationId(IDClaim)
            };

            reviewRepo.add(review);
            reviewRepo.SaveChanges();
            return Json(review);
        }


    }
}
