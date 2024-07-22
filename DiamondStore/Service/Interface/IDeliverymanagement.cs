using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IDeliverymanagement
    {
        Task<IEnumerable<Delivery>> GetAllAsync();
        Task<Delivery> GetByIdAsync(string id);
        Task<IEnumerable<Delivery>> FindAsync(Expression<Func<Delivery, bool>> predicate);
        Task<Delivery> AddAsync(Delivery entity);
        Task<Delivery> UpdateAsync(Delivery entity);
        Task DeleteAsync(string id);
        Task<List<User>> GetManagerList();
        Task<List<User>> GetShipperList();

        Task<List<Order>> GetOrderList();
    }
}
