using BussinessObject.Models;
using System.Linq.Expressions;

namespace Service.Interface
{
    public interface IVoucherService
    {
        Task<IEnumerable<Voucher>> GetAllAsync();
        Task<Voucher> GetByIdAsync(string id);
        Task<IEnumerable<Voucher>> FindAsync(Expression<Func<Voucher, bool>> predicate);
        Task<Voucher> AddAsync(Voucher entity);
        Task<Voucher> UpdateAsync(Voucher entity);
        Task DeleteAsync(string id);
    }
}
