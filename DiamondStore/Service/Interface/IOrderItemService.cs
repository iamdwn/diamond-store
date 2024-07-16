using BussinessObject.Models;
using System.Linq.Expressions;

namespace Service.Interface
{
    public interface IOrderItemService
    {
        Task<OrderItem> GetByIdAsync(string id);
        Task<List<OrderItem>> GetAllAsync();
        Task AddAsync(OrderItem entity);
        Task UpdateAsync(OrderItem entity);
        Task DeleteAsync(string id);
        Task<IEnumerable<OrderItem>> FindAsync(Expression<Func<OrderItem, bool>> predicate);
    }
}
