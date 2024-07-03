using BussinessObject.Models;

namespace Service.Interface
{
    public interface IOrderService
    {
        Task<Order> GetByIdAsync(string id);
        Task<List<Order>> GetAllAsync();
        Task AddAsync(Order entity);
        Task UpdateAsync(Order entity);
        Task DeleteAsync(string id);
    }
}
