using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class DeliveryRepo : IBaseCRUD<Delivery>
    {
        public async Task<Delivery> AddAsync(Delivery entity)
        {
            using (var _context = new DiamondStoreContext())
            {
                await _context.Deliveries.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
        }

        public Task<bool> CreateAsync(Delivery entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(string id)
        {
            if (!Guid.TryParse(id, out Guid guid))
            {
                throw new FormatException("Invalid GUID format.");
            }

            using (var _context = new DiamondStoreContext())
            {
                var entity = await _context.Deliveries.FindAsync(guid);
                if (entity != null)
                {
                    _context.Deliveries.Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public Task<Delivery> Find(Expression<Func<Delivery, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Delivery>> FindAsync(Expression<Func<Delivery, bool>> predicate)
        {
            using (var _context = new DiamondStoreContext())
            {
                return await _context.Deliveries.Include(o => o.Order)
                    .Include(o => o.Shiper)
                    .Where(predicate).ToListAsync();
            }
        }

        public async Task<IEnumerable<Delivery>> GetAllAsync()
        {
            using (var _context = new DiamondStoreContext())
            {
                return await _context.Deliveries.Include(o => o.Order).ToListAsync();
            }
        }

        public async Task<Delivery> GetByIdAsync(string id)
        {
            if (!Guid.TryParse(id, out Guid guid))
            {
                throw new FormatException("Invalid GUID format.");
            }

            using (var _context = new DiamondStoreContext())
            {
                return await _context.Deliveries.FindAsync(guid);
            }
        }

        public Task<bool> Update(Delivery entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Delivery> UpdateAsync(Delivery entity)
        {
            using (var _context = new DiamondStoreContext())
            {
                _context.Deliveries.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
        }
    }
}
