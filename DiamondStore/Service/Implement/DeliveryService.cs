using BussinessObject.Models;
using Repository.Interface;
using Service.Interface;
using System.Linq.Expressions;

namespace Service.Implement
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IBaseCRUD<Delivery> _repo;

        public DeliveryService(IBaseCRUD<Delivery> repo)
        {
            _repo = repo;
        }

        public async Task<Delivery> AddAsync(Delivery entity)
        {
            return await _repo.AddAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<Delivery>> FindAsync(Expression<Func<Delivery, bool>> predicate)
        {
            return await _repo.FindAsync(predicate);
        }

        public async Task<IEnumerable<Delivery>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Delivery> GetByIdAsync(string id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Delivery> UpdateAsync(Delivery entity)
        {
            return await _repo.UpdateAsync(entity);
        }
    }
}
