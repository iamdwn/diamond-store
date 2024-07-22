﻿using BussinessObject.Models;
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

        public async Task DeleteAsync(string id)
        {
            using (var _context = new DiamondStoreContext())
            {
                var entity = await _context.Products.FindAsync(Guid.Parse(id));
                if (entity != null)
                {
                    _context.Products.Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate)
        {
            using (var _context = new DiamondStoreContext())
            {
                return await _context.Products.Where(predicate).Include(p => p.Category).ToListAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using (var _context = new DiamondStoreContext())
            {
                return await _context.Products.Include(p => p.Category).ToListAsync();
            }
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            using (var _context = new DiamondStoreContext())
            {
                return await _context.Products.Include(p => p.Category).ThenInclude(c => c.Distributor).FirstOrDefaultAsync(p => p.ProductId.ToString().Equals(id));
            }
        }

        public async Task<Product> UpdateAsync(Product entity)
        {
            using (var _context = new DiamondStoreContext())
            {
                var product = _context.Products.FirstOrDefault(p => p.ProductId.Equals(entity.ProductId));

                if (product != null)
                {
                    product.Name = entity.Name;
                    product.Price = entity.Price;
                    product.Description = entity.Description;
                    product.IsExpired = entity.IsExpired;
                    //product.CategoryId = entity.CategoryId;
                    _context.Products.Update(product);
                    await _context.SaveChangesAsync();
                    return entity;
                }
                return null;
            }
        }
    }
}
