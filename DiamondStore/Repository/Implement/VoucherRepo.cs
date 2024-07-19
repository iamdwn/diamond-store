using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System.Linq.Expressions;

namespace Repository.Implement
{
    public class VoucherRepo : IBaseCRUD<Voucher>
    {
        public async Task<Voucher> AddAsync(Voucher entity)
        {
            using (var _context = new DiamondStoreContext())
            {
                await _context.Vouchers.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
        }

        public Task<bool> CreateAsync(Voucher entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(string id)
        {
            using (var _context = new DiamondStoreContext())
            {
                var entity = await _context.Set<Voucher>().FindAsync(id);
                if (entity != null)
                {
                    _context.Vouchers.Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public Task<Voucher> Find(Expression<Func<Voucher, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Voucher>> FindAsync(Expression<Func<Voucher, bool>> predicate)
        {
            using (var _context = new DiamondStoreContext())
            {
                return await _context.Vouchers.AsNoTracking().Where(predicate).ToListAsync();
            }
        }

        public async Task<IEnumerable<Voucher>> GetAllAsync()
        {
            using (var _context = new DiamondStoreContext())
            {
                return await _context.Vouchers.AsNoTracking().ToListAsync();
            }
        }

        public async Task<Voucher> GetByIdAsync(string id)
        {
            using (var _context = new DiamondStoreContext())
            {
                return await _context.Vouchers.FindAsync(id);
            }
        }

        public Task<bool> Update(Voucher entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Voucher> UpdateAsync(Voucher entity)
        {
            using (var _context = new DiamondStoreContext())
            {
                _context.Vouchers.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
        }
    }
}
