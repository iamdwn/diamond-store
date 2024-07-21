using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.Implement
{
    public class OrderRepo : IBaseCRUD<Order>
    {
        public async Task<Order> GetByIdAsync(string id)
        {
            try
            {
                using (var _context = new DiamondStoreContext())
                {
                    return await _context.Orders.FirstAsync(o => o.OrderId == Guid.Parse(id));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Order> UpdateAsync(Order entity)
        {
            try
            {
                using (var _context = new DiamondStoreContext())
                {
                    var order = await _context.Orders.FindAsync(entity.OrderId);
                    if (order != null)
                    {
                        order.TotalAmount = entity.TotalAmount;
                        order.Status = entity.Status;
                        order.VoucherId = entity.VoucherId;
                        order.TotalPrice = entity.TotalPrice;
                        await _context.SaveChangesAsync();
                    }
                    return order;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(string id)
        {
            try
            {
                using (var _context = new DiamondStoreContext())
                {
                    var order = await _context.Orders
                                              .Include(o => o.OrderItems)
                                              .Include(o => o.Deliveries) // Include deliveries
                                              .FirstOrDefaultAsync(o => o.OrderId == Guid.Parse(id));
                    if (order != null)
                    {
                        _context.OrderItems.RemoveRange(order.OrderItems);
                        _context.Deliveries.RemoveRange(order.Deliveries); // Remove deliveries
                        _context.Orders.Remove(order);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Order>> FindAsync(Expression<Func<Order, bool>> predicate)
        {
            try
            {
                using (var _context = new DiamondStoreContext())
                {
                    return await _context.Orders.AsNoTracking().Where(predicate).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Order> AddAsync(Order entity)
        {
            try
            {
                using (var _context = new DiamondStoreContext())
                {
                    _context.Orders.Add(entity);
                    await _context.SaveChangesAsync();
                    return entity;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            try
            {
                using (var _context = new DiamondStoreContext())
                {
                    return await _context.Orders.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Order> Find(Expression<Func<Order, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateAsync(Order entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
