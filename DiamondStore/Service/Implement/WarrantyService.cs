using BussinessObject.Models;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class WarrantyService:IWarrantyService
    {
        private readonly IBaseCRUD<Warranty> _repo;

        public WarrantyService(IBaseCRUD<Warranty> repo)
        {
            _repo = repo;
        }

        public async Task<Warranty> AddAsync(Warranty entity)
        {
            return await _repo.AddAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<Warranty>> FindAsync(Expression<Func<Warranty, bool>> predicate)
        {
            return await _repo.FindAsync(predicate);
        }

        public async Task<IEnumerable<Warranty>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Warranty> GetByIdAsync(string id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Warranty> UpdateAsync(Warranty entity)
        {
            return await _repo.UpdateAsync(entity);
        }
    }
}
