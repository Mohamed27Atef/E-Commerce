using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;

namespace E_Commerce.Repository.ReviewRepo
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly ECommerceContext context;
        public ReviewRepo(ECommerceContext context)
        {
            this.context = context;
        }


        public List<Review> getByProduct(int id)
        {
            var reviews = context.Review.Where(a => a.ProductId == id).Include(a=>a.User.ApplicationIdentityUser).ToList();

            if (reviews != null)
                return reviews;

            return null;
        }

        public void add(Review review)
        {

            if (review != null)
            {
                context.Review.Add(review);

            }

        }


        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public bool getByUserId(int userId, int productId)
        {
            Review r = context.Review.Where(e => e.UserId == userId && e.ProductId == productId).FirstOrDefault();
            if(r == null) return false;
            return true;
        }
    }
}
