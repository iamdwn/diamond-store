﻿using BussinessObject.Models;
using Repository.Interface;
using Service.Interface;
using System.Linq.Expressions;

namespace Service.Implement
{
    public class ProductService : IProductService
    {
        private readonly IBaseCRUD<Product> _repo;
        private readonly IProductRepo _productRepo;

        public ProductService(IBaseCRUD<Product> repo, IProductRepo productRepo)
        {
            _repo = repo;
            _productRepo = productRepo;
        }

        public async Task<Product> AddAsync(Product entity)
        {
            return await _repo.AddAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate)
        {
            return await _repo.FindAsync(predicate);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Product> UpdateAsync(Product entity)
        {
            return await _repo.UpdateAsync(entity);
        }
        public async Task<IEnumerable<Category>> GetListCategory()
        {
            return await _productRepo.GetListCategory();
        }
    }
}
