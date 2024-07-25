using BussinessObject.Models;
using System.Linq.Expressions;

namespace Service.Interface
{
    public interface IOrderService
    {
        Task<Order> GetByIdAsync(string id);
        Task<List<Order>> GetAllAsync();
        Task AddAsync(Order entity);
        Task UpdateAsync(Order entity);
        Task DeleteAsync(string id);
        Task<IEnumerable<Order>> FindAsync(Expression<Func<Order, bool>> predicate);
        Task<List<Order>> OrderHistory(string userId);
        Task ComfirmOrder(string userId);
    }
}
