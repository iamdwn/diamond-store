using BussinessObject.Models;
using Repository.Interface;
using Service.Interface;
using System.Linq.Expressions;

namespace Service.Implement
{
    public class VoucherService : IVoucherService
    {
        private readonly IBaseCRUD<Voucher> _repo;

        public VoucherService(IBaseCRUD<Voucher> repo)
        {
            _repo = repo;
        }

        public async Task<Voucher> AddAsync(Voucher entity)
        {
            return await _repo.AddAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<Voucher>> FindAsync(Expression<Func<Voucher, bool>> predicate)
        {
            return await _repo.FindAsync(predicate);
        }

        public async Task<IEnumerable<Voucher>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Voucher> GetByIdAsync(string id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Voucher> UpdateAsync(Voucher entity)
        {
            return await _repo.UpdateAsync(entity);
        }
    }
}
