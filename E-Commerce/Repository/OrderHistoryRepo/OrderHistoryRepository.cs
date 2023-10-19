using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;

namespace E_Commerce.Repository.OrderHistoryRepo
{
    public class OrderHistoryRepository : IOrderHistoryRepository
    {
       
        private readonly ECommerceContext context;
        public OrderHistoryRepository(ECommerceContext context)
        {
            this.context = context;
        }
        public void add(OrderHistory entity)
        {

            if (entity != null)
            {
                context.OrderHistorys.Add(entity);

            }
        }

        public void delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<OrderHistory> getAll(string include = "")
        {
            var query = context.OrderHistorys.AsQueryable();
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

        public OrderHistory getById(int id)
        {
            return context.OrderHistorys.FirstOrDefault(o => o.id == id);
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        OrderHistory IGenericRepository<OrderHistory>.getById(int id)
        {
            throw new NotImplementedException();
        }

        List<OrderHistory> IGenericRepository<OrderHistory>.getAll(string include)
        {
            throw new NotImplementedException();
        }

        public void update(OrderHistory entity)
        {
            throw new NotImplementedException();
        }
    }
}
