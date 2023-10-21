using E_Commerce.Models;
using MVC_Project.Models;

namespace E_Commerce.Repository.OrderHistoryRepo
{
    public interface IOrderHistoryRepository : IGenericRepository<OrderHistory>
    {
        public List<OrderHistory> GetByOrderId(int id);
    }
    
}
