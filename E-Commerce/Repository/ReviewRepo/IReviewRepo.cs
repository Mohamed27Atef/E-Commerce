using MVC_Project.Models;

namespace E_Commerce.Repository.ReviewRepo
{
    public interface IReviewRepo
    {
        List<Review> getByProduct(int id);
        bool getByUserId(int userId);

        public void add(Review review);

        public int SaveChanges();
    }
}