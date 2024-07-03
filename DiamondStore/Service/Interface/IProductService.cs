using BussinessObject.Models;
using System.Linq.Expressions;

namespace Service.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(string id);
        Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate);
        Task<Product> AddAsync(Product entity);
        Task<Product> UpdateAsync(Product entity);
        Task DeleteAsync(string id);
    }
}
