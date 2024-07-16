using BussinessObject.Models;
using Repository.Interface;
using Service.Interface;
using System.Linq.Expressions;

namespace Service.Implement
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IBaseCRUD<OrderItem> _repo;

        public OrderItemService(IBaseCRUD<OrderItem> repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public async Task<OrderItem> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("ID cannot be null or empty.", nameof(id));
            }

            var orderItem = await _repo.GetByIdAsync(id);
            if (orderItem == null)
            {
                throw new KeyNotFoundException($"Order item with ID {id} not found.");
            }

            return orderItem;
        }

        public async Task<List<OrderItem>> GetAllAsync()
        {
            var orderItems = await _repo.GetAllAsync();
            return orderItems.ToList();
        }

        public async Task AddAsync(OrderItem entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Order item entity cannot be null.");
            }

            await _repo.AddAsync(entity);
        }

        public async Task UpdateAsync(OrderItem entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Order item entity cannot be null.");
            }

            var existingOrderItems = await _repo.GetByIdAsync(entity.OrderItemId.ToString());
            if (existingOrderItems == null)
            {
                throw new KeyNotFoundException($"Order item with ID {entity.OrderItemId} not found.");
            }

            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("ID cannot be null or empty.", nameof(id));
            }

            var existingOrderItem = await _repo.GetByIdAsync(id);
            if (existingOrderItem == null)
            {
                throw new KeyNotFoundException($"Order item with ID {id} not found.");
            }

            await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<OrderItem>> FindAsync(Expression<Func<OrderItem, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentException("Predicate is null");
            }
            return await _repo.FindAsync(predicate);
        }
    }
}
