using E_Commerce.Models;
using MVC_Project.Models;

namespace E_Commerce.Repository.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly ECommerceContext context;
        public UserRepository(ECommerceContext context)
        {
            this.context = context;
        }

        public User getUserByID(string id)
        {
            User user = context.Users.FirstOrDefault(u => u.ApplicationIdentityUser_id == id);

            if (user != null)
                return user;

            return null;
        }
        public void add(User entity)
        {
            throw new NotImplementedException();
        }

        public void delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> getAll(string include = "")
        {
            throw new NotImplementedException();
        }

        public User getById(int id)
        {
            throw new NotImplementedException();
        }

        public void update(User entity)
        {
            throw new NotImplementedException();
        }
        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}
