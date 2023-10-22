using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;

namespace E_Commerce.Repository.OrderRepo
{
    public class OrderRepository : IOrderRepository
    {

        private readonly ECommerceContext context;
        public OrderRepository(ECommerceContext context)
        {
            this.context = context;
        }
        
        public void add(Order entity)
        {
            context.Order.Add(entity);
        }

        public void delete(int id)
        {
            Order order = getById(id);
            context.Order.Remove(order);
        }

        public List<Order> getAll(string include = "")
        {
            var query = context.Order.AsQueryable();
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

        public Order getById(int id)
        {
           return context.Order.Include(o=>o.User).ThenInclude(u=> u.ApplicationIdentityUser).FirstOrDefault(o => o.Id == id);
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public void update(Order entity)
        {
            context.Order.Update(entity);
        }
    }
}
