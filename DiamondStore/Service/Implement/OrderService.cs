using BussinessObject.Models;
using Repository.Interface;
using Service.Interface;
using System.Linq.Expressions;

namespace Service.Implement
{
    public class OrderService : IOrderService
    {
        private readonly IBaseCRUD<Order> _repo;

        public OrderService(IBaseCRUD<Order> repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public async Task<Order> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("ID cannot be null or empty.", nameof(id));
            }

            var order = await _repo.GetByIdAsync(id);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }

            return order;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            var orders = await _repo.GetAllAsync();
            return orders.ToList();
        }

        public async Task AddAsync(Order entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Order entity cannot be null.");
            }

            await _repo.AddAsync(entity);
        }

        public async Task UpdateAsync(Order entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Order entity cannot be null.");
            }

            var existingOrder = await _repo.GetByIdAsync(entity.OrderId.ToString());
            if (existingOrder == null)
            {
                throw new KeyNotFoundException($"Order with ID {entity.OrderId} not found.");
            }

            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("ID cannot be null or empty.", nameof(id));
            }

            var existingOrder = await _repo.GetByIdAsync(id);
            if (existingOrder == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }

            await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<Order>> FindAsync(Expression<Func<Order, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentException("Predicate is null");
            }           
            return await _repo.FindAsync(predicate);
        }
    }
}
