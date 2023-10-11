using E_Commerce.Models;

namespace E_Commerce.Repository.UserRepo
{
    public interface IUserRepository:IGenericRepository<User>
    {
        public User getUserByID(string id);
    }
}
