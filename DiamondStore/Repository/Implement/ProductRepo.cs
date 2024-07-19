using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System.Linq.Expressions;

namespace Repository.Implement
{
    public class ProductRepo : IBaseCRUD<Product>
    {
        public async Task<Product> AddAsync(Product entity)
        {
            using (var _context = new DiamondStoreContext())
            {
                await _context.Products.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
        }

        public Task<bool> CreateAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(string id)
        {
            using (var _context = new DiamondStoreContext())
            {
                var entity = await _context.Products.FindAsync(id);
                if (entity != null)
                {
                    _context.Products.Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public Task<Product> Find(Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate)
        {
            using (var _context = new DiamondStoreContext())
            {
                return await _context.Products.Where(predicate).ToListAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using (var _context = new DiamondStoreContext())
            {
                return await _context.Products.ToListAsync();
            }
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            using (var _context = new DiamondStoreContext())
            {
                return await _context.Products.FindAsync(id);
            }
        }

        public Task<bool> Update(Product entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> UpdateAsync(Product entity)
        {
            using (var _context = new DiamondStoreContext())
            {
                _context.Products.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
        }
    }
}
