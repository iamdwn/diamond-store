using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IDeliverManagementRepo
    {
         Task<List<User>> GetManagerList();

        Task<List<User>> GetShipperList();

        Task<IEnumerable<Delivery>> GetAllAsyncShipper(Guid shipperId);

        Task<List<Order>> GetOrderList();
        Task<IEnumerable<Delivery>> GetAllAsync();
        Task<Delivery> GetByIdAsync(string id);
        Task<IEnumerable<Delivery>> FindAsync(Expression<Func<Delivery, bool>> predicate);
        Task<Delivery> Find(Expression<Func<Delivery, bool>> predicate);
        Task<Delivery> AddAsync(Delivery entity);
        Task<Delivery> UpdateAsync(Delivery entity);
        Task DeleteAsync(string id);
        Task<bool> CreateAsync(Delivery entity);
        Task<bool> Update(Delivery entity);
    }
}
