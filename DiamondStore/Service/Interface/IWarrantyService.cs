using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IWarrantyService
    {
        Task<IEnumerable<Warranty>> GetAllAsync();
        Task<Warranty> GetByIdAsync(string id);
        Task<IEnumerable<Warranty>> FindAsync(Expression<Func<Warranty, bool>> predicate);
        Task<Warranty> AddAsync(Warranty entity);
        Task<Warranty> UpdateAsync(Warranty entity);
        Task DeleteAsync(string id);
        Task<List<User>> GetCustomerList();
    }
}
