using BussinessObject.DTO;
using BussinessObject.Models;
using System.Linq.Expressions;

namespace Service.Interface
{
    public interface IDeliveryService
    {
        Task<IEnumerable<Delivery>> GetAllAsync();
        Task<Delivery> GetByIdAsync(string id);
        Task<IEnumerable<Delivery>> FindAsync(Expression<Func<Delivery, bool>> predicate);
        Task<Delivery> AddAsync(Delivery entity);
        Task<Delivery> UpdateAsync(Delivery entity);
        Task DeleteAsync(string id);
        Task<List<DeliveryResponse>> GetDeliveryResponsesByAdmin();
    }
}
