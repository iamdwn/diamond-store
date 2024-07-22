using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class DeliverymanagementRepo : IBaseCRUD<Delivery>,IDeliverManagementRepo
    {
        public async Task<Delivery> AddAsync(Delivery entity)
        {
            try
            {
                using (var _context = new DiamondStoreContext())
                {
                    await _context.Deliveries.AddAsync(entity);
                    await _context.SaveChangesAsync();
                    return entity;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            };
        }

        public Task<bool> CreateAsync(Delivery entity)
        {
            throw new NotImplementedException();
        }

        public  async Task DeleteAsync(string id)
        {
            using (var _context = new DiamondStoreContext())
            {
                var entity = await _context.Deliveries.FindAsync(Guid.Parse(id));
                if (entity != null)
                {
                    _context.Deliveries.Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public Task<Delivery> Find(System.Linq.Expressions.Expression<Func<Delivery, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Delivery>> FindAsync(System.Linq.Expressions.Expression<Func<Delivery, bool>> predicate)
        {
            using (var _context = new DiamondStoreContext())
            {
                return await _context.Deliveries.Where(predicate).ToListAsync();
            }
        }

        public async Task<IEnumerable<Delivery>> GetAllAsync()
        {
            using (var _context = new DiamondStoreContext())
            {
                return await _context.Deliveries.Include(d => d.Manager)
                .Include(d => d.Order)
                .Include(d => d.Shiper).AsNoTracking().ToListAsync();
            }
        }

        public async Task<Delivery> GetByIdAsync(string id)
        {
            using (var _context = new DiamondStoreContext())
            {
                return await _context.Deliveries.FindAsync(Guid.Parse(id));
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

        public async Task<List<User>> GetManagerList()
        {
            using (var _context = new DiamondStoreContext())
            {
            return   await _context.Users.Include(d => d.Role).Where(e => e.Role.Id == 3).ToListAsync();
            }
        }
        public async Task<List<User>> GetShipperList()
        {
            using (var _context = new DiamondStoreContext())
            {
                return await _context.Users.Include(d => d.Role).Where(e => e.Role.Id == 2).ToListAsync();
            }
        }
        public async Task<List<Order>> GetOrderList()
        {
            using (var _context = new DiamondStoreContext())
            {
                return await _context.Orders.ToListAsync();
            }
        }
    }
}
