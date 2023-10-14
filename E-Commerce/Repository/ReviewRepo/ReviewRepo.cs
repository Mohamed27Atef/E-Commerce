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
            var reviews = context.Review.Where(a => a.ProductId == id).Include(a=>a.User).ToList();

            if (reviews != null)
                return reviews;

            return null;
        }
    }
}
