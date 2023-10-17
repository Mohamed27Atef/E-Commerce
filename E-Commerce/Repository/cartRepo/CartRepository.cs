using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;

namespace E_Commerce.Repository.cartRepo
{
    public class CartRepository : ICartRepository
    {


        private readonly ECommerceContext context;
        public CartRepository(ECommerceContext context)
        {
            this.context = context;
        }

        public void add(Cart entity)
        {
            context.Carts.Add(entity);
           
        }

        public void delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Cart> getAll(string include = "")
        {
            var query = context.Carts.AsQueryable();
            if (!String.IsNullOrEmpty(include))
            {
                var includes = include.Split(",");
                foreach (var inc in includes)
                {
                    query = query.Include(inc.Trim());
                }
            }
            return query.ToList();
        }

        public Cart? getById(int id)
        {
            var cart = context.Carts.FirstOrDefault(p => p.Id == id);
            if (cart != null)
                return cart;

            return null;
        }

        public Cart getCartByUserId(int id)
        {
            return context.Carts.FirstOrDefault(c => c.user_id == id);
        }

        public void update(Cart entity)
        {
            throw new NotImplementedException();
        }
        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        bool ICartRepository.CheckIfUserHasCart(int user_id)
        {
            return context.Carts.Where(r => r.user_id == user_id).FirstOrDefault() == null ? false : true;
        }
    }
}
